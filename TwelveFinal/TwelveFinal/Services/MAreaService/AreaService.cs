using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories;

namespace TwelveFinal.Services.MAreaService
{
    public interface IAreaService : IServiceScoped
    {
        Task<Area> Get(Guid Id);
        Task<int> Count(AreaFilter areaFilter);
        Task<List<Area>> List(AreaFilter areaFilter);
    }
    public class AreaService : IAreaService
    {
        private IUOW UOW;
        public AreaService(IUOW UOW)
        {
            this.UOW = UOW;
        }

        public async Task<int> Count(AreaFilter areaFilter)
        {
            return await UOW.AreaRepository.Count(areaFilter);
        }

        public async Task<Area> Get(Guid Id)
        {
            if (Id == Guid.Empty) return null;
            var Area = await UOW.AreaRepository.Get(Id);
            return Area;
        }

        public async Task<List<Area>> List(AreaFilter areaFilter)
        {
            return await UOW.AreaRepository.List(areaFilter);
        }
    }
}
