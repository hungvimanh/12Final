using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IUniversityAdmissionRepository
    {
        Task<UniversityAdmission> Get(Guid FormId);
        Task<bool> Update(UniversityAdmission universityAdmission);
    }
    public class UniversityAdmissionRepository : IUniversityAdmissionRepository
    {
        private readonly TFContext tFContext;
        public UniversityAdmissionRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        public async Task<UniversityAdmission> Get(Guid FormId)
        {
            UniversityAdmission universityAdmission = await tFContext.Form.Where(g => g.Id.Equals(FormId)).Select(g => new UniversityAdmission
            {
                Id = g.Id,
                Area = g.Area,
                Connected = g.Connected,
                GraduateYear = g.GraduateYear,
                PriorityType = g.PriorityType,
                University_Majorses = g.University_Majors.Select(a => new University_Majors
                {
                    Id = a.Id,
                    MajorsId = a.MajorsId,
                    MajorsCode = a.Majors.Code,
                    MajorsName = a.Majors.Name,
                    UniversityId = a.UniversityId,
                    UniversityCode = a.University.Code,
                    UniversityName = a.University.Name,
                    SubjectGroupType = a.SubjectGroupType,
                    Benchmark = a.Benchmark,
                    Year = a.Year
                }).ToList()
            }).FirstOrDefaultAsync();

            return universityAdmission;
        }

        public async Task<bool> Update(UniversityAdmission universityAdmission)
        {
            await tFContext.Form.Where(g => g.Id.Equals(universityAdmission.Id)).UpdateFromQueryAsync(g => new FormDAO
            {
                Area = universityAdmission.Area,
                Connected = universityAdmission.Connected,
                GraduateYear = universityAdmission.GraduateYear,
                PriorityType = universityAdmission.PriorityType,
                University_Majors = universityAdmission.University_Majorses.Select(a => new University_MajorsDAO
                {
                    Id = a.Id,
                    MajorsId = a.MajorsId,
                    UniversityId = a.UniversityId,
                    SubjectGroupType = a.SubjectGroupType,
                    Benchmark = a.Benchmark,
                    Year = a.Year
                }).ToList()
            });

            return true;
        }
    }
}
