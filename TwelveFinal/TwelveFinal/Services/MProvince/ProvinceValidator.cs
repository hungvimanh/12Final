using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MProvince
{
    public interface IProvinceValidator : IServiceScoped
    {
        Task<bool> Create(Province Province);
        Task<bool> Update(Province Province);
        Task<bool> Delete(Province Province);
    }
    public class ProvinceValidator : IProvinceValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            NotExisted,
            Duplicate
        }

        public ProvinceValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public async Task<bool> Create(Province province)
        {
            bool IsValid = true;
            IsValid &= await CodeValidate(province);
            return IsValid;
        }

        public async Task<bool> Delete(Province province)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(province);
            return IsValid;
        }

        public async Task<bool> Update(Province province)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(province);
            IsValid &= await CodeValidate(province);
            return IsValid;
        }

        private async Task<bool> IsExisted(Province province)
        {
            //Kiểm tra sự tồn tại
            if (await UOW.ProvinceRepository.Get(province.Id) == null)
            {
                province.AddError(nameof(ProvinceValidator), nameof(province.Name), ErrorCode.NotExisted);
            }
            return province.IsValidated;
        }

        private async Task<bool> CodeValidate(Province province)
        {
            //Kiểm tra sự trùng lặp Code
            ProvinceFilter filter = new ProvinceFilter
            {
                Id = new GuidFilter { NotEqual = province.Id },
                Code = new StringFilter { Equal = province.Code }
            };

            var count = await UOW.ProvinceRepository.Count(filter);
            if (count > 0)
            {
                province.AddError(nameof(ProvinceValidator), nameof(province.Code), ErrorCode.Duplicate);
            }
            return province.IsValidated;
        }
    }
}
