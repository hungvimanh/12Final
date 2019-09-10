using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwelveFinal.Entities;
using TwelveFinal.Repositories.Models;

namespace TwelveFinal.Repositories
{
    public interface IPersonalInformationRepository
    {
        Task<bool> Create(PersonalInformation personalInformation);
        Task<PersonalInformation> Get(Guid Id);
        Task<bool> Update(PersonalInformation personalInformation);
        Task<bool> Delete(Guid Id);
    }
    public class PersonalInformationRepository : IPersonalInformationRepository
    {
        private readonly TFContext tFContext;
        public PersonalInformationRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }
        public async Task<bool> Create(PersonalInformation personalInformation)
        {
            PersonalInformationDAO personalInformationDAO = new PersonalInformationDAO
            {
                Id = personalInformation.Id,
                FullName = personalInformation.FullName,
                Address = personalInformation.Address,
                Dob = personalInformation.Dob,
                PlaceOfBirth = personalInformation.PlaceOfBirth,
                Email = personalInformation.Email,
                Gender = personalInformation.Gender,
                HighSchoolId = personalInformation.HighSchoolId,
                Identify = personalInformation.Identify,
                IsPermanentResidenceMore18 = personalInformation.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = personalInformation.IsPermanentResidenceSpecialMore18,
                Nation = personalInformation.Nation,
                Phone = personalInformation.Phone,
                TownId = personalInformation.TownId
            };

            tFContext.PersonalInformation.Add(personalInformationDAO);
            await tFContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Guid Id)
        {
            await tFContext.Form.Where(f => f.PersonalInfomartionId.Equals(Id)).DeleteFromQueryAsync();
            await tFContext.PersonalInformation.Where(b => b.Id.Equals(Id)).DeleteFromQueryAsync();
            return true;
        }

        public async Task<PersonalInformation> Get(Guid Id)
        {
            PersonalInformation personalInformation = await tFContext.PersonalInformation.Where(p => p.Id.Equals(Id)).Select(p => new PersonalInformation
            {
                Id = p.Id,
                FullName = p.FullName,
                Address = p.Address,
                Dob = p.Dob,
                PlaceOfBirth = p.PlaceOfBirth,
                Email = p.Email,
                Gender = p.Gender,
                HighSchoolId = p.HighSchoolId,
                Identify = p.Identify,
                IsPermanentResidenceMore18 = p.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = p.IsPermanentResidenceSpecialMore18,
                Nation = p.Nation,
                Phone = p.Phone,
                TownId = p.TownId,
            }).FirstOrDefaultAsync();

            return personalInformation;
        }

        public async Task<bool> Update(PersonalInformation personalInformation)
        {
            await tFContext.PersonalInformation.Where(p => p.Id.Equals(personalInformation.Id)).UpdateFromQueryAsync(p => new PersonalInformationDAO
            {
                Id = personalInformation.Id,
                FullName = personalInformation.FullName,
                Address = personalInformation.Address,
                Dob = personalInformation.Dob,
                PlaceOfBirth = personalInformation.PlaceOfBirth,
                Email = personalInformation.Email,
                Gender = personalInformation.Gender,
                HighSchoolId = personalInformation.HighSchoolId,
                Identify = personalInformation.Identify,
                IsPermanentResidenceMore18 = personalInformation.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = personalInformation.IsPermanentResidenceSpecialMore18,
                Nation = personalInformation.Nation,
                Phone = personalInformation.Phone,
                TownId = personalInformation.TownId,
            });

            return true;
        }
    }
}
