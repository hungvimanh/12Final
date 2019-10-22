using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;
using TwelveFinal.Services.MHighSchool;
using TwelveFinal.Services.MProvince;
using TwelveFinal.Services.MTown;

namespace TwelveFinal.Services.MPersonal
{
    public interface IPersonalInformationValidator : IServiceScoped
    {
        Task<bool> Check(PersonalInformation personalInformation);
    }
    public class PersonalInformationValidator : IPersonalInformationValidator
    {
        private IUOW UOW;
        private IProvinceService ProvinceService;
        private ITownService TownService;
        private IHighSchoolService HighSchoolService;
        public enum ErrorCode
        {
            NotExisted,
            Invalid
        }

        public PersonalInformationValidator(
            IUOW _UOW, 
            IProvinceService provinceService,
            ITownService townService,
            IHighSchoolService highSchoolService
            )
        {
            UOW = _UOW;
            ProvinceService = provinceService;
            ProvinceService = provinceService;
            HighSchoolService = highSchoolService;
        }

        public async Task<bool> Check(PersonalInformation personalInformation)
        {
            bool IsValid = true;
            IsValid &= await ValidateDepartmentCode(personalInformation);
            IsValid &= await ValidateDate(personalInformation);
            IsValid &= await ValidateName(personalInformation);
            IsValid &= await ValidateGender(personalInformation);
            IsValid &= await ValidateDob(personalInformation);
            IsValid &= await ValidatePlateOfBirth(personalInformation);
            IsValid &= await ValidateIdentify(personalInformation);
            IsValid &= await ValidateEthnic(personalInformation);
            IsValid &= await ValidateGrade12Name(personalInformation);
            IsValid &= await ValidateAddress(personalInformation);
            IsValid &= await ValidateHighShool(personalInformation);
            IsValid &= await ValidatePhone(personalInformation);
            IsValid &= await ValidateEmail(personalInformation);
            return IsValid;
        }

        private async Task<bool> ValidateDepartmentCode(PersonalInformation personalInformation)
        {
            //Kiểm tra mã sở GD DT
            ProvinceFilter filter = new ProvinceFilter
            {
                Code = new StringFilter { Equal = personalInformation.DepartmentCode }
            };

            var count = await UOW.ProvinceRepository.Count(filter);
            if(count == 0)
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.DepartmentCode), ErrorCode.NotExisted);
            }
            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateDate(PersonalInformation personalInformation)
        {
            //Kiểm tra ngày viết phiếu
            if(personalInformation.Date == null || personalInformation.Date == default(DateTime))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Date), ErrorCode.Invalid);
            }
            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateName(PersonalInformation personalInformation)
        {
            //Kiểm tra họ và tên nhập hợp lệ
            // tên phải được viết in hoa
            if (string.IsNullOrEmpty(personalInformation.FullName))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.FullName), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }
            
            if (personalInformation.FullName.Any(char.IsLower))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.FullName), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateGender(PersonalInformation personalInformation)
        {
            //Kiểm tra đã tick giới tính chưa?
            if(personalInformation.Gender == null)
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Gender), ErrorCode.Invalid);
            }
            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateDob(PersonalInformation personalInformation)
        {
            //Kiểm tra ngày tháng năm sinh
            if(personalInformation.Dob == null || personalInformation.Dob == default(DateTime))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Dob), ErrorCode.Invalid);
            }
            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateIdentify(PersonalInformation personalInformation)
        {
            //Kiểm tra số CMND/Căn cước công dân
            if(string.IsNullOrEmpty(personalInformation.Identify))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Identify), ErrorCode.Invalid);
            }
            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidatePlateOfBirth(PersonalInformation personalInformation)
        {
            //Kiểm tra nơi sinh có tồn tại?
            if (string.IsNullOrEmpty(personalInformation.PlaceOfBirth))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.PlaceOfBirth), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            ProvinceFilter filter = new ProvinceFilter
            {
                Name = new StringFilter { Equal = personalInformation.PlaceOfBirth }
            };
            if(await UOW.ProvinceRepository.Count(filter) == 0)
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.PlaceOfBirth), ErrorCode.NotExisted);
            }

            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateEthnic(PersonalInformation personalInformation)
        {
            //Kiểm tra dân tộc có hợp lệ hay ko?
            if (string.IsNullOrEmpty(personalInformation.Ethnic))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Ethnic), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }
            EthnicFilter filter = new EthnicFilter
            {
                Name = new StringFilter { Equal = personalInformation.Ethnic }
            };
            var count = await UOW.EthnicRepository.Count(filter);
            if(count == 0)
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Ethnic), ErrorCode.NotExisted);
            }
            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateGrade12Name(PersonalInformation personalInformation)
        {
            //Tên lớp 12
            if (string.IsNullOrEmpty(personalInformation.Grade12Name))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Grade12Name), ErrorCode.Invalid);
            }
            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateAddress(PersonalInformation personalInformation)
        {
            //Kiểm tra địa chỉ
            if(string.IsNullOrEmpty(personalInformation.TownCode))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.TownId), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            TownFilter filter = new TownFilter
            {
                Id = new GuidFilter { Equal = personalInformation.TownId},
                Code = new StringFilter { Equal = personalInformation.TownCode }
            };
            var count = await UOW.TownRepository.Count(filter);
            if(count == 0)
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.TownId), ErrorCode.NotExisted);
                return personalInformation.IsValidated;
            }

            if (string.IsNullOrEmpty(personalInformation.Address))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Address), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateHighShool(PersonalInformation personalInformation)
        {
            //Kiểm tra trường c3
            if(string.IsNullOrEmpty(personalInformation.HighSchoolGrade10Code))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.HighSchoolGrade10Code), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            if (string.IsNullOrEmpty(personalInformation.HighSchoolGrade11Code))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.HighSchoolGrade11Code), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            if (string.IsNullOrEmpty(personalInformation.HighSchoolGrade12Code))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.HighSchoolGrade12Code), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            if (!await HighSchoolValidation(personalInformation.HighSchoolGrade10Code))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.HighSchoolGrade10Code), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            if (!await HighSchoolValidation(personalInformation.HighSchoolGrade11Code))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.HighSchoolGrade11Code), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            if (!await HighSchoolValidation(personalInformation.HighSchoolGrade12Code))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.HighSchoolGrade12Code), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            return personalInformation.IsValidated;
        }
        private async Task<bool> HighSchoolValidation(string highSchoolCode)
        {
            HighSchoolFilter filter = new HighSchoolFilter
            {
                Code = new StringFilter { Equal = highSchoolCode }
            };

            var count = await UOW.HighSchoolRepository.Count(filter);
            if (count == 0)
                return false;
            return true;
        }

        private async Task<bool> ValidatePhone(PersonalInformation personalInformation)
        {
            //Kiểm tra số điện thoại
            if (string.IsNullOrEmpty(personalInformation.Phone))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Phone), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            return personalInformation.IsValidated;
        }

        private async Task<bool> ValidateEmail(PersonalInformation personalInformation)
        {
            //Kiểm tra email
            if (string.IsNullOrEmpty(personalInformation.Email))
            {
                personalInformation.AddError(nameof(PersonalInformationValidator), nameof(personalInformation.Email), ErrorCode.Invalid);
                return personalInformation.IsValidated;
            }

            return personalInformation.IsValidated;
        }
    }
}
