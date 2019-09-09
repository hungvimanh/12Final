using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface ITownRepository
    {
        Task<bool> Create(Town town);
        Task<Town> Get(Guid Id);
        Task<bool> Update(Town town);
        Task<bool> Delete(Guid Id);
    }
    public class TownRepository : ITownRepository
    {
        private readonly TFContext tFContext;
        public TownRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(Town town)
        {
            TownDAO TownDAO = new TownDAO
            {
                Id = town.Id,
                Code = town.Code,
                Name = town.Name,
                DistrictId = town.DistrictId,
            };

            tFContext.Town.Add(TownDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.PersonalInformation.Where(p => p.TownId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.Town.Where(t => t.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Town> Get(Guid Id)
        {
            Town Town = await tFContext.Town.Where(t => t.Id.Equals(Id)).Select(t => new Town
            {
                Id = t.Id,
                Code = t.Code,
                Name = t.Name,
                DistrictId = t.DistrictId,
                DistrictCode = t.District.Code,
                DistrictName = t.District.Name
            }).FirstOrDefaultAsync();

            return Town;
        }

        public async Task<bool> Update(Town town)
        {
            await tFContext.Town.Where(t => t.Id.Equals(town.Id)).UpdateFromQueryAsync(t => new TownDAO
            {
                Id = town.Id,
                Code = town.Code,
                Name = town.Name,
                DistrictId = town.DistrictId
            });

            return true;
        }
    }
}
