using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;
using TwelveFinal.Services.MHighSchool;
using TwelveFinal.Services.MProvince;

namespace TwelveFinal.Services.MRegister
{
    public interface IRegisterInformationValidator : IServiceScoped
    {
        Task<bool> Check(RegisterInformation registerInformation);
    }
    public class RegisterInformationValidator : IRegisterInformationValidator
    {
        private IUOW UOW;
        private IProvinceService ProvinceService;
        private IHighSchoolService HighSchoolService;
        public enum ErrorCode
        {
            Invalid,
            NotExisted
        }

        public RegisterInformationValidator(IUOW _UOW, IProvinceService _ProvinceService, IHighSchoolService _HighSchoolService)
        {
            UOW = _UOW;
            ProvinceService = _ProvinceService;
            HighSchoolService = _HighSchoolService;
        }

        public async Task<bool> Check(RegisterInformation registerInformation)
        {
            bool IsValid = true;
            IsValid &= await ValidateStudyAtHighSchool(registerInformation);
            IsValid &= await ValidateClusterContest(registerInformation);
            IsValid &= await ValidateRegisterPlaceOfExam(registerInformation);
            IsValid &= await ValidateLanguages(registerInformation);
            return IsValid;
        }

        private async Task<bool> ValidateStudyAtHighSchool(RegisterInformation registerInformation)
        {
            //Kiểm tra StudyAtHighSchool
            if (registerInformation.StudyAtHighSchool == null)
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.StudyAtHighSchool), ErrorCode.Invalid);
                return registerInformation.IsValidated;
            }

            return registerInformation.IsValidated;
        }

        private async Task<bool> ValidateClusterContest(RegisterInformation registerInformation)
        {
            //Kiểm tra cụm dự thi
            if (string.IsNullOrEmpty(registerInformation.ClusterContestCode))
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.ClusterContestCode), ErrorCode.Invalid);
                return registerInformation.IsValidated;
            }

            var filter = new ProvinceFilter
            {
                Code = new StringFilter { Equal = registerInformation.ClusterContestCode }
            };
            var count = await UOW.ProvinceRepository.Count(filter);
            if(count == 0)
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.ClusterContestCode), ErrorCode.NotExisted);
                return registerInformation.IsValidated;
            }

            return registerInformation.IsValidated;
        }

        private async Task<bool> ValidateRegisterPlaceOfExam(RegisterInformation registerInformation)
        {
            //Kiểm tra nơi đăng ký dự thi
            if (string.IsNullOrEmpty(registerInformation.RegisterPlaceOfExamCode))
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.RegisterPlaceOfExamCode), ErrorCode.Invalid);
                return registerInformation.IsValidated;
            }

            var filter = new HighSchoolFilter
            {
                Code = new StringFilter { Equal = registerInformation.RegisterPlaceOfExamCode }
            };
            var count = await UOW.HighSchoolRepository.Count(filter);
            if (ProvinceService.Get(registerInformation.RegisterPlaceOfExamId) == null)
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.RegisterPlaceOfExamCode), ErrorCode.NotExisted);
                return registerInformation.IsValidated;
            }

            return registerInformation.IsValidated;
        }

        private async Task<bool> ValidateLanguages(RegisterInformation registerInformation)
        {
            //Kiểm tra kí hiệu ngoại ngữ
            List<string> Languages = new List<string> { "N1", "N2", "N3", "N4", "N5", "N6" };

            if(!string.IsNullOrEmpty(registerInformation.Languages) && !Languages.Contains(registerInformation.Languages))
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.Languages), ErrorCode.Invalid);
            }
            return registerInformation.IsValidated;
        }

    }
}
