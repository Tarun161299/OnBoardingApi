
namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using DocumentFormat.OpenXml.Office2010.Excel;
    using DocumentFormat.OpenXml.Spreadsheet;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Interfaces;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;
    public class AdministratorDirector : IAdministratorDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AdministratorDirector(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var entity = await this.unitOfWork.AdministratorRepository.FindByAsync(x => x.UserId == id, cancellationToken).ConfigureAwait(false);

            await this.unitOfWork.AdministratorRepository.DeleteAsync(entity, cancellationToken).ConfigureAwait(false);
            return await this.unitOfWork.AdministratorRepository.CommitAsync(cancellationToken).ConfigureAwait(false);

        }

        public virtual async Task<List<AbsModels.Administratordetails>> GetAllAsync(CancellationToken cancellationToken)
        {
            var userlist = this.unitOfWork.AdministratorRepository.GetAll();
            var userlistzmstmode = this.unitOfWork.ZmstAuthenticationModeRepository.GetAll();
            var userlistzmstsequrityq = this.unitOfWork.ZmstSecurityQuestionRepository.GetAll();

            var query = from user in userlist
                        join userzmstsequrity in userlistzmstsequrityq on user.SecurityQuesId equals userzmstsequrity.SecurityQuesId 
                        join userlistzmst in userlistzmstmode on user.AuthMode equals userlistzmst.AuthCode into temp
                        from m in temp.DefaultIfEmpty()
                        select new AbsModels.Administratordetails
                        {
                             
                              UserName = user.UserName,
                              Designation = user.Designation,
                              EmailId = user.EmailId,
                              MobileNo = user.MobileNo,
                              UserId = user.UserId,
                              PasswordHash = user.PasswordHash,
                              SecurityQuesId = user.SecurityQuesId,
                              SecurityQuesdesc = userzmstsequrity.SecurityQues,
                              SecurityAns = user.SecurityAns,
                              AuthMode = user.AuthMode,
                              AuthModedesc = m.Authmode,
                          
                       };

            return query.ToList();
        }

        public async Task<int> InsertAsync(Administrator administrator, CancellationToken cancellationToken)
            {
            // var savedetails = new OnBoardingSystem.Data.Abstractions.Models.Administrator();
            //savedetails.UserName = administrator.UserName;
            //savedetails.Designation = administrator.Designation;
            //savedetails.EmailId = administrator.EmailId;
            //savedetails.MobileNo = administrator.MobileNo;
            //savedetails.UserId = administrator.UserId;
            //savedetails.PasswordHash = administrator.PasswordHash;
            //savedetails.SecurityQuesId = administrator.SecurityQuesId;
            //savedetails.SecurityAns = administrator.SecurityAns;
            //savedetails.AuthMode = administrator.AuthMode;
            //savedetails.Photopath = administrator.Photopath;
            try
            {
                var Savedetails = this.mapper.Map<Data.EF.Models.Administrator>(administrator);
                await this.unitOfWork.AdministratorRepository.InsertAsync(Savedetails, cancellationToken).ConfigureAwait(false);
                return await this.unitOfWork.CommitAsync(cancellationToken);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<int> UpdateAsync(string id,Administrator administrator, CancellationToken cancellationToken)
        {
            var entity = await this.unitOfWork.AdministratorRepository.FindByAsync(x => x.UserId == id, cancellationToken).ConfigureAwait(false);
            if (entity != null)
            {
                entity.UserName = administrator.UserName;
                entity.Designation = administrator.Designation;
                entity.EmailId = administrator.EmailId;
                entity.MobileNo = administrator.MobileNo;
             
                entity.PasswordHash = administrator.PasswordHash;
                entity.SecurityQuesId = administrator.SecurityQuesId;
                entity.SecurityAns = entity.SecurityAns;
                entity.AuthMode = administrator.AuthMode;
                entity.Photopath = administrator.Photopath;

            }

            await this.unitOfWork.AdministratorRepository.UpdateAsync(entity, cancellationToken).ConfigureAwait(false);
            return await unitOfWork.CommitAsync(cancellationToken);
        }

        public virtual async Task<AbsModels.Administrator> GetDocumentByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var documentlist = await this.unitOfWork.AdministratorRepository.FindByAsync(x => x.UserId == id, cancellationToken).ConfigureAwait(false);
                var result = this.mapper.Map<Abstractions.Models.Administrator>(documentlist);
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       
    }
}
