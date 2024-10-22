//-----------------------------------------------------------------------
// <copyright file="IMailServiceDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Data.Abstractions.Behaviors
{
    using OnBoardingSystem.Data.Abstractions.Models;
    public interface IZmstProgramDirector
    {
        List<ManageProgramModel> ProgramExcel(ExcelBase64 excelBase64);
        string DownloadExcel();

        Task<List<ZmstProgram>> GetAll(CancellationToken cancellationToken);
    }
}
