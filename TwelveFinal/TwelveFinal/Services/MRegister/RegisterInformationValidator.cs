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
        Task<bool> Create(RegisterInformation registerInformation);
        Task<bool> Update(RegisterInformation registerInformation);
        Task<bool> Delete(RegisterInformation registerInformation);
    }
    public class RegisterInformationValidator : IRegisterInformationValidator
    {
        private IUOW UOW;
        private ProvinceService ProvinceService;
        private HighSchoolService HighSchoolService;
        public enum ErrorCode
        {
            StudyAtHighSchoolInvalid,
            PassedInvalid,
            ContestGroupInvalid,
            ContestGroupNotExisted,
            ContestUnitInvalid,
            ContestUnitNotExisted
        }

        public RegisterInformationValidator(IUOW _UOW, ProvinceService _ProvinceService, HighSchoolService _HighSchoolService)
        {
            UOW = _UOW;
            ProvinceService = _ProvinceService;
            HighSchoolService = _HighSchoolService;
        }

        public Task<bool> Create(RegisterInformation registerInformation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(RegisterInformation registerInformation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(RegisterInformation registerInformation)
        {
            throw new NotImplementedException();
        }

        private async Task<bool> ValidateStudyAtHighSchool(RegisterInformation registerInformation)
        {
            if(registerInformation.StudyAtHighSchool == null)
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.StudyAtHighSchool), ErrorCode.StudyAtHighSchoolInvalid);
                return registerInformation.IsValidated;
            }

            return registerInformation.IsValidated;
        }

        private async Task<bool> ValidatePassed(RegisterInformation registerInformation)
        {
            if (registerInformation.Graduated == null)
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.Graduated), ErrorCode.PassedInvalid);
                return registerInformation.IsValidated;
            }

            return registerInformation.IsValidated;
        }

        private async Task<bool> ValidateContestGroup(RegisterInformation registerInformation)
        {
            if (registerInformation.ClusterContestId == null || registerInformation.ClusterContestId == Guid.Empty)
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.ClusterContestId), ErrorCode.ContestGroupInvalid);
                return registerInformation.IsValidated;
            }

            if(ProvinceService.Get(registerInformation.ClusterContestId) == null)
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.ClusterContestId), ErrorCode.ContestGroupNotExisted);
                return registerInformation.IsValidated;
            }

            return registerInformation.IsValidated;
        }

        private async Task<bool> ValidateContestUnit(RegisterInformation registerInformation)
        {
            if (registerInformation.RegisterPlaceOfExamId == null || registerInformation.RegisterPlaceOfExamId == Guid.Empty)
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.RegisterPlaceOfExamId), ErrorCode.ContestUnitInvalid);
                return registerInformation.IsValidated;
            }

            if (ProvinceService.Get(registerInformation.RegisterPlaceOfExamId) == null)
            {
                registerInformation.AddError(nameof(RegisterInformationValidator), nameof(registerInformation.RegisterPlaceOfExamId), ErrorCode.ContestUnitNotExisted);
                return registerInformation.IsValidated;
            }

            return registerInformation.IsValidated;
        }

        //todo
        private async Task<bool> ValidateTest(RegisterInformation registerInformation)
        {
            return registerInformation.IsValidated;
        }
    }
}
