using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;

namespace TwelveFinal.Services.MMajors
{
    public interface IMajorsValidator : IServiceScoped
    {
        Task<bool> Create(Majors Majors);
        Task<bool> Update(Majors Majors);
        Task<bool> Delete(Majors Majors);
    }
    public class MajorsValidator : IMajorsValidator
    {
        public Task<bool> Create(Majors Majors)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Majors Majors)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Majors Majors)
        {
            throw new NotImplementedException();
        }
    }
}
