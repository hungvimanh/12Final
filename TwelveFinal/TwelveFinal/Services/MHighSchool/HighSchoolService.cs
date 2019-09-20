using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MHighSchool
{
    public interface IHighSchoolService : IServiceScoped
    {
        Task<HighSchool> Create(HighSchool HighSchool);
        Task<HighSchool> Get(Guid Id);
        Task<HighSchool> Update(HighSchool HighSchool);
        Task<HighSchool> Delete(HighSchool HighSchool);
    }
    public class HighSchoolService : IHighSchoolService
    {
        private readonly IUOW UOW;
        private readonly IHighSchoolValidator HighSchoolValidator;

        public HighSchoolService(
            IUOW UOW,
            IHighSchoolValidator HighSchoolValidator
            )
        {
            this.UOW = UOW;
            this.HighSchoolValidator = HighSchoolValidator;
        }
        public async Task<HighSchool> Create(HighSchool HighSchool)
        {
            HighSchool.Id = Guid.NewGuid();
            if (!await HighSchoolValidator.Create(HighSchool))
                return HighSchool;

            try
            {
                await UOW.Begin();
                await UOW.HighSchoolRepository.Create(HighSchool);
                await UOW.Commit();
                return await Get(HighSchool.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<HighSchool> Delete(HighSchool HighSchool)
        {
            if (!await HighSchoolValidator.Delete(HighSchool))
                return HighSchool;

            try
            {
                await UOW.Begin();
                await UOW.HighSchoolRepository.Delete(HighSchool.Id);
                await UOW.Commit();
                return await Get(HighSchool.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<HighSchool> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            HighSchool HighSchool = await UOW.HighSchoolRepository.Get(Id);
            return HighSchool;
        }

        public async Task<HighSchool> Update(HighSchool HighSchool)
        {
            if (!await HighSchoolValidator.Update(HighSchool))
                return HighSchool;

            try
            {
                await UOW.Begin();
                await UOW.HighSchoolRepository.Update(HighSchool);
                await UOW.Commit();
                return await Get(HighSchool.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
