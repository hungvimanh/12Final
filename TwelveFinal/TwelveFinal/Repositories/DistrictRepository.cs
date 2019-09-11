using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IDistrictRepository
    {
        Task<bool> Create(District district);
        Task<District> Get(Guid Id);
        Task<bool> Update(District district);
        Task<bool> Delete(Guid Id);
    }
    public class DistrictRepository : IDistrictRepository
    {
        private readonly TFContext tFContext;
        public DistrictRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(District district)
        {
            DistrictDAO DistrictDAO = new DistrictDAO
            {
                Id = district.Id,
                Code = district.Code,
                Name = district.Name,
                ProvinceId = district.ProvinceId
            };

            tFContext.District.Add(DistrictDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Town.Where(t => t.DistrictId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.District.Where(d => d.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<District> Get(Guid Id)
        {
            District District = await tFContext.District.Where(d => d.Id.Equals(Id)).Select(d => new District
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name,
                ProvinceId = d.ProvinceId,
                ProvinceCode = d.Province.Code,
                ProvinceName = d.Province.Name,
                Towns = tFContext.Town.Where(t => t.DistrictId.Equals(Id)).Select(t => new Town
                {
                    Id = t.Id,
                    Code = t.Code,
                    Name = t.Name,
                    DistrictId = t.DistrictId,
                    DistrictCode = t.District.Code,
                    DistrictName = t.District.Name
                }).ToList()
            }).FirstOrDefaultAsync();

            return District;
        }

        public async Task<bool> Update(District district)
        {
            await tFContext.District.Where(t => t.Id.Equals(district.Id)).UpdateFromQueryAsync(t => new DistrictDAO
            {
                Code = district.Code,
                Name = district.Name,
                ProvinceId = district.ProvinceId
            });

            return true;
        }
    }
}
