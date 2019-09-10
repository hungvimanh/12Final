using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;

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
