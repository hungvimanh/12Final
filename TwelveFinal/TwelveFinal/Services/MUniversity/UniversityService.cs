using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MUniversity
{
    public interface IUniversityService : IServiceScoped
    {
        Task<University> Create(University University);
        Task<University> Get(Guid Id);
        Task<List<University>> List(UniversityFilter universityFilter);
        Task<University> Update(University University);
        Task<University> Delete(University University);
    }
    public class UniversityService : IUniversityService
    {
        private readonly IUOW UOW;
        private readonly IUniversityValidator UniversityValidator;

        public async Task<University> Create(University University)
        {
            University.Id = Guid.NewGuid();
            if (!await UniversityValidator.Create(University))
                return University;

            try
            {
                await UOW.Begin();
                await UOW.UniversityRepository.Create(University);
                await UOW.Commit();
                return await Get(University.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<University> Delete(University University)
        {
            if (!await UniversityValidator.Delete(University))
                return University;

            try
            {
                await UOW.Begin();
                await UOW.UniversityRepository.Delete(University.Id);
                await UOW.Commit();
                return await Get(University.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<University> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            University University = await UOW.UniversityRepository.Get(Id);
            return University;
        }

        public async Task<List<University>> List(UniversityFilter universityFilter)
        {
            return await UOW.UniversityRepository.List(universityFilter);
        }

        public async Task<University> Update(University University)
        {
            if (!await UniversityValidator.Update(University))
                return University;

            try
            {
                await UOW.Begin();
                await UOW.UniversityRepository.Update(University);
                await UOW.Commit();
                return await Get(University.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
