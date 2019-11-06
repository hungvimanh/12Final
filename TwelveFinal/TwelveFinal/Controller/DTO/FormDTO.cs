using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class FormDTO : DataDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string FullName { get; set; }
        public DateTime? Dob { get; set; }
        public bool? Gender { get; set; }
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
        public Guid HighSchoolGrade11Id { get; set; }
        public string HighSchoolGrade11Code { get; set; }
        public string HighSchoolGrade11Name { get; set; }
        public Guid HighSchoolGrade12Id { get; set; }
        public string HighSchoolGrade12Code { get; set; }
        public string HighSchoolGrade12Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public bool? ResultForUniversity { get; set; }
        public bool StudyAtHighSchool { get; set; }
        public bool? Graduated { get; set; }
        public Guid ClusterContestId { get; set; }
        public string ClusterContestCode { get; set; }
        public string ClusterContestName { get; set; }
        public Guid RegisterPlaceOfExamId { get; set; }
        public string RegisterPlaceOfExamCode { get; set; }
        public string RegisterPlaceOfExamName { get; set; }
        public bool? Maths { get; set; }
        public bool? Literature { get; set; }
        public string Languages { get; set; }
        public bool? NaturalSciences { get; set; }
        public bool? SocialSciences { get; set; }
        public bool? Physics { get; set; }
        public bool? Chemistry { get; set; }
        public bool? Biology { get; set; }
        public bool? History { get; set; }
        public bool? Geography { get; set; }
        public bool? CivicEducation { get; set; }

        public string ExceptLanguages { get; set; }
        public int? Mark { get; set; }
        public int? ReserveMaths { get; set; }
        public int? ReservePhysics { get; set; }
        public int? ReserveChemistry { get; set; }
        public int? ReserveLiterature { get; set; }
        public int? ReserveHistory { get; set; }
        public int? ReserveGeography { get; set; }
        public int? ReserveBiology { get; set; }
        public int? ReserveCivicEducation { get; set; }
        public int? ReserveLanguages { get; set; }

        public string PriorityType { get; set; }
        public string Area { get; set; }

        public List<AspirationDTO> Aspirations { get; set; }
    }
}
