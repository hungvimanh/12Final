using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IHighSchoolRepository
    {
        Task<bool> Create(HighSchool highSchool);
        Task<HighSchool> Get(Guid Id);
        Task<bool> Update(HighSchool highSchool);
        Task<bool> Delete(Guid Id);
    }
    public class HighSchoolRepository : IHighSchoolRepository
    {
        private readonly TFContext tFContext;
        public HighSchoolRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(HighSchool highSchool)
        {
            HighSchoolDAO HighSchoolDAO = new HighSchoolDAO
            {
                Id = highSchool.Id,
                Code = highSchool.Code,
                Name = highSchool.Name,
                ProvinceId = highSchool.ProvinceId,
                
            };

            tFContext.HighSchool.Add(HighSchoolDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.HighSchoolReference.Where(h => h.Grade10Id.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.HighSchoolReference.Where(h => h.Grade11Id.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.HighSchoolReference.Where(h => h.Grade12Id.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.HighSchool.Where(h => h.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<HighSchool> Get(Guid Id)
        {
            HighSchool HighSchool = await tFContext.HighSchool.Where(p => p.Id.Equals(Id)).Select(p => new HighSchool
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                ProvinceId = p.ProvinceId,
                ProvinceCode = p.Province.Code,
                ProvinceName = p.Province.Name 
            }).FirstOrDefaultAsync();

            return HighSchool;
        }

        public async Task<bool> Update(HighSchool highSchool)
        {
            await tFContext.HighSchool.Where(t => t.Id.Equals(highSchool.Id)).UpdateFromQueryAsync(t => new HighSchoolDAO
            {
                Id = highSchool.Id,
                Code = highSchool.Code,
                Name = highSchool.Name,
                ProvinceId = highSchool.ProvinceId
            });

            return true;
        }
    }
}
