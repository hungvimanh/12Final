using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IUniversity_MajorsRepository
    {
        Task<bool> Create(University_Majors university_Majors);
        Task<List<University_Majors>> List(University_MajorsFilter university_MajorsFilter);
        Task<int> Count(University_MajorsFilter university_MajorsFilter);
        Task<University_Majors> Get(Guid Id);
        Task<bool> Update(University_Majors university_Majors);
        Task<bool> Delete(University_Majors university_Majors);
    }
    public class University_MajorsRepository : IUniversity_MajorsRepository
    {
        private readonly TFContext tFContext;
        public University_MajorsRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        private IQueryable<University_MajorsDAO> DynamicFilter(IQueryable<University_MajorsDAO> query, University_MajorsFilter university_MajorsFilter)
        {
            if (university_MajorsFilter == null)
                return query.Where(q => 1 == 0);
            if (university_MajorsFilter.Id != null)
                query = query.Where(q => q.Id, university_MajorsFilter.Id);
            if (university_MajorsFilter.MajorsId != null)
                query = query.Where(q => q.MajorsId, university_MajorsFilter.MajorsId);
            if (university_MajorsFilter.UniversityId != null)
                query = query.Where(q => q.UniversityId, university_MajorsFilter.UniversityId);
            if (university_MajorsFilter.MajorsCode != null)
                query = query.Where(q => q.Majors.Code, university_MajorsFilter.MajorsCode);
            if (university_MajorsFilter.MajorsName != null)
                query = query.Where(q => q.Majors.Name, university_MajorsFilter.MajorsName);
            if (university_MajorsFilter.UniversityCode != null)
                query = query.Where(q => q.University.Code, university_MajorsFilter.UniversityCode);
            if (university_MajorsFilter.UniversityName != null)
                query = query.Where(q => q.University.Name, university_MajorsFilter.UniversityName);
            if (university_MajorsFilter.Benchmark != null)
                query = query.Where(q => q.Benchmark, university_MajorsFilter.Benchmark);
            return query;
        }
        private IQueryable<University_MajorsDAO> DynamicOrder(IQueryable<University_MajorsDAO> query, University_MajorsFilter university_MajorsFilter)
        {
            switch (university_MajorsFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (university_MajorsFilter.OrderBy)
                    {
                        case University_MajorsOrder.MajorsCode:
                            query = query.OrderBy(q => q.Majors.Code);
                            break;
                        case University_MajorsOrder.UniversityCode:
                            query = query.OrderBy(q => q.University.Code);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (university_MajorsFilter.OrderBy)
                    {
                        case University_MajorsOrder.MajorsCode:
                            query = query.OrderByDescending(q => q.Majors.Code);
                            break;
                        case University_MajorsOrder.UniversityCode:
                            query = query.OrderByDescending(q => q.University.Name);
                            break;
                        default:
                            query = query.OrderByDescending(q => q.CX);
                            break;
                    }
                    break;
                default:
                    query = query.OrderBy(q => q.CX);
                    break;
            }
            query = query.Skip(university_MajorsFilter.Skip).Take(university_MajorsFilter.Take);
            return query;
        }
        private async Task<List<University_Majors>> DynamicSelect(IQueryable<University_MajorsDAO> query)
        {

            List<University_Majors> university_Majorss = await query.Select(q => new University_Majors()
            {
                Id = q.Id,
                MajorsId = q.MajorsId,
                MajorsCode = q.Majors.Code,
                MajorsName = q.Majors.Name,
                UniversityId = q.UniversityId,
                UniversityCode = q.University.Code,
                UniversityName = q.University.Name,
                Benchmark = q.Benchmark
            }).ToListAsync();
            return university_Majorss;
        }

        public async Task<int> Count(University_MajorsFilter university_MajorsFilter)
        {
            IQueryable<University_MajorsDAO> university_MajorsDAOs = tFContext.University_Majors;
            university_MajorsDAOs = DynamicFilter(university_MajorsDAOs, university_MajorsFilter);
            return await university_MajorsDAOs.CountAsync();
        }

        public async Task<List<University_Majors>> List(University_MajorsFilter university_MajorsFilter)
        {
            if (university_MajorsFilter == null) return new List<University_Majors>();
            IQueryable<University_MajorsDAO> university_MajorsDAOs = tFContext.University_Majors;
            university_MajorsDAOs = DynamicFilter(university_MajorsDAOs, university_MajorsFilter);
            university_MajorsDAOs = DynamicOrder(university_MajorsDAOs, university_MajorsFilter);
            var university_Majorss = await DynamicSelect(university_MajorsDAOs);
            return university_Majorss;
        }

        public async Task<bool> Create(University_Majors university_Majors)
        {
            University_MajorsDAO university_MajorsDAO = new University_MajorsDAO
            {
                Id = university_Majors.Id,
                MajorsId = university_Majors.MajorsId,
                UniversityId = university_Majors.UniversityId,
                Benchmark = university_Majors.Benchmark,
            };

            tFContext.University_Majors.Add(university_MajorsDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(University_Majors university_Majors)
        {
            await tFContext.University_Majors.Where(m => m.MajorsId.Equals(university_Majors.MajorsId) && m.UniversityId.Equals(university_Majors.UniversityId)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<University_Majors> Get(Guid Id)
        {
            University_Majors University_Majors = await tFContext.University_Majors.Where(u => u.MajorsId.Equals(Id)).Select(u => new University_Majors
            {
                Id = u.Id,
                UniversityId = u.UniversityId,
                UniversityCode = u.University.Code,
                UniversityName = u.University.Name,
                MajorsId = u.MajorsId,
                MajorsCode = u.Majors.Code,
                MajorsName = u.Majors.Name,
                Benchmark = u.Benchmark
            }).FirstOrDefaultAsync();

            return University_Majors;
        }

        public async Task<bool> Update(University_Majors University_Majors)
        {
            await tFContext.University_Majors.Where(u => u.Id.Equals(University_Majors.Id)).UpdateFromQueryAsync(u => new University_MajorsDAO
            {
                MajorsId = University_Majors.MajorsId,
                UniversityId = University_Majors.UniversityId,
                Benchmark = University_Majors.Benchmark
            });

            return true;
        }
    }
}
