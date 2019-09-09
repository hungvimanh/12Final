using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;

namespace TwelveFinal.Services.MUniversityAdmission
{
    public interface IUniversityAdmissionValidator : IServiceScoped
    {
        Task<bool> Create(UniversityAdmission universityAdmission);
        Task<bool> Update(UniversityAdmission universityAdmission);
        Task<bool> Delete(UniversityAdmission universityAdmission);
    }
    public class UniversityAdmissionValidator : IUniversityAdmissionValidator
    {
        public Task<bool> Create(UniversityAdmission universityAdmission)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(UniversityAdmission universityAdmission)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(UniversityAdmission universityAdmission)
        {
            throw new NotImplementedException();
        }
    }
}
