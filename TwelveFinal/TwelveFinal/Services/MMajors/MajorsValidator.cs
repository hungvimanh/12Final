using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MMajors
{
    public interface IMajorsValidator : IServiceScoped
    {
        Task<bool> Create(Majors Majors);
        Task<bool> Update(Majors Majors);
        Task<bool> Delete(Majors Majors);
    }
    public class MajorsValidator : IMajorsValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {

        }

        public MajorsValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public Task<bool> Create(Majors Majors)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Majors Majors)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Majors Majors)
        {
            throw new NotImplementedException();
        }
    }
}
