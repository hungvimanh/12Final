using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MProvince
{
    public interface IProvinceService : IServiceScoped
    {
        Task<Province> Create(Province Province);
        Task<Province> Get(Guid Id);
        Task<Province> Update(Province Province);
        Task<Province> Delete(Province Province);
    }
    public class ProvinceService : IProvinceService
    {
        private readonly IUOW UOW;
        private readonly IProvinceValidator ProvinceValidator;

        public async Task<Province> Create(Province Province)
        {
            Province.Id = Guid.NewGuid();
            if (!await ProvinceValidator.Create(Province))
                return Province;

            try
            {
                await UOW.Begin();
                await UOW.ProvinceRepository.Create(Province);
                await UOW.Commit();
                return await Get(Province.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Province> Delete(Province Province)
        {
            if (!await ProvinceValidator.Delete(Province))
                return Province;

            try
            {
                await UOW.Begin();
                await UOW.ProvinceRepository.Delete(Province.Id);
                await UOW.Commit();
                return await Get(Province.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Province> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            Province Province = await UOW.ProvinceRepository.Get(Id);
            return Province;
        }

        public async Task<Province> Update(Province Province)
        {
            if (!await ProvinceValidator.Update(Province))
                return Province;

            try
            {
                await UOW.Begin();
                await UOW.ProvinceRepository.Update(Province);
                await UOW.Commit();
                return await Get(Province.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
