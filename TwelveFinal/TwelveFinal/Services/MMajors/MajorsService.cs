using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MMajors
{
    public interface IMajorsService : IServiceScoped
    {
        Task<Majors> Create(Majors Majors);
        Task<Majors> Get(Guid Id);
        Task<Majors> Update(Majors Majors);
        Task<Majors> Delete(Majors Majors);
    }
    public class MajorsService : IMajorsService
    {
        private readonly IUOW UOW;
        private readonly IMajorsValidator MajorsValidator;

        public async Task<Majors> Create(Majors Majors)
        {
            Majors.Id = Guid.NewGuid();
            if (!await MajorsValidator.Create(Majors))
                return Majors;

            try
            {
                await UOW.Begin();
                await UOW.MajorsRepository.Create(Majors);
                await UOW.Commit();
                return await Get(Majors.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Majors> Delete(Majors Majors)
        {
            if (!await MajorsValidator.Delete(Majors))
                return Majors;

            try
            {
                await UOW.Begin();
                await UOW.MajorsRepository.Delete(Majors.Id);
                await UOW.Commit();
                return await Get(Majors.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Majors> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            Majors Majors = await UOW.MajorsRepository.Get(Id);
            return Majors;
        }

        public async Task<Majors> Update(Majors Majors)
        {
            if (!await MajorsValidator.Update(Majors))
                return Majors;

            try
            {
                await UOW.Begin();
                await UOW.MajorsRepository.Update(Majors);
                await UOW.Commit();
                return await Get(Majors.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
