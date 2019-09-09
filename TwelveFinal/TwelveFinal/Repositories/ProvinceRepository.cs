using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IProvinceRepository
    {
        Task<bool> Create(Province province);
        Task<Province> Get(Guid Id);
        Task<bool> Update(Province province);
        Task<bool> Delete(Guid Id);
    }
    public class ProvinceRepository : IProvinceRepository
    {
        private readonly TFContext tFContext;
        public ProvinceRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(Province province)
        {
            ProvinceDAO provinceDAO = new ProvinceDAO
            {
                Id = province.Id,
                Code = province.Code,
                Name = province.Name,
                Districts = province.Districts.Select(d => new DistrictDAO
                {
                    Id = d.Id,
                    Code = d.Code,
                    Name = d.Name,
                    ProvinceId = d.ProvinceId,
                    Towns = d.Towns.Select(t => new TownDAO
                    {
                        Id = t.Id,
                        Code = t.Code,
                        Name = t.Name,
                        DistrictId = t.DistrictId
                    }).ToList()
                }).ToList()
            };

            tFContext.Province.Add(provinceDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Town.Where(t => t.District.ProvinceId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.District.Where(d => d.ProvinceId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.Province.Where(p => p.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Province> Get(Guid Id)
        {
            Province province = await tFContext.Province.Where(p => p.Id.Equals(Id)).Select(p => new Province
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Districts = tFContext.District.Where(d => d.ProvinceId.Equals(Id)).Select(d => new District
                {
                    Id = d.Id,
                    Code = d.Code,
                    Name = d.Name,
                    ProvinceId = d.ProvinceId,
                    ProvinceCode = d.Province.Code,
                    ProvinceName = d.Province.Name,
                    Towns = tFContext.Town.Where(t => t.District.ProvinceId.Equals(Id)).Select(t => new Town
                    {
                        Id = t.Id,
                        Code = t.Code,
                        Name = t.Name,
                        DistrictId = t.DistrictId,
                        DistrictCode = t.District.Code,
                        DistrictName = t.District.Name
                    }).ToList()
                }).ToList()
            }).FirstOrDefaultAsync();

            return province;
        }

        public async Task<bool> Update(Province Province)
        {
            await tFContext.Province.Where(t => t.Id.Equals(Province.Id)).UpdateFromQueryAsync(t => new ProvinceDAO
            {
                Id = Province.Id,
                Code = Province.Code,
                Name = Province.Name,
            });

            return true;
        }
    }
}
