using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUniversity
{
    public interface IUniversityValidator : IServiceScoped
    {
        Task<bool> Create(University University);
        Task<bool> Update(University University);
        Task<bool> Delete(University University);
    }
    public class UniversityValidator : IUniversityValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {

        }

        public UniversityValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public Task<bool> Create(University University)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(University University)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(University University)
        {
            throw new NotImplementedException();
        }
    }
}
