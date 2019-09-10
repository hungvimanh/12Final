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
        Task<bool> Create(HighSchool HighSchool);
        Task<bool> Update(HighSchool HighSchool);
        Task<bool> Delete(HighSchool HighSchool);
    }
    public class HighSchoolValidator : IHighSchoolValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {

        }

        public HighSchoolValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public Task<bool> Create(HighSchool HighSchool)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(HighSchool HighSchool)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(HighSchool HighSchool)
        {
            throw new NotImplementedException();
        }
    }
}
