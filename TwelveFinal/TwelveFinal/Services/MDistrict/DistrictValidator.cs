using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MDistrict
{
    public interface IDistrictValidator : IServiceScoped
    {
        Task<bool> Create(District District);
        Task<bool> Update(District District);
        Task<bool> Delete(District District);
    }
    public class DistrictValidator : IDistrictValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            NotExisted,
            Duplicate
        }

        public DistrictValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Create(District district)
        {
            bool IsValid = true;
            IsValid &= await CodeValidate(district);
            return IsValid;
        }

        public async Task<bool> Delete(District district)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(district);
            return IsValid;
        }

        public async Task<bool> Update(District district)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(district);
            IsValid &= await CodeValidate(district);
            return IsValid;
        }

        private async Task<bool> IsExisted(District district)
        {
            if (await UOW.DistrictRepository.Get(district.Id) == null)
            {
                district.AddError(nameof(DistrictValidator), nameof(district.Name), ErrorCode.NotExisted);
            }
            return district.IsValidated;
        }

        private async Task<bool> CodeValidate(District district)
        {
            DistrictFilter filter = new DistrictFilter
            {
                Id = new GuidFilter { NotEqual = district.Id },
                ProvinceId = district.ProvinceId,
                Code = new StringFilter { Equal = district.Code }
            };

            var count = await UOW.DistrictRepository.Count(filter);
            if (count > 0)
            {
                district.AddError(nameof(DistrictValidator), nameof(district.Code), ErrorCode.Duplicate);
            }
            return district.IsValidated;
        }
    }
}
