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
        Task<PersonalInformation> Get(Guid Id);
        Task<bool> Update(PersonalInformation personalInformation);
    }
    public class PersonalInformationRepository : IPersonalInformationRepository
    {
        private readonly TFContext tFContext;
        public PersonalInformationRepository(TFContext _tFContext)
        {
            tFContext = _tFContext;
        }

        public async Task<PersonalInformation> Get(Guid Id)
        {
            PersonalInformation personalInformation = await tFContext.Form.Where(p => p.Id.Equals(Id)).Select(p => new PersonalInformation
            {
                Id = p.Id,
                FullName = p.FullName,
                Address = p.Address,
                Dob = p.Dob,
                PlaceOfBirth = p.PlaceOfBirth,
                Email = p.Email,
                Gender = p.Gender,
                HighSchoolGrade10Id = p.HighSchoolGrade10Id,
                HighSchoolGrade10Code = p.HighSchoolGrade10.Code,
                HighSchoolGrade10Name = p.HighSchoolGrade10.Name,
                HighSchoolGrade10DistrictCode = p.HighSchoolGrade10.District.Code,
                HighSchoolGrade10DistrictName = p.HighSchoolGrade10.District.Name,
                HighSchoolGrade10ProvinceCode = p.HighSchoolGrade10.District.Province.Code,
                HighSchoolGrade10ProvinceName = p.HighSchoolGrade10.District.Province.Name,
                HighSchoolGrade11Id = p.HighSchoolGrade11Id,
                HighSchoolGrade11Code = p.HighSchoolGrade11.Code,
                HighSchoolGrade11Name = p.HighSchoolGrade11.Name,
                HighSchoolGrade11DistrictCode = p.HighSchoolGrade11.District.Code,
                HighSchoolGrade11DistrictName = p.HighSchoolGrade11.District.Name,
                HighSchoolGrade11ProvinceCode = p.HighSchoolGrade11.District.Province.Code,
                HighSchoolGrade11ProvinceName = p.HighSchoolGrade11.District.Province.Name,
                HighSchoolGrade12Id = p.HighSchoolGrade12Id,
                HighSchoolGrade12Code = p.HighSchoolGrade12.Code,
                HighSchoolGrade12Name = p.HighSchoolGrade12.Name,
                HighSchoolGrade12DistrictCode = p.HighSchoolGrade12.District.Code,
                HighSchoolGrade12DistrictName = p.HighSchoolGrade12.District.Name,
                HighSchoolGrade12ProvinceCode = p.HighSchoolGrade12.District.Province.Code,
                HighSchoolGrade12ProvinceName = p.HighSchoolGrade12.District.Province.Name,
                Grade12Name = p.Grade12Name,
                Identify = p.Identify,
                IsPermanentResidenceMore18 = p.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = p.IsPermanentResidenceSpecialMore18,
                Ethnic = p.Ethnic,
                Phone = p.Phone,
                TownId = p.TownId,
                TownCode = p.Town.Code,
                TownName = p.Town.Name,
                DistrictCode = p.Town.District.Code,
                DistrictName = p.Town.District.Name,
                ProvinceCode = p.Town.District.Province.Code,
                ProvinceName = p.Town.District.Province.Name,
                
            }).FirstOrDefaultAsync();

            return personalInformation;
        }

        public async Task<bool> Update(PersonalInformation personalInformation)
        {
            await tFContext.Form.Where(p => p.Id.Equals(personalInformation.Id)).UpdateFromQueryAsync(p => new FormDAO
            {
                FullName = personalInformation.FullName,
                Address = personalInformation.Address,
                Dob = personalInformation.Dob,
                PlaceOfBirth = personalInformation.PlaceOfBirth,
                Email = personalInformation.Email,
                Gender = personalInformation.Gender,
                HighSchoolGrade10Id = personalInformation.HighSchoolGrade10Id,
                HighSchoolGrade11Id = personalInformation.HighSchoolGrade11Id,
                HighSchoolGrade12Id = personalInformation.HighSchoolGrade12Id,
                Grade12Name = personalInformation.Grade12Name,
                Identify = personalInformation.Identify,
                IsPermanentResidenceMore18 = personalInformation.IsPermanentResidenceMore18,
                IsPermanentResidenceSpecialMore18 = personalInformation.IsPermanentResidenceSpecialMore18,
                Ethnic = personalInformation.Ethnic,
                Phone = personalInformation.Phone,
                TownId = personalInformation.TownId,
                
            });

            return true;
        }
    }
}
