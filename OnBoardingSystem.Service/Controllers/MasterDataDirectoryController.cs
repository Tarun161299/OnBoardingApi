namespace OnBoardingSystem.Service.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Behaviors;

    public class MasterDataDirectoryController : ControllerBase
    {
        private readonly IZmstGenderDirector zmstgenderDirector;
        private readonly IZmstInstituteDirector zmstinstituteDirector;
        private readonly IZmstServiceTypeDirector izmstservicetype;
        private readonly IZmstAgencyDirector zmstagencyDirector;
        private readonly IZmstProjectsDirector iZmstProjectsDirector;
        private readonly IZmstCategoryDirector zmstcategoryDirector;
        private readonly IZmstIdentityTypeDirector zmstidentitytypeDirector;
        private readonly IZmstReligionDirector zmstreligionDirector;
        private readonly IZmstExamTypeDirector zmstexamtypeDirector;
        private readonly IZmstInstituteTypeDirector zmstinstitutetypeDirector;
        private readonly IZmstSpecialExamPaperDirector zmstspecialexampaperDirector;
        private readonly IZmstProgramDirector iManageProgramDirector;
        private readonly IZmstQualificationDirector zmstqualificationDirector;
        private readonly IZmstSubjectDirector iZmstSubjectDirector;
        private readonly IZmstQualifyingExamBoardDirector zmstqualifyingexamboardDirector;
        private readonly IZmstQualifyingCourseDirector zmstqualifyingcourseDirector;
        private readonly IZmstExperienceTypeDirector zmstexperiencetypeDirector;
        private readonly IZmstDocumentTypeDirector zmstdocumenttypeDirector;
        private readonly IZmstWillingnessDirector iZmstWillingnessDirector;
        private readonly IZmstSymbolDirector zmstsymbolDirector;
        private readonly IZmstStreamDirector zmststreamDirector;
        private readonly IZmstRankTypeDirector zmstranktypeDirector;
        private readonly IZmstQuotaDirector zmstquotaDirector;
        private readonly IZmstSeatCategoryDirector zmstseatcategoryDirector;
        private readonly IZmstSubCategoryPriorityDirector zmstsubcategorypriorityDirector;
        private readonly IZmstTypeofDisabilityDirector zmsttypeofdisabilityDirector;
        private readonly IZmstSubCategoryDirector zmstsubcategoryDirector;
        private readonly IZmstDistrictDirector zmstdistrictDirector;
        private readonly IZmstNationalityDirector zmstnationalityDirector;
        private readonly IZmstCountryDirector zmstcountryDirector;
        private readonly IZmstServiceTypeDirector zmstservicetype;
        private readonly IZmstStateDirector zmststateDirector;
        private readonly IZmstMinimumQualificationDirector zmstminimumqualificationDirector;
        private readonly IZmstPassingStatusDirector zmstpassingstatusDirector;
        private readonly IZmstQualifyingExamResultModeDirector zmstqualifyingexamresultmodeDirector;
        private readonly IZmstQualifyingExamLearningModeDirector zmstqualifyingexamlearningmodeDirector;
        private readonly IZmstSeatGroupDirector zmstseatgroupDirector;
        private readonly IZmstSeatSubCategoryDirector zmstseatsubcategoryDirector;
        private readonly IZmstSeatTypeDirector zmstseattypeDirector;
        private readonly IZmstBranchDirector zmstbranchDirector;
        private readonly IMdSmsEmailTemplateDirector mdsmsemailtemplateDirector;
        private readonly IApplicationSummaryDirector iApplicationSummaryDirector;
         private readonly IMapper mapper;

        public MasterDataDirectoryController(
            IZmstGenderDirector _zmstgenderDirector,
            IZmstInstituteDirector zmstinstituteDirector,
            IZmstServiceTypeDirector iZmstServiceTypeDirector,
            IZmstInstituteTypeDirector zmstinstitutetypeDirector,
            IZmstSpecialExamPaperDirector zmstspecialexampaperDirector,
            IZmstExamTypeDirector zmstexamtypeDirector,
            IZmstProgramDirector _iManageProgramDirector,
            IZmstQualificationDirector zmstqualificationDirector,
            IZmstSubjectDirector IZmstSubjectDirector,
            IZmstQualifyingExamBoardDirector zmstqualifyingexamboardDirector,
            IZmstQualifyingCourseDirector zmstqualifyingcourseDirector,
            IZmstAgencyDirector zmstagencyDirector,
            IZmstProjectsDirector _iZmstProjectsDirector,
            IZmstCategoryDirector zmstcategoryDirector,
            IZmstIdentityTypeDirector zmstidentitytypeDirector,
            IZmstReligionDirector zmstreligionDirector,
            IZmstDocumentTypeDirector zmstdocumenttypeDirector,
            IZmstWillingnessDirector iZmstWillingnessDirector,
            IZmstSymbolDirector zmstsymbolDirector,
            IZmstStreamDirector zmststreamDirector,
            IZmstRankTypeDirector zmstranktypeDirector,
            IZmstQuotaDirector zmstquotaDirector,
            IZmstSeatCategoryDirector zmstseatcategoryDirector,
            IZmstSubCategoryPriorityDirector zmstsubcategorypriorityDirector,
            IZmstTypeofDisabilityDirector zmsttypeofdisabilityDirector,
            IZmstSubCategoryDirector zmstsubcategoryDirector,
            IZmstDistrictDirector zmstdistrictDirector,
            IZmstNationalityDirector zmstnationalityDirector,
            IZmstCountryDirector zmstcountryDirector,
            IZmstServiceTypeDirector ZmstServiceTypeDirector,
            IZmstStateDirector zmststateDirector,
            IZmstMinimumQualificationDirector zmstMinimumQualificationDirector,
            IZmstPassingStatusDirector zmstpassingstatusDirector,
            IZmstQualifyingExamResultModeDirector zmstqualifyingexamresultmodeDirector,
            IZmstQualifyingExamLearningModeDirector zmstqualifyingexamlearningmodeDirector,
            IZmstSeatGroupDirector zmstseatgroupDirector,
            IZmstSeatSubCategoryDirector zmstseatsubcategoryDirector,
            IZmstSeatTypeDirector zmstseattypeDirector,
            IZmstBranchDirector zmstbranchDirector,
            IMdSmsEmailTemplateDirector mdsmsemailtemplateDirector,
            IZmstExperienceTypeDirector zmstexperiencetypeDirector,
            IApplicationSummaryDirector _iApplicationSummaryDirector,
            IMapper mapper
            )
        {
            this.zmstgenderDirector = _zmstgenderDirector;
            this.zmstinstituteDirector = zmstinstituteDirector;
            this.izmstservicetype = iZmstServiceTypeDirector;
            this.zmstagencyDirector = zmstagencyDirector;
            this.iZmstProjectsDirector = _iZmstProjectsDirector;
            this.zmstcategoryDirector = zmstcategoryDirector;
            this.zmstidentitytypeDirector = zmstidentitytypeDirector;
            this.zmstreligionDirector = zmstreligionDirector;
            this.zmstexamtypeDirector = zmstexamtypeDirector;
            this.zmstinstitutetypeDirector = zmstinstitutetypeDirector;
            this.zmstspecialexampaperDirector = zmstspecialexampaperDirector;
            this.zmstexamtypeDirector = zmstexamtypeDirector;
            this.iManageProgramDirector = _iManageProgramDirector;
            this.zmstqualificationDirector = zmstqualificationDirector;
            this.iZmstSubjectDirector = IZmstSubjectDirector;
            this.zmstqualifyingexamboardDirector = zmstqualifyingexamboardDirector;
            this.zmstqualifyingcourseDirector = zmstqualifyingcourseDirector;
            this.zmstdocumenttypeDirector = zmstdocumenttypeDirector;
            this.iZmstWillingnessDirector = iZmstWillingnessDirector;
            this.zmstsymbolDirector = zmstsymbolDirector;
            this.zmststreamDirector = zmststreamDirector;
            this.zmstranktypeDirector = zmstranktypeDirector;
            this.zmstquotaDirector = zmstquotaDirector;
            this.zmstseatcategoryDirector = zmstseatcategoryDirector;
            this.zmstsubcategorypriorityDirector = zmstsubcategorypriorityDirector;
            this.zmsttypeofdisabilityDirector = zmsttypeofdisabilityDirector;
            this.zmstsubcategoryDirector = zmstsubcategoryDirector;
            this.zmstdistrictDirector = zmstdistrictDirector;
            this.zmstnationalityDirector = zmstnationalityDirector;
            this.zmstcountryDirector = zmstcountryDirector;
            this.zmstservicetype = ZmstServiceTypeDirector;
            this.zmststateDirector = zmststateDirector;
            this.zmstminimumqualificationDirector = zmstMinimumQualificationDirector;
            this.zmstpassingstatusDirector = zmstpassingstatusDirector;
            this.zmstqualifyingexamresultmodeDirector = zmstqualifyingexamresultmodeDirector;
            this.zmstqualifyingexamlearningmodeDirector = zmstqualifyingexamlearningmodeDirector;
            this.zmstseatgroupDirector = zmstseatgroupDirector;
            this.zmstseatsubcategoryDirector = zmstseatsubcategoryDirector;
            this.zmstseattypeDirector = zmstseattypeDirector;
            this.zmstbranchDirector = zmstbranchDirector;
            this.mdsmsemailtemplateDirector = mdsmsemailtemplateDirector;
            this.zmstexperiencetypeDirector = zmstexperiencetypeDirector;
            this.iApplicationSummaryDirector = _iApplicationSummaryDirector;
            this.mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllGenderListAsync()
        {
            List<ZmstGender> genderData = new List<ZmstGender>();
            genderData = await zmstgenderDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var List = from gender in genderData
                       select new MasterDataDirectory
                       {
                           code = gender.GenderId,
                           value = gender.GenderName,
                           alternateNames = gender.AlternateNames,
                       };
            return List.ToList();

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllInstituteListAsync()
        {
            List<ZmstInstitute> instList = new List<ZmstInstitute>();
            instList = await this.zmstinstituteDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var List = from inst in instList
                       select new MasterDataDirectory
                       {
                           code = inst.InstCd,
                           value = inst.InstNm,
                           InstTypeId = inst.InstTypeId,
                       };
            return List.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllServiceTypeAsync()
        {
            List<ZmstServiceType> zmstServiceType = new List<Data.Abstractions.Models.ZmstServiceType>();
            zmstServiceType = await this.izmstservicetype.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var List = from service in zmstServiceType
                       select new MasterDataDirectory
                       {
                           code = service.ServiceTypeId,
                           value = service.ServiceTypeName,
                       };
            return List.ToList();
        }

        /// <summary>
        /// Get ZmstInstituteType List.
        /// </summary>
        /// <returns>Get All ZmstInstituteType List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllInstituteTypeAsync()
        {
            List<ZmstInstituteType> instType = new List<ZmstInstituteType>();
            instType = await this.zmstinstitutetypeDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var List = from inst in instType
                       select new MasterDataDirectory
                       {
                           code = inst.InstituteTypeId,
                           value = inst.InstituteType,
                           priority = inst.Priority.ToString(),
                       };
            return List.ToList();

        }

        /// <summary>
        /// Get ZmstSpecialExamPaper List.
        /// </summary>
        /// <returns>Get All ZmstSpecialExamPaper List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ZmstSpecialExamPaper), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ZmstSpecialExamPaper), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllSpecialExamAsync()
        {
            List<Data.Abstractions.Models.ZmstSpecialExamPaper> specialExamType = new List<Data.Abstractions.Models.ZmstSpecialExamPaper>();
            specialExamType = await zmstspecialexampaperDirector.GetAllAsync(default).ConfigureAwait(false);
            var List = from specialExam in specialExamType
                       select new MasterDataDirectory
                       {
                           code = specialExam.Id,
                           value = specialExam.Description,
                           specialexamId = specialExam.SpecialExamId,
                       };
            return List.ToList();
        }

        /// <summary>
        /// Get ZmstExamType List.
        /// </summary>
        /// <returns>Get All ZmstExamType List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllExamTypeAsync()
        {
            List<ZmstExamType> examType = new List<ZmstExamType>();
            examType = await zmstexamtypeDirector.GetAllAsync(default).ConfigureAwait(false);
            var List = from exam in examType
                       select new MasterDataDirectory
                       {
                           code = exam.Id,
                           value = exam.Description
                       };
            return List.ToList();
        }

        /// <summary>
        /// Get ZmstBranch List.
        /// </summary>
        /// <returns>Get All ZmstBranch List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllProgramAsync()
        {
            List<ZmstProgram> zmstProgram = new List<ZmstProgram>();
            zmstProgram = await iManageProgramDirector.GetAll(default).ConfigureAwait(false);
            var List = from program in zmstProgram
                       select new MasterDataDirectory
                       {
                           code = program.Brcd,
                           value = program.Brnm,
                           agencyId = program.Agencyid
                       };
            return List.ToList();
        }

        /// <summary>
        /// Get ZmstQualification List.
        /// </summary>
        /// <returns>Get All ZmstQualification List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ZmstQualification), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ZmstQualification), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllQualificationAsync()
        {
            List<ZmstQualification> zmstQualification = new List<ZmstQualification>();
            zmstQualification = await zmstqualificationDirector.GetAllAsync(default).ConfigureAwait(false);
            var list = from qalification in zmstQualification
                       select new MasterDataDirectory
                       {
                           code = qalification.QualificationId,
                           value = qalification.Name,
                           alternateNames = qalification.AlternateNames
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstServiceType List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ZmstSubject), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ZmstSubject), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllSubjectsAsync()
        {
            List<ZmstSubject> zmstSubject = new List<ZmstSubject>();
            zmstSubject = await iZmstSubjectDirector.GetAllAsync(default).ConfigureAwait(false);
            var list = from subject in zmstSubject
                       select new MasterDataDirectory
                       {
                           code = subject.subjectId,
                           value = subject.subjectName,
                           alternateNames = subject.alternateNames,
                           qualificationId = subject.qualificationId
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstQualifyingExamBoard List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingExamBoard List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ZmstQualifyingExamBoard), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ZmstQualifyingExamBoard), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllQualifyingExamBoardAsync()
        {
            List<ZmstQualifyingExamBoard> zmstQualifyingExamBoard = new List<ZmstQualifyingExamBoard>();
            zmstQualifyingExamBoard = await zmstqualifyingexamboardDirector.GetAllAsync(default).ConfigureAwait(false);
            var list = from qualifyingExamBoard in zmstQualifyingExamBoard
                       select new MasterDataDirectory
                       {
                           code = qualifyingExamBoard.Id,
                           value = qualifyingExamBoard.Description,
                           qualificationId = qualifyingExamBoard.QualificationId
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstQualifyingCourse List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingCourse List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ZmstQualifyingCourse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ZmstQualifyingCourse), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]

        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllQualifyingCourseAsync()
        {
            List<ZmstQualifyingCourse> zmstQualifyingCourse = new List<ZmstQualifyingCourse>();
            zmstQualifyingCourse = await zmstqualifyingcourseDirector.GetAllAsync(default).ConfigureAwait(false);
            var list = from qualifyingCourse in zmstQualifyingCourse
                       select new MasterDataDirectory
                       {
                           code = qualifyingCourse.QualificationCourseId,
                           value = qualifyingCourse.QualificationCourseName,
                           qualificationId = qualifyingCourse.QualificationCourseId
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstServiceType List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllZmstAgencyAsync()
        {
            List<ZmstAgency> zmstAgency = new List<ZmstAgency>();
            zmstAgency = await zmstagencyDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from agency in zmstAgency
                       select new MasterDataDirectory
                       {
                           code = agency.AgencyId,
                           AgencyAbbr = agency.AgencyAbbr,
                           value = agency.AgencyName,
                           AgencyType = agency.AgencyType,
                           StateId = agency.StateId,
                           ServiceTypeId = agency.ServiceTypeId,
                           address = agency.Address,
                           isActive = agency.IsActive,
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstProjects List.
        /// </summary>
        /// <returns>Get All ZmstProjects List.</returns>            
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ZmstProjects), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ZmstProjects), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllZmstProjectsAsync()
        {
            List<ZmstProjects> zmstProjects = new List<ZmstProjects>();
            zmstProjects = await iZmstProjectsDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from projects in zmstProjects
                       select new MasterDataDirectory
                       {
                           code = projects.ProjectId.ToString(),
                           value = projects.ProjectName,
                           agencyId = projects.AgencyId.ToString(),
                           ServiceTypeId = projects.ServiceType.ToString(),
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstCategory List.
        /// </summary>
        /// <returns>Get All ZmstCategory List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ZmstCategory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ZmstCategory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllZmstCategoryAsync()
        {
            List<ZmstCategory> zmstCategory = new List<ZmstCategory>();
            zmstCategory = await zmstcategoryDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from category in zmstCategory
                       select new MasterDataDirectory
                       {
                           code = category.CategoryId,
                           value = category.CategoryName,
                           alternateNames = category.AlternateNames,
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstIdentityType List.
        /// </summary>
        /// <returns>Get All ZmstIdentityType List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ZmstIdentityType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ZmstIdentityType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllZmstIdentityTypeAsync()
        {
            List<ZmstIdentityType> zmstIdentityType = new List<ZmstIdentityType>();
            zmstIdentityType = await zmstidentitytypeDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from identityType in zmstIdentityType
                       select new MasterDataDirectory
                       {
                           code = identityType.IdentityTypeId,
                           value = identityType.Description,
                           alternateNames = identityType.AlternateNames,
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstReligion List.
        /// </summary>
        /// <returns>Get All ZmstReligion List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ZmstReligion), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ZmstReligion), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllZmstReligionAsync()
        {
            List<ZmstReligion> zmstReligion = new List<ZmstReligion>();
            zmstReligion = await zmstreligionDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from religion in zmstReligion
                       select new MasterDataDirectory
                       {
                           code = religion.ReligionId,
                           value = religion.Description,
                           alternateNames = religion.AlternateNames,
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstExamType List.
        /// </summary>
        /// <returns>Get All ZmstExamType List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ZmstExamType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ZmstExamType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllZmstExamTypeAsync()
        {
            List<ZmstExamType> zmstExamType = new List<ZmstExamType>();
            zmstExamType = await zmstexamtypeDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from examType in zmstExamType
                       select new MasterDataDirectory
                       {
                           code = examType.Id,
                           value = examType.Description,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAlldocumenttypeAsync()
        {
            List<ZmstDocumentType> documenttype = new List<ZmstDocumentType>();
            documenttype = await zmstdocumenttypeDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from document in documenttype
                       select new MasterDataDirectory
                       {
                           code = document.DocumentTypeId,
                           value = document.Title,
                           alternateNames = document.AlternateNames,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncwillingness()
        {
            List<ZmstWillingness> willingness = new List<ZmstWillingness>();
            willingness = await iZmstWillingnessDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from willnesstype in willingness
                       select new MasterDataDirectory
                       {
                           code = willnesstype.WillingnessId,
                           value = willnesstype.Description,
                           alternateNames = willnesstype.AlternateNames,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncSymbol()
        {
            List<ZmstSymbol> symbolObj = new List<ZmstSymbol>();
            symbolObj = await zmstsymbolDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from symbolObjLinq in symbolObj
                       select new MasterDataDirectory
                       {
                           code = symbolObjLinq.SymbolId,
                           value = symbolObjLinq.Description,
                           alternateNames = symbolObjLinq.AlternateNames,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncStreamList()
        {
            List<ZmstStream> streamObj = new List<ZmstStream>();
            streamObj = await zmststreamDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from streamLinq in streamObj
                       select new MasterDataDirectory
                       {
                           code = streamLinq.StreamId,
                           value = streamLinq.StreamName,
                           alternateNames = streamLinq.AlternateNames,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncRankType()
        {
            List<ZmstRankType> rankTypeObj = new List<ZmstRankType>();
            rankTypeObj = await zmstranktypeDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from rankTypeLinq in rankTypeObj
                       select new MasterDataDirectory
                       {
                           code = rankTypeLinq.Id,
                           value = rankTypeLinq.Description,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncQuotaList()
        {
            List<ZmstQuota> QuotaObj = new List<ZmstQuota>();
            QuotaObj = await zmstquotaDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from QuotaLinq in QuotaObj
                       select new MasterDataDirectory
                       {
                           code = QuotaLinq.QuotaId,
                           value = QuotaLinq.Name,
                           alternateNames = QuotaLinq.AlternateNames,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncSeatCategoryList()
        {
            List<ZmstSeatCategory> SeatCategoryObj = new List<ZmstSeatCategory>();
            SeatCategoryObj = await zmstseatcategoryDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from SeatCategoryLinq in SeatCategoryObj
                       select new MasterDataDirectory
                       {
                           code = SeatCategoryLinq.SeatCategoryId,
                           value = SeatCategoryLinq.Description,
                           alternateNames = SeatCategoryLinq.AlternateNames,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncSubcategoryPriorityList()
        {
            List<ZmstSubCategoryPriority> SeatCategoryPriorityObj = new List<ZmstSubCategoryPriority>();
            SeatCategoryPriorityObj = await zmstsubcategorypriorityDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from SeatCategoryPriorityObjLinq in SeatCategoryPriorityObj
                       select new MasterDataDirectory
                       {
                           code = SeatCategoryPriorityObjLinq.SubCategoryPriorityId,
                           value = SeatCategoryPriorityObjLinq.Description,
                           alternateNames = SeatCategoryPriorityObjLinq.AlternateNames,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncTypeOfDisabilityList()
        {
            List<ZmstTypeofDisability> TypeofDisabilityObj = new List<ZmstTypeofDisability>();
            TypeofDisabilityObj = await zmsttypeofdisabilityDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from TypeofDisabilityLinq in TypeofDisabilityObj
                       select new MasterDataDirectory
                       {
                           code = TypeofDisabilityLinq.Id,
                           value = TypeofDisabilityLinq.Description,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncSubCategoryList()
        {
            List<ZmstSubCategory> SubCategoryObj = new List<ZmstSubCategory>();
            SubCategoryObj = await zmstsubcategoryDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from SubCategoryObjLinq in SubCategoryObj
                       select new MasterDataDirectory
                       {
                           code = SubCategoryObjLinq.SubCategoryId,
                           value = SubCategoryObjLinq.SubCategoryName,
                           alternateNames = SubCategoryObjLinq.AlternateNames
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncDistrict()
        {
            List<ZmstDistrict> districtObj = new List<ZmstDistrict>();
            districtObj = await zmstdistrictDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from districtObjLinq in districtObj
                       select new MasterDataDirectory
                       {
                           StateId = districtObjLinq.StateId,
                           code = districtObjLinq.DistrictId,
                           value = districtObjLinq.DistrictName,
                           alternateNames = districtObjLinq.AlternateNames
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncNationality()
        {
            List<ZmstNationality> nationalityObj = new List<ZmstNationality>();
            nationalityObj = await zmstnationalityDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from nationalityObjLinq in nationalityObj
                       select new MasterDataDirectory
                       {
                           code = nationalityObjLinq.NationalityId,
                           value = nationalityObjLinq.Description,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllAsyncCountry()
        {
            List<ZmstCountry> countryObj = new List<ZmstCountry>();
            countryObj = await zmstcountryDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from countryObjLinq in countryObj
                       select new MasterDataDirectory
                       {
                           code = countryObjLinq.Code,
                           value = countryObjLinq.Name,
                           isdCode = countryObjLinq.Isdcode,
                           SAARCCode = countryObjLinq.SAarccode,
                           SAARCName = countryObjLinq.SAarcname,
                           priority = countryObjLinq.Priority.ToString(),
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstState List.
        /// </summary>
        /// <returns>Get All ZmstState List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetStateAsync()
        {
            List<ZmstState> zmstState = new List<ZmstState>();
            zmstState = await zmststateDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from state in zmstState
                       select new MasterDataDirectory
                       {
                           code = state.StateId,
                           value = state.StateName,
                           alternateNames = state.AlternateNames,
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstMinimumQualification List.
        /// </summary>
        /// <returns>Get All ZmstMinimumQualification List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllMinimumQualificationAsync()
        {
            List<ZmstMinimumQualification> zmstminqual = new List<ZmstMinimumQualification>();
            zmstminqual = await zmstminimumqualificationDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from minqual in zmstminqual
                       select new MasterDataDirectory
                       {
                           code = minqual.MinimumQualId,
                           value = minqual.Description,
                           alternateNames = minqual.AlternateNames,
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstPassingStatus List.
        /// </summary>
        /// <returns>Get All ZmstPassingStatus List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        // [Route("GetAll")]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllPassingStatusAsync()
        {
            List<ZmstPassingStatus> passstatus = new List<ZmstPassingStatus>();
            passstatus = await zmstpassingstatusDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from passtatus in passstatus
                       select new MasterDataDirectory
                       {
                           code = passtatus.PassingStatusId,
                           value = passtatus.Description,
                       };
            return list.ToList();
        }
        /// <summary>
        /// Get ZmstQualifyingExamResultMode List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingExamResultMode List.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllQualifyingExamResultModeAsync()
        {
            List<ZmstQualifyingExamResultMode> qualifyingExamResultMode = new List<ZmstQualifyingExamResultMode>();
            qualifyingExamResultMode = await zmstqualifyingexamresultmodeDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from qualExamResultMode in qualifyingExamResultMode
                       select new MasterDataDirectory
                       {
                           code = qualExamResultMode.Id,
                           value = qualExamResultMode.Description,
                           alternateNames = qualExamResultMode.Alternatenames,
                       };
            return list.ToList();

        }
        /// <summary>
        /// Get ZmstQualifyingExamResultMode List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingExamResultMode List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllQualifyingExamLearningModeAsync()
        {
            List<ZmstQualifyingExamLearningMode> qualifyingExamLearningMode = new List<ZmstQualifyingExamLearningMode>();
            qualifyingExamLearningMode = await zmstqualifyingexamlearningmodeDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from qualifyingExamLearningModeLinq in qualifyingExamLearningMode
                       select new MasterDataDirectory
                       {
                           code = qualifyingExamLearningModeLinq.Id,
                           value = qualifyingExamLearningModeLinq.Description,
                           alternateNames = qualifyingExamLearningModeLinq.AlternateNames,
                       };
            return list.ToList();
        }

        /// <summary>
        /// Get ZmstQualifyingExamResultMode List.
        /// </summary>
        /// <returns>Get All ZmstQualifyingExamResultMode List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllseatgroupAsync()
        {
            List<ZmstSeatGroup> seatGroupObj = new List<ZmstSeatGroup>();
            seatGroupObj = await zmstseatgroupDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from seatGroupObjLinq in seatGroupObj
                       select new MasterDataDirectory
                       {
                           code = seatGroupObjLinq.Id,
                           value = seatGroupObjLinq.Description,
                           alternateNames = seatGroupObjLinq.AlternateNames,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllSeatSubCategory()
        {
            List<ZmstSeatSubCategory> seatSubCategoryObj = new List<ZmstSeatSubCategory>();
            seatSubCategoryObj = await zmstseatsubcategoryDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from seatSubCategoryObjLinq in seatSubCategoryObj
                       select new MasterDataDirectory
                       {
                           code = seatSubCategoryObjLinq.SeatSubCategoryId,
                           value = seatSubCategoryObjLinq.Description,
                           alternateNames = seatSubCategoryObjLinq.Alternatenames,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllSeatType()
        {
            List<ZmstSeatType> seatTypeObj = new List<ZmstSeatType>();
            seatTypeObj = await zmstseattypeDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from seatTypeObjLinq in seatTypeObj
                       select new MasterDataDirectory
                       {
                           code = seatTypeObjLinq.Id,
                           value = seatTypeObjLinq.Description,
                           alternateNames = seatTypeObjLinq.AlternateNames,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllBranchType()
        {
            List<ZmstProgram> branchObj = new List<ZmstProgram>();
            branchObj = await iManageProgramDirector.GetAll(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from branchObjLinq in branchObj
                       select new MasterDataDirectory
                       {
                           code = branchObjLinq.Brcd,
                           value = branchObjLinq.Brnm,
                           agencyId = branchObjLinq.Agencyid,
                           shift = branchObjLinq.Bshift,
                           tfw = branchObjLinq.Btfw,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllSmsEmailTemplate()
        {
            List<MdSmsEmailTemplate> smsEmailTemplateObj = new List<MdSmsEmailTemplate>();
            smsEmailTemplateObj = await mdsmsemailtemplateDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from smsEmailTemplateObjLinq in smsEmailTemplateObj
                       select new MasterDataDirectory
                       {
                           templateid = smsEmailTemplateObjLinq.TemplateId,
                           messagetemplate = smsEmailTemplateObjLinq.MessageTemplate,
                           messagetemplatetrai = smsEmailTemplateObjLinq.MessageTemplate,
                           description = smsEmailTemplateObjLinq.Description,
                           templatestatus = smsEmailTemplateObjLinq.TemplateId,
                       };
            return list.ToList();
        } 

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<MasterDataDirectory>>> GetAllExperienceType()
        {
            List<ZmstExperienceType> experienceTypeObj = new List<ZmstExperienceType>();
            experienceTypeObj = await zmstexperiencetypeDirector.GetAllAsync(default).ConfigureAwait(false);
            List<MasterDataDirectory> masterData = new List<MasterDataDirectory>();
            var list = from experienceTypeObjLinq in experienceTypeObj
                       select new MasterDataDirectory
                       {
                           code = experienceTypeObjLinq.Id,
                           value = experienceTypeObjLinq.ExperienceType,
                       };
            return list.ToList();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(MasterDataDirectory), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<GetApplicationSummary>>> GetAppSummarydata()
        {
           var appsummaryList=await this.iApplicationSummaryDirector.GetAppSummaryData(default).ConfigureAwait(false);
           return appsummaryList;
        }

        [HttpPost]
        [Route("List/UpdateInstitute")]
        public async Task<ActionResult<string>> UpdateInstitute([FromBody] UpdateInstitute input)
        {
            var institute = this.mapper.Map<Data.Abstractions.Models.ZmstInstitute>(input);
            int result = await this.zmstinstituteDirector.UpdateAsync(institute,default).ConfigureAwait(false);
            if(result > 0)
            {
                return "S";
            }
            else
            {
                return "F";
            }
        }
    }
}