using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MGraduation
{
    public interface IGraduationInformationService : IServiceScoped
    {
        Task<GraduationInformation> Get(Guid Id);
        Task<GraduationInformation> Update(GraduationInformation graduationInformation);
    }
    public class GraduationInformationService : IGraduationInformationService
    {
        private readonly IUOW UOW;
        private readonly IGraduationInformationValidator GraduationInformationValidator;

        public async Task<GraduationInformation> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            GraduationInformation GraduationInformation = await UOW.GraduationInformationRepository.Get(Id);
            return GraduationInformation;
        }

        public async Task<GraduationInformation> Update(GraduationInformation graduationInformation)
        {
            if (!await GraduationInformationValidator.Update(graduationInformation))
                return graduationInformation;

            try
            {
                await UOW.Begin();
                await UOW.GraduationInformationRepository.Update(graduationInformation);
                await UOW.Commit();
                return await Get(graduationInformation.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
