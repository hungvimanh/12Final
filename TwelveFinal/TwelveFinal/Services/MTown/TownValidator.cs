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

        }

        public TownValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public Task<bool> Create(Town Town)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Town Town)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Town Town)
        {
            throw new NotImplementedException();
        }
    }
}
