using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class HighSchoolReferenceDAO
    {
        public HighSchoolReferenceDAO()
        {
            PersonalInformations = new HashSet<PersonalInformationDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public Guid Grade10Id { get; set; }
        public Guid Grade11Id { get; set; }
        public Guid Grade12Id { get; set; }

        public virtual HighSchoolDAO Grade10 { get; set; }
        public virtual HighSchoolDAO Grade11 { get; set; }
        public virtual HighSchoolDAO Grade12 { get; set; }
        public virtual ICollection<PersonalInformationDAO> PersonalInformations { get; set; }
    }
}
