using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface ISubjectGroupRepository
    {
        Task<bool> Create(SubjectGroup subjectGroup);
        Task<SubjectGroup> Get(Guid Id);
        //Task<List<SubjectGroup>> List(University_Majors university_Majors);
        Task<bool> Update(SubjectGroup subjectGroup);
        Task<bool> Delete(Guid Id);
    }
    public class SubjectGroupRepository : ISubjectGroupRepository
    {
        private readonly TFContext tFContext;
        public SubjectGroupRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(SubjectGroup subjectGroup)
        {
            SubjectGroupDAO subjectGroupDAO = new SubjectGroupDAO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name
            };

            tFContext.SubjectGroup.Add(subjectGroupDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.University_Majors.Where(t => t.SubjectGroupId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.SubjectGroup.Where(d => d.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<SubjectGroup> Get(Guid Id)
        {
            SubjectGroup subjectGroup = await tFContext.SubjectGroup.Where(d => d.Id.Equals(Id)).Select(d => new SubjectGroup
            {
                Id = d.Id,
                Code = d.Code,
                Name = d.Name
            }).FirstOrDefaultAsync();

            return subjectGroup;
        }

        //public Task<List<SubjectGroup>> List(University_Majors university_Majors)
        //{
            
        //}

        public async Task<bool> Update(SubjectGroup subjectGroup)
        {
            await tFContext.SubjectGroup.Where(t => t.Id.Equals(subjectGroup.Id)).UpdateFromQueryAsync(t => new SubjectGroupDAO
            {
                Id = subjectGroup.Id,
                Code = subjectGroup.Code,
                Name = subjectGroup.Name
            });

            return true;
        }
    }
}
