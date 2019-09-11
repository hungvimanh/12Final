using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MGraduation
{
    public interface IGraduationInformationValidator : IServiceScoped
    {
        Task<bool> Create(GraduationInformation graduationInformation);
        Task<bool> Update(GraduationInformation graduationInformation);
        Task<bool> Delete(GraduationInformation graduationInformation);
    }
    public class GraduationInformationValidator : IGraduationInformationValidator
    {
        private IUOW UOW;
        public enum ErrorCode
        {

        }

        public GraduationInformationValidator(IUOW _UOW)
        {
            UOW = _UOW;
        }

        public Task<bool> Create(GraduationInformation graduationInformation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(GraduationInformation graduationInformation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(GraduationInformation graduationInformation)
        {
            throw new NotImplementedException();
        }

        
    }
}
