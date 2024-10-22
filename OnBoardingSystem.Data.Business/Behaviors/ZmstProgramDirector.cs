//-----------------------------------------------------------------------
// <copyright file="MailServiceDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using OfficeOpenXml;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Services;
    using OnBoardingSystem.Data.Interfaces;

    public class ZmstProgramDirector : IZmstProgramDirector
    {

        private const string ConnectionString = "ConnectionStrings:OnBoardingSystem";
        private readonly MailService _mailSettings;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UtilityService utilityService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ExcelUtilityServices _excelUtilityService;

        public class dupRows
        {
            public string rows { get; set; }
        }
        public ZmstProgramDirector(IUnitOfWork _unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper _mapper, ExcelUtilityServices excelUtilityService, JWTTokenService _JWTTokenService)//, IAppOnboardingAdminloginDirector _AppAdminLoginDirector)
        {
            _httpContextAccessor = httpContextAccessor;
            _excelUtilityService = excelUtilityService;
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public List<ManageProgramModel> ProgramExcel(ExcelBase64 excelBase64)
        {
            int Flag = 0;
            bool isError;
            string Message;
            DataSet ds;
            List<ManageProgramModel> sheetDataValidatefinal = new List<ManageProgramModel>();
            List<ManageProgramModel> sheetData = new List<ManageProgramModel>();
            byte[] file;
            file = Convert.FromBase64String(excelBase64.ExcelString);
            int nFileLen = file.Length;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Stream stream = new MemoryStream(file);

            using (var package = new ExcelPackage(stream))
            {
                List<string> dupRows = new List<string>();
                package.Load(stream);
                var vs = package.Workbook.Worksheets.FirstOrDefault();
                var rowData = vs.Dimension.Rows;
                for (int row = 2; row <= rowData; row++)
                {
                    sheetData.Add(new ManageProgramModel
                    {
                        SerialNo = (row - 1).ToString(),
                        Name = (vs.Cells[row, 1].Value ?? string.Empty).ToString().Trim(),
                        Shift = (vs.Cells[row, 2].Value ?? string.Empty).ToString().Trim(),
                        TFW = (vs.Cells[row, 3].Value ?? string.Empty).ToString().Trim(),
                        agencyId = (vs.Cells[row, 4].Value ?? string.Empty).ToString().Trim(),
                        parent = (vs.Cells[row, 5].Value ?? string.Empty).ToString().Trim(),
                        Description = "",
                        RowNumber = "",
                        Code = "",
                    });
                }
                int z = 0;
                int i = 0;
                foreach (var dr in sheetData)
                {
                    i = i + 1;
                    if (dr.Shift.ToString() != "N" && dr.Shift.ToString() != "1" && dr.Shift.ToString() != "2")
                    {
                        dr.RowNumber = (i).ToString();
                        dr.Description = dr.Description.ToString() + " Shift is not valid";
                        Flag = 1;
                    }
                    if (dr.TFW.ToString() != "N" && dr.TFW.ToString() != "T")
                    {
                        dr.RowNumber = (i).ToString();
                        dr.Description = dr.Description.ToString() + "TFW is not valid";
                        Flag = 1;
                    }
                    if (dr.Shift.ToString() != "N" && dr.Shift.ToString() != "1" && dr.Shift.ToString() != "2" && dr.TFW.ToString() != "N" && dr.TFW.ToString() != "T")
                    {
                        dr.RowNumber = (i).ToString();
                        dr.Description = dr.Description.ToString() + "Shift and TFW is not valid";
                        Flag = 1;
                    }
                    if (dr.Name.ToString().ToUpper().Contains("TFW") && dr.TFW.ToString() != "T")
                    {
                        dr.RowNumber = (i).ToString();
                        dr.Description = dr.Description.ToString() + "TWF must be 'T'";
                        Flag = 1;
                    }
                    if ((dr.Name == "") && (dr.Shift == "") && (dr.TFW == "") && (dr.agencyId == "") && (dr.parent == ""))
                    {
                        dr.RowNumber = (i).ToString();
                        dr.Description = "This row is Null";
                        Flag = 1;
                    }
                    if ((hasSpecialChar(dr.Name)) || (hasSpecialChar(dr.Shift)) || (hasSpecialChar(dr.TFW)) || (hasSpecialChar(dr.agencyId)) || (hasSpecialChar(dr.parent)))
                    {
                        dr.RowNumber = (i).ToString();
                        dr.Description = dr.Description.ToString() + "This row has Special Character ";
                        Flag = 1;
                    }
                    z++;
                }

                var duprecords = from s in sheetData
                                 select new ManageProgramModel
                                 {
                                     Name = s.Name,
                                     Shift = s.Shift,
                                     TFW = s.TFW,
                                     agencyId = s.agencyId,
                                     parent = s.parent,

                                 };
                var distintGroupby = duprecords.GroupBy(x => new { x.Name, x.TFW, x.Shift, x.agencyId, x.parent }).Where(g => g.Count() > 1)
                .Select(y => y.First())
                .ToList();
                if (distintGroupby.Count != 0)
                {
                    Flag = 1;
                    foreach (var dupdata in distintGroupby)
                    {
                        foreach (var drSheet in sheetData)
                        {
                            if (drSheet.Name == dupdata.Name &&
                            drSheet.TFW == dupdata.TFW &&
                            drSheet.agencyId == dupdata.agencyId &&
                            drSheet.Shift == dupdata.Shift &&
                            drSheet.parent == dupdata.parent)
                            {
                                drSheet.RowNumber = drSheet.SerialNo;
                                drSheet.Description = drSheet.Description + " This Data Is Duplicate";
                            }
                        }
                    }
                }
            }
            if (Flag == 1)
            {
                sheetData[0].Message = "Has Error";
                return sheetData;

            }
            if (Flag == 0)
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    DataTable dataTable = new DataTable(typeof(ManageProgramModel).Name);
                    PropertyInfo[] Props = typeof(ManageProgramModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in Props)
                    {
                        dataTable.Columns.Add(prop.Name);
                    }
                    foreach (ManageProgramModel item in sheetData)
                    {
                        var values = new object[Props.Length];
                        for (int L = 0; L < Props.Length; L++)
                        {
                            values[L] = Props[L].GetValue(item, null);
                        }
                        dataTable.Rows.Add(values);
                    }

                    var ConnectionStrings = _httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IConfiguration>();

                    var webConfigConnectionString = ConnectionStrings.GetValue<string>(ConnectionString);
                    conn.ConnectionString = webConfigConnectionString;
                    conn.Open();
                    SqlBulkCopy objbulk = new SqlBulkCopy(webConfigConnectionString);
                    string tableName = "ExcelData_" + Guid.NewGuid().ToString().Replace("-", "") + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    objbulk.DestinationTableName = tableName;
                    string query;
                    query = @"CREATE TABLE [dbo].[{0}](
                                [Name] [varchar](500) NULL,
                                [Shift] [varchar](50) NULL,
                                [TFW] [varchar](50) NULL,
                                [AgencyId] [varchar](50) NULL,
                                [parent] [varchar](500) NULL
                                )";
                    query = string.Format(query, tableName);

                    SqlCommand sqlcmd = new SqlCommand(query, conn);
                    sqlcmd.ExecuteNonQuery();
                    dataTable.Columns.Remove("SerialNo");
                    dataTable.Columns.Remove("RowNumber");
                    dataTable.Columns.Remove("Description");
                    dataTable.Columns.Remove("Code");
                    dataTable.Columns.Remove("AgencyName");
                    dataTable.Columns.Remove("Message");
                    objbulk.WriteToServer(dataTable);
                    SqlParameter[] sqlparam = new SqlParameter[4];
                    sqlparam[0] = new SqlParameter("@tableName", tableName);
                    sqlparam[1] = new SqlParameter("@Mode", excelBase64.Mode);
                    sqlparam[2] = new SqlParameter("@Message", SqlDbType.VarChar, 50);
                    sqlparam[2].Direction = ParameterDirection.Output;
                    sqlparam[2].Size = 1;
                    sqlparam[3] = new SqlParameter("@isError", SqlDbType.Bit, 1);
                    sqlparam[3].Direction = ParameterDirection.Output;
                    sqlparam[3].Size = 50;
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = "EXEC " + "usp_ImportPrograms @tableName,@Mode,@isError output,@Message output";

                    command.Parameters.Add(new SqlParameter(sqlparam[0].ParameterName, sqlparam[0].Value));
                    command.Parameters.Add(new SqlParameter(sqlparam[1].ParameterName, sqlparam[1].Value));
                    command.Parameters.Add(new SqlParameter(sqlparam[2].ParameterName, SqlDbType.Bit, 1));
                    command.Parameters[sqlparam[2].ParameterName].Direction = ParameterDirection.Output;
                    command.Parameters.Add(new SqlParameter(sqlparam[3].ParameterName, SqlDbType.Bit, 1));
                    command.Parameters[sqlparam[3].ParameterName].Direction = ParameterDirection.Output;

                    List<ManageProgramModel> sheetDataValidate = new List<ManageProgramModel>();
                    using (var reader = command.ExecuteReader())
                    {
                        if (excelBase64.Mode == "V")
                        {
                            while (reader.Read())
                            {
                                sheetDataValidate.Add(new Abstractions.Models.ManageProgramModel
                                {
                                    //reader.IsDBNull(reader.GetOrdinal("SA")) ? 0 : reader.GetInt32(reader.GetOrdinal("SA"))
                                    Name = reader.GetString(reader.GetOrdinal("Name")),
                                    Shift = reader.GetString(reader.GetOrdinal("Shift")),
                                    TFW = reader.GetString(reader.GetOrdinal("TFW")),
                                    agencyId = reader.GetString(reader.GetOrdinal("agencyId")),
                                    parent = reader.GetString(reader.GetOrdinal("parent")),
                                    Code = reader.GetString(reader.GetOrdinal("Code")),
                                    AgencyName = reader.GetString(reader.GetOrdinal("AgencyName")),
                                    Description = reader.IsDBNull(reader.GetOrdinal("Description")) ? "" : reader.GetString(reader.GetOrdinal("Description")),
                                });
                            }
                            reader.NextResult();
                            while (reader.Read())
                            {
                                isError = reader.GetBoolean(0);
                                Message = reader.GetString(1);
                                if (Message == "Duplicate data found.")
                                {
                                    sheetDataValidate[0].Message = "Has Error";
                                }
                                else
                                {
                                    sheetDataValidate[0].Message = Message;
                                }
                            }
                        }
                        sheetDataValidatefinal = sheetDataValidate;
                        if (excelBase64.Mode == "S")
                        {
                            while (reader.Read())
                            {
                                isError = reader.GetBoolean(0);
                                Message = reader.GetString(1);
                                if (Message == "Duplicate data found.")
                                {
                                    sheetDataValidate.Add(new Abstractions.Models.ManageProgramModel
                                    {
                                        //reader.IsDBNull(reader.GetOrdinal("SA")) ? 0 : reader.GetInt32(reader.GetOrdinal("SA"))
                                        Name = "",
                                        Shift = "",
                                        TFW = "",
                                        agencyId = "",
                                        parent = "",
                                        Code = "",
                                        AgencyName = "",
                                        Description = "",
                                        Message = "Has Error",
                                    });
                                    //sheetDataValidate[0].
                                }
                                else
                                {
                                    sheetDataValidate.Add(new Abstractions.Models.ManageProgramModel
                                    {
                                        //reader.IsDBNull(reader.GetOrdinal("SA")) ? 0 : reader.GetInt32(reader.GetOrdinal("SA"))
                                        Name = "",
                                        Shift = "",
                                        TFW = "",
                                        agencyId = "",
                                        parent = "",
                                        Code = "",
                                        AgencyName = "",
                                        Description = "",
                                        Message = Message,
                                    });
                                }
                            }
                        }
                    }
                }
            }

            string[] dupserialNo = null;
            int k = 0;
            var data = sheetData;
            return sheetDataValidatefinal;
        }
        public DataTable CreateDataTableForUpload()
        {

            DataTable Entry = new DataTable();
            Entry.Columns.Add("Name", typeof(string));
            Entry.Columns.Add("Shift", typeof(string));
            Entry.Columns.Add("TFW", typeof(string));
            Entry.Columns.Add("agencyId", typeof(string));
            Entry.Columns.Add("parent", typeof(string));

            DataRow dr = null;
            dr = Entry.NewRow();
            dr["Name"] = "Civil Engineering ";
            dr["Shift"] = "N";
            dr["TFW"] = "N";
            dr["agencyId"] = "124";
            dr["parent"] = "";
            Entry.Rows.Add(dr);
            dr = Entry.NewRow();
            dr["Name"] = "Civil Engineering (Morning Shift)";
            dr["Shift"] = "1";
            dr["TFW"] = "N";
            dr["agencyId"] = "124";
            dr["parent"] = "";
            Entry.Rows.Add(dr);
            dr = Entry.NewRow();
            dr["Name"] = "Civil Engineering (Afternoon Shift)";
            dr["Shift"] = "2";
            dr["TFW"] = "N";
            dr["agencyId"] = "124";
            dr["parent"] = "";
            Entry.Rows.Add(dr);
            dr = Entry.NewRow();
            dr["Name"] = "Civil Engineering";
            dr["Shift"] = "N";
            dr["TFW"] = "N";
            dr["agencyId"] = "118";
            dr["parent"] = "";
            Entry.Rows.Add(dr);
            dr = Entry.NewRow();
            dr["Name"] = "Civil Engineering (Morning Shift)-TFW";
            dr["Shift"] = "1";
            dr["TFW"] = "T";
            dr["agencyId"] = "124";
            dr["parent"] = "";
            Entry.Rows.Add(dr);
            dr = Entry.NewRow();
            dr["Name"] = "Civil Engineering(Afternoon Shift) - TFW";
            dr["Shift"] = "2";
            dr["TFW"] = "T";
            dr["agencyId"] = "124";
            dr["parent"] = "";
            Entry.Rows.Add(dr);
            dr = Entry.NewRow();
            dr["Name"] = "Civil Engineering-TFW";
            dr["Shift"] = "N";
            dr["TFW"] = "T";
            dr["agencyId"] = "124";
            dr["parent"] = "";
            Entry.Rows.Add(dr);

            return Entry;
        }
        public static bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%/=?»«@£§€{}.;'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }

        public string DownloadExcel()
        {
            try
            {
                string imgBase64Str = "";
                DataTable ResultDt = CreateDataTableForUpload();
                MemoryStream stream = this._excelUtilityService.GetExcel(ResultDt);
                byte[] imageArray = stream.ToArray();
                imgBase64Str = Convert.ToBase64String(imageArray);
                return imgBase64Str;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<List<Abstractions.Models.ZmstProgram>> GetAll(CancellationToken cancellationToken)
        {
            //var response=await unitOfWork.ZmstProgramRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
            var programList = unitOfWork.ZmstProgramRepository.GetAll();
            var mdAgencyList = unitOfWork.MDAgencyRepository.GetAll();
            var result = from programs in programList
                         join agency in mdAgencyList on programs.Agencyid equals agency.AgencyId.ToString()
                         select new ZmstProgram
                         {
                             Id = programs.Id,
                             Brcd = programs.Brcd,
                             Brnm = programs.Brnm,
                             Agencyid = programs.Agencyid,
                             AgencyName = agency.AgencyName,
                             BrcdOrg = programs.BrcdOrg,
                             Bshift = programs.Bshift,
                             Btfw = programs.Btfw,
                         };
            return this.mapper.Map<List<Abstractions.Models.ZmstProgram>>(result);
        }
    }
}
