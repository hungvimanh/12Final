using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MDistrict
{
    public interface IDistrictService : IServiceScoped
    {
        Task<District> Create(District District);
        Task<District> Get(Guid Id);
        Task<District> Update(District District);
        Task<District> Delete(District District);
    }
    public class DistrictService : IDistrictService
    {
        private readonly IUOW UOW;
        private readonly IDistrictValidator DistrictValidator;

        public async Task<District> Create(District District)
        {
            District.Id = Guid.NewGuid();
            if (!await DistrictValidator.Create(District))
                return District;

            try
            {
                await UOW.Begin();
                await UOW.DistrictRepository.Create(District);
                await UOW.Commit();
                return await Get(District.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<District> Delete(District District)
        {
            if (!await DistrictValidator.Delete(District))
                return District;

            try
            {
                await UOW.Begin();
                await UOW.DistrictRepository.Delete(District.Id);
                await UOW.Commit();
                return await Get(District.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<District> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            District District = await UOW.DistrictRepository.Get(Id);
            return District;
        }

        public async Task<District> Update(District District)
        {
            if (!await DistrictValidator.Update(District))
                return District;

            try
            {
                await UOW.Begin();
                await UOW.DistrictRepository.Update(District);
                await UOW.Commit();
                return await Get(District.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
