using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;

namespace TwelveFinal.Services.MRegister
{
    public interface IRegisterInformationValidator : IServiceScoped
    {
        Task<bool> Create(RegisterInformation registerInformation);
        Task<bool> Update(RegisterInformation registerInformation);
        Task<bool> Delete(RegisterInformation registerInformation);
    }
    public class RegisterInformationValidator : IRegisterInformationValidator
    {
        public Task<bool> Create(RegisterInformation registerInformation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(RegisterInformation registerInformation)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(RegisterInformation registerInformation)
        {
            throw new NotImplementedException();
        }
    }
}
