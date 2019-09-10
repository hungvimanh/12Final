using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class PersonalInformationDAO
    {
        public PersonalInformationDAO()
        {
            Forms = new HashSet<FormDAO>();
            Users = new HashSet<UserDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Nation { get; set; }
        public string Identify { get; set; }
        public Guid TownId { get; set; }
        public bool? IsPermanentResidenceMore18 { get; set; }
        public bool? IsPermanentResidenceSpecialMore18 { get; set; }
        public Guid HighSchoolId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual HighSchoolReferenceDAO HighSchool { get; set; }
        public virtual TownDAO Town { get; set; }
        public virtual ICollection<FormDAO> Forms { get; set; }
        public virtual ICollection<UserDAO> Users { get; set; }
    }
}
