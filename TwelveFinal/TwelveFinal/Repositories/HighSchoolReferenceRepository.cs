using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IHighSchoolReferenceRepository
    {
        Task<bool> Create(HighSchoolReference highSchoolReference);
        Task<HighSchoolReference> Get(Guid Id);
        Task<bool> Update(HighSchoolReference highSchoolReference);
        Task<bool> Delete(Guid Id);
    }
    public class HighSchoolReferenceRepository : IHighSchoolReferenceRepository
    {
        private readonly TFContext tFContext;
        public HighSchoolReferenceRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(HighSchoolReference highSchoolReference)
        {
            HighSchoolReferenceDAO highSchoolReferenceDAO = new HighSchoolReferenceDAO
            {
                Id = highSchoolReference.Id,
                Grade10Id = highSchoolReference.Grade10Id,
                Grade11Id = highSchoolReference.Grade11Id,
                Grade12Id = highSchoolReference.Grade12Id

            };

            tFContext.HighSchoolReference.Add(highSchoolReferenceDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.HighSchoolReference.Where(h => h.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<HighSchoolReference> Get(Guid Id)
        {
            HighSchoolReference HighSchoolReference = await tFContext.HighSchoolReference.Where(p => p.Id.Equals(Id)).Select(p => new HighSchoolReference
            {
                Id = p.Id,
                Grade10Id = p.Grade10Id,
                Grade11Id = p.Grade11Id,
                Grade12Id = p.Grade12Id,
            }).FirstOrDefaultAsync();

            return HighSchoolReference;
        }

        public async Task<bool> Update(HighSchoolReference highSchoolReference)
        {
            await tFContext.HighSchoolReference.Where(t => t.Id.Equals(highSchoolReference.Id)).UpdateFromQueryAsync(t => new HighSchoolReferenceDAO
            {
                Id = highSchoolReference.Id,
                Grade10Id = highSchoolReference.Grade10Id,
                Grade11Id = highSchoolReference.Grade11Id,
                Grade12Id = highSchoolReference.Grade12Id
            });

            return true;
        }
    }
}
