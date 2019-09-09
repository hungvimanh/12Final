using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IGraduationRepository
    {
        Task<bool> Create(Graduation graduation);
        Task<Graduation> Get(Guid Id);
        Task<bool> Update(Graduation graduation);
        Task<bool> Delete(Guid Id);
    }
    public class GraduationRepository : IGraduationRepository
    {
        private readonly TFContext tFContext;
        public GraduationRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(Graduation graduation)
        {
            GraduationDAO GraduationDAO = new GraduationDAO
            {
                Id = graduation.Id,
                Mark = graduation.Mark,
                ExceptLanguages = graduation.ExceptLanguages,
            };

            tFContext.Graduation.Add(GraduationDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Form.Where(f => f.GraduationInformationId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.Graduation.Where(g => g.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Graduation> Get(Guid Id)
        {
            Graduation graduation = await tFContext.Graduation.Where(g => g.Id.Equals(Id)).Select(g => new Graduation
            {
                Id = g.Id,
                Mark = g.Mark,
                ExceptLanguages = g.ExceptLanguages,
            }).FirstOrDefaultAsync();

            return graduation;
        }

        public async Task<bool> Update(Graduation graduation)
        {
            await tFContext.Graduation.Where(g => g.Id.Equals(graduation.Id)).UpdateFromQueryAsync(g => new GraduationDAO
            {
                Id = graduation.Id,
                Mark = graduation.Mark,
                ExceptLanguages = graduation.ExceptLanguages,
            });

            return true;
        }
    }
}
