using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IRegisterInformationRepository
    {
        Task<bool> Create(RegisterInformation register);
        Task<RegisterInformation> Get(Guid Id);
        Task<bool> Update(RegisterInformation register);
        Task<bool> Delete(Guid Id);
    }
    public class RegisterInformationRepository : IRegisterInformationRepository
    {
        private readonly TFContext tFContext;
        public RegisterInformationRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(RegisterInformation register)
        {
            RegisterInformationDAO RegisterDAO = new RegisterInformationDAO
            {
                Id = register.Id,
                Passed = register.Passed,
                ResultForUniversity = register.ResultForUniversity,
                StudyAtHighSchool = register.StudyAtHighSchool,
                ContestGroupId = register.ContestGroupId,
                ContestUnitId = register.ContestUnitId,
                TestId = register.TestId,
            };

            tFContext.RegisterInformation.Add(RegisterDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Form.Where(f => f.RegisterInformationId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.RegisterInformation.Where(r => r.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<RegisterInformation> Get(Guid Id)
        {
            RegisterInformation register = await tFContext.RegisterInformation.Where(r => r.Id.Equals(Id)).Select(r => new RegisterInformation
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

        public async Task<bool> Update(RegisterInformation register)
        {
            await tFContext.RegisterInformation.Where(r => r.Id.Equals(register.Id)).UpdateFromQueryAsync(r => new RegisterInformationDAO
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
