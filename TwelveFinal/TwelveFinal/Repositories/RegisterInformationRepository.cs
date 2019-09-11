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
        Task<RegisterInformation> Get(Guid FormId);
        Task<bool> Update(RegisterInformation register);
    }
    public class RegisterInformationRepository : IRegisterInformationRepository
    {
        private readonly TFContext tFContext;
        public RegisterInformationRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        public async Task<RegisterInformation> Get(Guid FormId)
        {
            RegisterInformation register = await tFContext.Form.Where(r => r.Id.Equals(FormId)).Select(r => new RegisterInformation
            {
                Id = r.Id,
                Graduated = r.Graduated,
                ResultForUniversity = r.ResultForUniversity,
                StudyAtHighSchool = r.StudyAtHighSchool,
                ClusterContestId = r.ClusterContestId,
                RegisterPlaceOfExamId = r.RegisterPlaceOfExamId,
                Maths = r.Maths,
                Literature = r.Literature,
                Languages = r.Languages,
                Physics = r.Physics,
                Chemistry = r.Chemistry,
                Biology = r.Biology,
                History = r.History,
                Geography = r.Geography,
                CivicEducation = r.CivicEducation,
                NaturalSciences = r.NaturalSciences,
                SocialSciences = r.SocialSciences
            }).FirstOrDefaultAsync();

            return register;
        }

        public async Task<bool> Update(RegisterInformation register)
        {
            await tFContext.Form.Where(r => r.Id.Equals(register.Id)).UpdateFromQueryAsync(r => new FormDAO
            {
                Graduated = register.Graduated,
                ResultForUniversity = register.ResultForUniversity,
                StudyAtHighSchool = register.StudyAtHighSchool,
                ClusterContestId = register.ClusterContestId,
                RegisterPlaceOfExamId = register.RegisterPlaceOfExamId,
                Maths = register.Maths,
                Literature = register.Literature,
                Languages = register.Languages,
                Physics = register.Physics,
                Chemistry = register.Chemistry,
                Biology = register.Biology,
                History = register.History,
                Geography = register.Geography,
                CivicEducation = register.CivicEducation,
                NaturalSciences = register.NaturalSciences,
                SocialSciences = register.SocialSciences
            });

            return true;
        }
    }
}
