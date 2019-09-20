using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MTown
{
    public interface ITownValidator : IServiceScoped
    {
        Task<bool> Create(Town Town);
        Task<bool> Update(Town Town);
        Task<bool> Delete(Town Town);
    }
    public class TownValidator : ITownValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {
            NotExisted,
            Duplicate
        }

        public async Task<bool> Create(Town town)
        {
            bool IsValid = true;
            IsValid &= await CodeValidate(town);
            return IsValid;
        }

        public async Task<bool> Delete(Town town)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(town);
            return IsValid;
        }

        public async Task<bool> Update(Town town)
        {
            bool IsValid = true;
            IsValid &= await IsExisted(town);
            IsValid &= await CodeValidate(town);
            return IsValid;
        }

        private async Task<bool> IsExisted(Town town)
        {
            if (await UOW.TownRepository.Get(town.Id) == null)
            {
                town.AddError(nameof(TownValidator), nameof(town.Name), ErrorCode.NotExisted);
            }
            return town.IsValidated;
        }

        private async Task<bool> CodeValidate(Town town)
        {
            TownFilter filter = new TownFilter
            {
                Id = new GuidFilter { NotEqual = town.Id },
                DistrictId = town.DistrictId,
                Code = new StringFilter { Equal = town.Code }
            };

            var count = await UOW.TownRepository.Count(filter);
            if (count > 0)
            {
                town.AddError(nameof(TownValidator), nameof(town.Code), ErrorCode.Duplicate);
            }
            return town.IsValidated;
        }
    }
}
