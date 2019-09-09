using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IRegisterRepository
    {
        Task<bool> Create(Register register);
        Task<Register> Get(Guid Id);
        Task<bool> Update(Register register);
        Task<bool> Delete(Guid Id);
    }
    public class RegisterRepository : IRegisterRepository
    {
        private readonly TFContext tFContext;
        public RegisterRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(Register register)
        {
            RegisterDAO RegisterDAO = new RegisterDAO
            {
                Id = register.Id,
                Passed = register.Passed,
                ResultForUniversity = register.ResultForUniversity,
                StudyAtHighSchool = register.StudyAtHighSchool,
                ContestGroupId = register.ContestGroupId,
                ContestUnitId = register.ContestUnitId,
                TestId = register.TestId,
            };

            tFContext.Register.Add(RegisterDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Form.Where(f => f.RegisterInformationId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.Register.Where(r => r.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<Register> Get(Guid Id)
        {
            Register register = await tFContext.Register.Where(r => r.Id.Equals(Id)).Select(r => new Register
            {
                Id = r.Id,
                Passed = r.Passed,
                ResultForUniversity = r.ResultForUniversity,
                StudyAtHighSchool = r.StudyAtHighSchool,
                ContestGroupId = r.ContestGroupId,
                ContestUnitId = r.ContestUnitId,
                TestId = r.TestId,
            }).FirstOrDefaultAsync();

            return register;
        }

        public async Task<bool> Update(Register register)
        {
            await tFContext.Register.Where(r => r.Id.Equals(register.Id)).UpdateFromQueryAsync(r => new RegisterDAO
            {
                Id = register.Id,
                Passed = register.Passed,
                ResultForUniversity = register.ResultForUniversity,
                StudyAtHighSchool = register.StudyAtHighSchool,
                ContestGroupId = register.ContestGroupId,
                ContestUnitId = register.ContestUnitId,
                TestId = register.TestId,
            });

            return true;
        }
    }
}
