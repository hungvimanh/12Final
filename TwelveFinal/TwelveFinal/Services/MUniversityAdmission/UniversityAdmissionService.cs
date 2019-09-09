using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUniversityAdmission
{
    public interface IUniversityAdmissionService : IServiceScoped
    {
        Task<UniversityAdmission> Create(UniversityAdmission universityAdmission);
        Task<UniversityAdmission> Get(Guid Id);
        Task<UniversityAdmission> Update(UniversityAdmission universityAdmission);
        Task<UniversityAdmission> Delete(UniversityAdmission universityAdmission);
    }
    public class UniversityAdmissionService : IUniversityAdmissionService
    {
        private readonly IUOW UOW;
        private readonly IUniversityAdmissionValidator UniversityAdmissionValidator;

        public async Task<UniversityAdmission> Create(UniversityAdmission universityAdmission)
        {
            universityAdmission.Id = Guid.NewGuid();
            if (!await UniversityAdmissionValidator.Create(universityAdmission))
                return universityAdmission;

            try
            {
                await UOW.Begin();
                await UOW.UniversityAdmissionRepository.Create(universityAdmission);
                await UOW.Commit();
                return await Get(universityAdmission.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<UniversityAdmission> Delete(UniversityAdmission universityAdmission)
        {
            if (!await UniversityAdmissionValidator.Delete(universityAdmission))
                return universityAdmission;

            try
            {
                await UOW.Begin();
                await UOW.UniversityAdmissionRepository.Delete(universityAdmission.Id);
                await UOW.Commit();
                return await Get(universityAdmission.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<UniversityAdmission> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            UniversityAdmission universityAdmission = await UOW.UniversityAdmissionRepository.Get(Id);
            return universityAdmission;
        }

        public async Task<UniversityAdmission> Update(UniversityAdmission universityAdmission)
        {
            if (!await UniversityAdmissionValidator.Update(universityAdmission))
                return universityAdmission;

            try
            {
                await UOW.Begin();
                await UOW.UniversityAdmissionRepository.Update(universityAdmission);
                await UOW.Commit();
                return await Get(universityAdmission.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
