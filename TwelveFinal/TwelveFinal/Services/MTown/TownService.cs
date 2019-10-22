using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MTown
{
    public interface ITownService : IServiceScoped
    {
        Task<Town> Create(Town Town);
        Task<Town> Get(Guid Id);
        Task<List<Town>> List(TownFilter townFilter);
        Task<Town> Update(Town Town);
        Task<Town> Delete(Town Town);
    }
    public class TownService : ITownService
    {
        private readonly IUOW UOW;
        private readonly ITownValidator TownValidator;

        public TownService(
            IUOW UOW,
            ITownValidator TownValidator
            )
        {
            this.UOW = UOW;
            this.TownValidator = TownValidator;
        }

        public async Task<Town> Create(Town Town)
        {
            Town.Id = Guid.NewGuid();
            if (!await TownValidator.Create(Town))
                return Town;

            try
            {
                await UOW.Begin();
                await UOW.TownRepository.Create(Town);
                await UOW.Commit();
                return await Get(Town.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Town> Delete(Town Town)
        {
            if (!await TownValidator.Delete(Town))
                return Town;

            try
            {
                await UOW.Begin();
                await UOW.TownRepository.Delete(Town.Id);
                await UOW.Commit();
                return await Get(Town.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<List<Town>> List(TownFilter townFilter)
        {
            return await UOW.TownRepository.List(townFilter);
        }

        public async Task<Town> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            Town Town = await UOW.TownRepository.Get(Id);
            return Town;
        }

        public async Task<Town> Update(Town Town)
        {
            if (!await TownValidator.Update(Town))
                return Town;

            try
            {
                await UOW.Begin();
                await UOW.TownRepository.Update(Town);
                await UOW.Commit();
                return await Get(Town.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
