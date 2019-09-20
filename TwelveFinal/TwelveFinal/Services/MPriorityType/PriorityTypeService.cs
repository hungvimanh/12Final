using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MPriorityType
{
    public interface IPriorityTypeService : IServiceScoped
    {
        Task<PriorityType> Get(Guid Id);
        Task<int> Count(PriorityTypeFilter priorityTypeFilter);
        Task<List<PriorityType>> List(PriorityTypeFilter priorityTypeFilter);
    }
    public class PriorityTypeService : IPriorityTypeService
    {
        private IUOW UOW;
        public PriorityTypeService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<int> Count(PriorityTypeFilter priorityTypeFilter)
        {
            return await UOW.PriorityTypeRepository.Count(priorityTypeFilter);
        }

        public async Task<PriorityType> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            var PriorityType = await UOW.PriorityTypeRepository.Get(Id);
            return PriorityType;
        }

        public async Task<List<PriorityType>> List(PriorityTypeFilter priorityTypeFilter)
        {
            return await UOW.PriorityTypeRepository.List(priorityTypeFilter);
        }
    }
}
