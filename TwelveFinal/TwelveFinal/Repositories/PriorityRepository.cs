using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IPriorityTypeRepository
    {
        Task<PriorityType> Get(Guid Id);
        Task<List<PriorityType>> List(PriorityTypeFilter priorityTypeFilter);
        Task<int> Count(PriorityTypeFilter priorityTypeFilter);
    }
    public class PriorityTypeRepository : IPriorityTypeRepository
    {
        private readonly TFContext tFContext;
        public PriorityTypeRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        private IQueryable<PriorityTypeDAO> DynamicFilter(IQueryable<PriorityTypeDAO> query, PriorityTypeFilter priorityTypeFilter)
        {
            if (priorityTypeFilter == null)
                return query.Where(q => 1 == 0);

            if (priorityTypeFilter.Id != null)
                query = query.Where(q => q.Id, priorityTypeFilter.Id);
            if (priorityTypeFilter.Code != null)
                query = query.Where(q => q.Code, priorityTypeFilter.Code);

            return query;
        }
        private IQueryable<PriorityTypeDAO> DynamicOrder(IQueryable<PriorityTypeDAO> query, PriorityTypeFilter priorityTypeFilter)
        {
            switch (priorityTypeFilter.OrderType)
            {
                case OrderType.ASC:
                    switch (priorityTypeFilter.OrderBy)
                    {
                        case PriorityTypeOrder.Code:
                            query = query.OrderBy(q => q.Code);
                            break;
                        default:
                            query = query.OrderBy(q => q.CX);
                            break;
                    }
                    break;
                case OrderType.DESC:
                    switch (priorityTypeFilter.OrderBy)
                    {
                        case PriorityTypeOrder.Code:
                            query = query.OrderByDescending(q => q.Code);
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
            query = query.Skip(priorityTypeFilter.Skip).Take(priorityTypeFilter.Take);
            return query;
        }
        private async Task<List<PriorityType>> DynamicSelect(IQueryable<PriorityTypeDAO> query)
        {

            List<PriorityType> priorityTypes = await query.Select(q => new PriorityType()
            {
                Id = q.Id,
                Code = q.Code,
            }).ToListAsync();
            return priorityTypes;
        }

        public async Task<int> Count(PriorityTypeFilter priorityTypeFilter)
        {
            IQueryable<PriorityTypeDAO> priorityTypeDAOs = tFContext.PriorityType;
            priorityTypeDAOs = DynamicFilter(priorityTypeDAOs, priorityTypeFilter);
            return await priorityTypeDAOs.CountAsync();
        }

        public async Task<List<PriorityType>> List(PriorityTypeFilter priorityTypeFilter)
        {
            if (priorityTypeFilter == null) return new List<PriorityType>();
            IQueryable<PriorityTypeDAO> priorityTypeDAOs = tFContext.PriorityType;
            priorityTypeDAOs = DynamicFilter(priorityTypeDAOs, priorityTypeFilter);
            priorityTypeDAOs = DynamicOrder(priorityTypeDAOs, priorityTypeFilter);
            var priorityTypes = await DynamicSelect(priorityTypeDAOs);
            return priorityTypes;
        }

        public async Task<PriorityType> Get(Guid Id)
        {
            PriorityType priorityType = await tFContext.PriorityType.Where(p => p.Id.Equals(Id)).Select(p => new PriorityType
            {
                Id = p.Id,
                Code = p.Code,
            }).FirstOrDefaultAsync();

            return priorityType;
        }
    }
}
