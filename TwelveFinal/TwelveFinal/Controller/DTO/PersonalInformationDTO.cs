using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class PersonalInformationDTO : DataDTO   
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Ethnic { get; set; }
        public string Identify { get; set; }
        public Guid TownId { get; set; }
        public string TownCode { get; set; }
        public string TownName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public bool? IsPermanentResidenceMore18 { get; set; }
        public bool? IsPermanentResidenceSpecialMore18 { get; set; }
        public Guid HighSchoolGrade10Id { get; set; }
        public string HighSchoolGrade10Code { get; set; }
        public string HighSchoolGrade10Name { get; set; }
        public string HighSchoolGrade10DistrictCode { get; set; }
        public string HighSchoolGrade10DistrictName { get; set; }
        public string HighSchoolGrade10ProvinceCode { get; set; }
        public string HighSchoolGrade10ProvinceName { get; set; }
        public Guid HighSchoolGrade11Id { get; set; }
        public string HighSchoolGrade11Code { get; set; }
        public string HighSchoolGrade11Name { get; set; }
        public string HighSchoolGrade11DistrictCode { get; set; }
        public string HighSchoolGrade11DistrictName { get; set; }
        public string HighSchoolGrade11ProvinceCode { get; set; }
        public string HighSchoolGrade11ProvinceName { get; set; }
        public Guid HighSchoolGrade12Id { get; set; }
        public string HighSchoolGrade12Code { get; set; }
        public string HighSchoolGrade12Name { get; set; }
        public string HighSchoolGrade12DistrictCode { get; set; }
        public string HighSchoolGrade12DistrictName { get; set; }
        public string HighSchoolGrade12ProvinceCode { get; set; }
        public string HighSchoolGrade12ProvinceName { get; set; }
        public string Grade12Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
