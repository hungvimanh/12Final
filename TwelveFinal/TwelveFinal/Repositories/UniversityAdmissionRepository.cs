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
                FormDetails = g.FormDetails.Select(d => new FormDetail
                {
                    Id = d.Id,
                    FormId = d.FormId,
                    MajorsId = d.MajorsId,
                    MajorsCode = d.Majors.Code,
                    MajorsName = d.Majors.Name,
                    UniversityId = d.UniversityId,
                    UniversityCode = d.University.Code,
                    UniversityName = d.University.Name,
                    UniversityAddress = d.University.Address,
                    SubjectGroupId = d.SubjectGroupId,
                    SubjectGroupCode = d.SubjectGroup.Code,
                    SubjectGroupName = d.SubjectGroup.Name,
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
                FormDetails = universityAdmission.FormDetails.Select(d => new FormDetailDAO
                {
                    Id = d.Id,
                    MajorsId = d.MajorsId,
                    UniversityId = d.UniversityId,
                    SubjectGroupId = d.SubjectGroupId,
                    FormId = d.FormId
                }).ToList()
            });

            return true;
        }
    }
}
