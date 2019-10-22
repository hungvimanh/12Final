using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MHighSchool
{
    public interface IHighSchoolValidator : IServiceScoped
    {
        Task<bool> Create(HighSchool highSchool);
        Task<bool> Update(HighSchool highSchool);
        Task<bool> Delete(HighSchool highSchool);
    }
    public class HighSchoolValidator : IHighSchoolValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            Duplicate,
            NotExisted
        }

        public HighSchoolValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Create(HighSchool highSchool)
        {
            bool IsValid = true;
            IsValid &= await CodeValidate(highSchool);
            IsValid &= await AreaValidate(highSchool);
            return IsValid;
        }

        public async Task<bool> Delete(HighSchool highSchool)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(highSchool);
            return IsValid;
        }

        public async Task<bool> Update(HighSchool highSchool)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(highSchool);
            IsValid &= await CodeValidate(highSchool);
            IsValid &= await AreaValidate(highSchool);
            return IsValid;
        }

        private async Task<bool> IsExisted(HighSchool highSchool)
        {
            //Kiểm tra sự tồn tại trong DB
            if(await UOW.HighSchoolRepository.Get(highSchool.Id) == null)
            {
                highSchool.AddError(nameof(HighSchoolValidator), nameof(highSchool.Name), ErrorCode.NotExisted);
            }
            return highSchool.IsValidated;
        }

        private async Task<bool> CodeValidate(HighSchool highSchool)
        {
            //Kiểm tra sự trùng lặp Code
            HighSchoolFilter filter = new HighSchoolFilter
            {
                Id = new GuidFilter { NotEqual = highSchool.Id },
                DistrictId = highSchool.DistrictId,
                Code = new StringFilter { Equal = highSchool.Code }
            };

            var count = await UOW.HighSchoolRepository.Count(filter);
            if(count > 0)
            {
                highSchool.AddError(nameof(HighSchoolValidator), nameof(highSchool.Code), ErrorCode.Duplicate);
            }
            return highSchool.IsValidated;
        }

        private async Task<bool> AreaValidate(HighSchool highSchool)
        {
            //Kiểm tra khu vực có hợp lệ?
            AreaFilter filter = new AreaFilter
            {
                Code = new StringFilter { Equal = highSchool.AreaCode }
            };
            var count = await UOW.AreaRepository.Count(filter);
            if(count == 0)
            {
                highSchool.AddError(nameof(HighSchoolValidator), nameof(highSchool.AreaCode), ErrorCode.NotExisted);
            }

            return highSchool.IsValidated;
        }
    }
}
