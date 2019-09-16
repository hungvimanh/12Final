using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUniversity_Majors_Majors
{
    public interface IUniversity_MajorsValidator : IServiceScoped
    {
        Task<bool> Create(University_Majors university_Majors);
        Task<bool> Update(University_Majors university_Majors);
        Task<bool> Delete(University_Majors university_Majors);
    }
    public class University_MajorsValidator : IUniversity_MajorsValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {

        }

        public University_MajorsValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public Task<bool> Create(University_Majors university_Majors)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(University_Majors university_Majors)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(University_Majors university_Majors)
        {
            throw new NotImplementedException();
        }
    }
}
