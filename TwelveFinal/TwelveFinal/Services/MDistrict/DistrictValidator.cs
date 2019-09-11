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

        }

        public DistrictValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public Task<bool> Create(District District)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(District District)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(District District)
        {
            throw new NotImplementedException();
        }
    }
}
