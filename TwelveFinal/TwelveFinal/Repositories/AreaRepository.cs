using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IAreaRepository
    {
        Task<Area> Get(Guid Id);
        Task<List<Area>> List(AreaFilter areaFilter);
        Task<int> Count(AreaFilter areaFilter);
    }
    public class AreaRepository : IAreaRepository
    {
        private readonly TFContext tFContext;
        public AreaRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        private IQueryable<AreaDAO> DynamicFilter(IQueryable<AreaDAO> query, AreaFilter areaFilter)
        {
            if (areaFilter == null)
                return query.Where(q => 1 == 0);

            if (areaFilter.Id != null)
                query = query.Where(q => q.Id, areaFilter.Id);
            if (areaFilter.Code != null)
                query = query.Where(q => q.Code, areaFilter.Code);
            if (areaFilter.Name != null)
                query = query.Where(q => q.Name, areaFilter.Name);

            return query;
        }
        private IQueryable<AreaDAO> DynamicOrder(IQueryable<AreaDAO> query, AreaFilter areaFilter)
        {
            switch (areaFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (areaFilter.OrderBy)
                    {
                        case AreaOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        case AreaOrder.Name:
                            query = query.OrderBy(q => q.Name);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (areaFilter.OrderBy)
                    {
                        case AreaOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
                            break;
                        case AreaOrder.Name:
                            query = query.OrderByDescending(q => q.Name);
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
            query = query.Skip(areaFilter.Skip).Take(areaFilter.Take);
            return query;
        }
        private async Task<List<Area>> DynamicSelect(IQueryable<AreaDAO> query)
        {

            List<Area> areas = await query.Select(q => new Area()
            {
                Id = q.Id,
                Code = q.Code,
                Name = q.Name
            }).ToListAsync();
            return areas;
        }

        public async Task<int> Count(AreaFilter areaFilter)
        {
            IQueryable<AreaDAO> areaDAOs = tFContext.Area;
            areaDAOs = DynamicFilter(areaDAOs, areaFilter);
            return await areaDAOs.CountAsync();
        }

        public async Task<List<Area>> List(AreaFilter areaFilter)
        {
            if (areaFilter == null) return new List<Area>();
            IQueryable<AreaDAO> areaDAOs = tFContext.Area;
            areaDAOs = DynamicFilter(areaDAOs, areaFilter);
            areaDAOs = DynamicOrder(areaDAOs, areaFilter);
            var areas = await DynamicSelect(areaDAOs);
            return areas;
        }

        public async Task<Area> Get(Guid Id)
        {
            Area area = await tFContext.Area.Where(a => a.Id.Equals(Id)).Select(a => new Area
            {
                Id = a.Id,
                Code = a.Code,
                Name = a.Name
            }).FirstOrDefaultAsync();

            return area;
        }
    }
}
