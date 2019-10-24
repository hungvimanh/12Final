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
        Task<Province> Create(Province province);
        Task<Province> Get(Guid Id);
        Task<List<Province>> List(ProvinceFilter provinceFilter);
        Task<Province> Update(Province province);
        Task<Province> Delete(Province province);
    }
    public class ProvinceService : IProvinceService
    {
        private readonly IUOW UOW;
        private readonly IProvinceValidator ProvinceValidator;

        public ProvinceService(
            IUOW UOW,
            IProvinceValidator ProvinceValidator
            )
        {
            this.UOW = UOW;
            this.ProvinceValidator = ProvinceValidator;
        }

        public async Task<Province> Create(Province province)
        {
            province.Id = Guid.NewGuid();
            if (!await ProvinceValidator.Create(province))
                return province;

            try
            {
                await UOW.Begin();
                await UOW.ProvinceRepository.Create(province);
                await UOW.Commit();
                return await Get(province.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<Province> Delete(Province province)
        {
            if (!await ProvinceValidator.Delete(province))
                return province;

            try
            {
                await UOW.Begin();
                await UOW.ProvinceRepository.Delete(province.Id);
                await UOW.Commit();
                return await Get(province.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }

        public async Task<List<Province>> List(ProvinceFilter provinceFilter)
        {
            return await UOW.ProvinceRepository.List(provinceFilter);
        }

        public async Task<Province> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            Province Province = await UOW.ProvinceRepository.Get(Id);
            return Province;
        }

        public async Task<Province> Update(Province province)
        {
            if (!await ProvinceValidator.Update(province))
                return province;

            try
            {
                await UOW.Begin();
                await UOW.ProvinceRepository.Update(province);
                await UOW.Commit();
                return await Get(province.Id);
            }
            catch (Exception ex)
            {
                await UOW.Rollback();
                throw new MessageException(ex);
            }
        }
    }
}
