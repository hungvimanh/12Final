using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class FormDAO
    {
        public FormDAO()
        {
            Aspirations = new HashSet<AspirationDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Ethnic { get; set; }
        public string Identify { get; set; }
        public Guid TownId { get; set; }
        public bool? IsPermanentResidenceMore18 { get; set; }
        public bool? IsPermanentResidenceSpecialMore18 { get; set; }
        public Guid HighSchoolGrade10Id { get; set; }
        public Guid HighSchoolGrade11Id { get; set; }
        public Guid HighSchoolGrade12Id { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool? ResultForUniversity { get; set; }
        public bool StudyAtHighSchool { get; set; }
        public bool? Graduated { get; set; }
        public Guid ClusterContestId { get; set; }
        public Guid RegisterPlaceOfExamId { get; set; }
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
        public bool? Connected { get; set; }

        public virtual ProvinceDAO ClusterContest { get; set; }
        public virtual HighSchoolDAO HighSchoolGrade10 { get; set; }
        public virtual HighSchoolDAO HighSchoolGrade11 { get; set; }
        public virtual HighSchoolDAO HighSchoolGrade12 { get; set; }
        public virtual HighSchoolDAO RegisterPlaceOfExam { get; set; }
        public virtual TownDAO Town { get; set; }
        public virtual UserDAO User { get; set; }
        public virtual ICollection<AspirationDAO> Aspirations { get; set; }
    }
}
