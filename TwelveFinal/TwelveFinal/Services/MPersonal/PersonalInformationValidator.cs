using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;
using TwelveFinal.Services.MHighSchool;
using TwelveFinal.Services.MTown;

namespace TwelveFinal.Services.MPersonal
{
    public interface IPersonalInformationValidator : IServiceScoped
    {
        Task<bool> Create(PersonalInformation personalInformation);
        Task<bool> Update(PersonalInformation personalInformation);
        Task<bool> Delete(PersonalInformation personalInformation);
        Task<bool> Check(PersonalInformation personalInformation);
    }
    public class PersonalInformationValidator : IPersonalInformationValidator
    {
        private IUOW UOW;
        private TownService TownService;
        private HighSchoolService HighSchoolService;
        public enum ErrorCode
        {
            NameError,
            NameInvalid,
            GenderInvalid,
            TownNotExisted,
            TownInvalid,
            AddressInvalid,
            HighSchoolInvalid,
            HighSchoolNotExisted,
            PhoneInvalid,
            EmailInvalid
        }

        public PersonalInformationValidator(IUOW _UOW, TownService _townService, HighSchoolService _highSchoolService)
        {
            UOW = _UOW;
            TownService = _townService;
            HighSchoolService = _highSchoolService;
        }

        public Task<bool> Create(PersonalInformation personalInformation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(PersonalInformation personalInformation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PersonalInformation personalInformation)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Check(PersonalInformation personalInformation)
        {
            bool IsValid = true;
            IsValid &= await ValidateName(personalInformation);
            IsValid &= await ValidateGender(personalInformation);
            IsValid &= await ValidateDob(personalInformation);
            IsValid &= await ValidateAddress(personalInformation);
            IsValid &= await ValidateHighShool(personalInformation);
            IsValid &= await ValidatePhone(personalInformation);
            IsValid &= await ValidateEmail(personalInformation);
            return IsValid;
        }

        private async Task<bool> ValidateName(PersonalInformation personalInformation)
        {
            if (string.IsNullOrEmpty(personalInformation.FullName))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.FullName), ErrorCode.NameInvalid);
                return personalInformation.IsValidated;
            }
            
            if (personalInformation.FullName.Any(char.IsLower))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.FullName), ErrorCode.NameError);
                return personalInformation.IsValidated;
            }

            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateGender(PersonalInformation personalInformation)
        {
            if(personalInformation.Gender == null)
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Gender), ErrorCode.GenderInvalid);
            }
            return personalInformation.IsValidated;
        }

        //to do
        private async Task<bool> ValidateDob(PersonalInformation personalInformation)
        {
            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateIdentify(PersonalInformation personalInformation)
        {
            if(personalInformation.Identify.Length < 12)
            {

            }
            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateAddress(PersonalInformation personalInformation)
        {
            if(personalInformation.TownId == null || personalInformation.TownId == Guid.Empty)
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.TownId), ErrorCode.TownInvalid);
            }

            if(TownService.Get(personalInformation.TownId) == null)
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.TownId), ErrorCode.TownNotExisted);
                return personalInformation.IsValidated;
            }

            if (string.IsNullOrEmpty(personalInformation.Address))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Address), ErrorCode.AddressInvalid);
                return personalInformation.IsValidated;
            }

            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateHighShool(PersonalInformation personalInformation)
        {
            if(personalInformation.HighSchoolGrade10Id == null || personalInformation.HighSchoolGrade10Id == Guid.Empty)
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.HighSchoolGrade10Id), ErrorCode.HighSchoolInvalid);
                return personalInformation.IsValidated;
            }

            if(HighSchoolService.Get(personalInformation.HighSchoolGrade10Id) == null)
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.HighSchoolGrade10Id), ErrorCode.HighSchoolNotExisted);
                return personalInformation.IsValidated;
            }

            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidatePhone(PersonalInformation personalInformation)
        {
            if (string.IsNullOrEmpty(personalInformation.Phone))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Phone), ErrorCode.PhoneInvalid);
                return personalInformation.IsValidated;
            }

            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateEmail(PersonalInformation personalInformation)
        {
            if (string.IsNullOrEmpty(personalInformation.Email))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Email), ErrorCode.EmailInvalid);
                return personalInformation.IsValidated;
            }

            return personalInformation.IsValidated;
        }
    }
}
