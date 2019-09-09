using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;

namespace TwelveFinal.Services.MPersonal
{
    public interface IPersonalInformationValidator : IServiceScoped
    {
        Task<bool> Create(PersonalInformation personalInformation);
        Task<bool> Update(PersonalInformation personalInformation);
        Task<bool> Delete(PersonalInformation personalInformation);
    }
    public class PersonalInformationValidator : IPersonalInformationValidator
    {
        public Task<bool> Create(PersonalInformation personalInformation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(PersonalInformation personalInformation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(PersonalInformation personalInformation)
        {
            throw new NotImplementedException();
        }
    }
}
