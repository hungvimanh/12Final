using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MRegister
{
    public interface IRegisterInformationService : IServiceScoped
    {
        Task<RegisterInformation> Get(Guid Id);
        Task<RegisterInformation> Update(RegisterInformation registerInformation);
    }
    public class RegisterInformationService : IRegisterInformationService
    {
        private readonly IUOW UOW;
        private readonly IRegisterInformationValidator RegisterInformationValidator;

        public async Task<RegisterInformation> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            RegisterInformation registerInformation = await UOW.RegisterInformationRepository.Get(Id);
            return registerInformation;
        }

        public async Task<RegisterInformation> Update(RegisterInformation registerInformation)
        {
            if (!await RegisterInformationValidator.Update(registerInformation))
                return registerInformation;

            try
            {
                await UOW.Begin();
                await UOW.RegisterInformationRepository.Update(registerInformation);
                await UOW.Commit();
                return await Get(registerInformation.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
