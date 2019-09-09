using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MPersonal
{
    public interface IPersonalInformationService : IServiceScoped
    {
        Task<PersonalInformation> Create(PersonalInformation personalInformation);
        Task<PersonalInformation> Get(Guid Id);
        Task<PersonalInformation> Update(PersonalInformation personalInformation);
        Task<PersonalInformation> Delete(PersonalInformation personalInformation);
    }
    public class PersonalInformationService : IPersonalInformationService
    {
        private readonly IUOW UOW;
        private readonly IPersonalInformationValidator PersonalInformationValidator;

        public async Task<PersonalInformation> Create(PersonalInformation personalInformation)
        {
            personalInformation.Id = Guid.NewGuid();
            if (!await PersonalInformationValidator.Create(personalInformation))
                return personalInformation;

            try
            {
                await UOW.Begin();
                await UOW.PersonalInformationRepository.Create(personalInformation);
                await UOW.Commit();
                return await Get(personalInformation.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<PersonalInformation> Delete(PersonalInformation personalInformation)
        {
            if (!await PersonalInformationValidator.Delete(personalInformation))
                return personalInformation;

            try
            {
                await UOW.Begin();
                await UOW.PersonalInformationRepository.Delete(personalInformation.Id);
                await UOW.Commit();
                return await Get(personalInformation.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<PersonalInformation> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            PersonalInformation personalInformation = await UOW.PersonalInformationRepository.Get(Id);
            return personalInformation;
        }

        public async Task<PersonalInformation> Update(PersonalInformation personalInformation)
        {
            if (!await PersonalInformationValidator.Update(personalInformation))
                return personalInformation;

            try
            {
                await UOW.Begin();
                await UOW.PersonalInformationRepository.Update(personalInformation);
                await UOW.Commit();
                return await Get(personalInformation.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
