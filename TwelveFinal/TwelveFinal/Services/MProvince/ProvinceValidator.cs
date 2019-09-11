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

        }

        public ProvinceValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public Task<bool> Create(Province Province)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Province Province)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Province Province)
        {
            throw new NotImplementedException();
        }
    }
}
