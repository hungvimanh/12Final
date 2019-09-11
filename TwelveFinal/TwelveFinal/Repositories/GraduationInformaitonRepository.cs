using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IGraduationInformationRepository
    {
        Task<GraduationInformation> Get(Guid Id);
        Task<bool> Update(GraduationInformation graduation);
    }
    public class GraduationInformationRepository : IGraduationInformationRepository
    {
        private readonly TFContext tFContext;
        public GraduationInformationRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        public async Task<GraduationInformation> Get(Guid Id)
        {
            GraduationInformation graduation = await tFContext.Form.Where(g => g.Id.Equals(Id)).Select(g => new GraduationInformation
            {
                Id = g.Id,
                Mark = g.Mark,
                ExceptLanguages = g.ExceptLanguages,
                ReserveMaths = g.ReserveMaths,
                ReserveLiterature = g.ReserveLiterature,
                ReserveLanguages = g.ReserveLanguages,
                ReservePhysics = g.ReservePhysics,
                ReserveChemistry = g.ReserveChemistry,
                ReserveBiology = g.ReserveBiology,
                ReserveHistory = g.ReserveHistory,
                ReserveGeography = g.ReserveGeography,
                ReserveCivicEducation = g.ReserveCivicEducation,
            }).FirstOrDefaultAsync();

            return graduation;
        }

        public async Task<bool> Update(GraduationInformation graduation)
        {
            await tFContext.Form.Where(g => g.Id.Equals(graduation.Id)).UpdateFromQueryAsync(g => new FormDAO
            {
                Mark = graduation.Mark,
                ExceptLanguages = graduation.ExceptLanguages,
                ReserveMaths = graduation.ReserveMaths,
                ReserveLiterature = graduation.ReserveLiterature,
                ReserveLanguages = graduation.ReserveLanguages,
                ReservePhysics = graduation.ReservePhysics,
                ReserveChemistry = graduation.ReserveChemistry,
                ReserveBiology = graduation.ReserveBiology,
                ReserveHistory = graduation.ReserveHistory,
                ReserveGeography = graduation.ReserveGeography,
                ReserveCivicEducation = graduation.ReserveCivicEducation,
            });

            return true;
        }
    }
}
