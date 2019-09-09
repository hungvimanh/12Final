using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class HighSchoolDAO
    {
        public HighSchoolDAO()
        {
            HighSchoolReferenceGrade10s = new HashSet<HighSchoolReferenceDAO>();
            HighSchoolReferenceGrade11s = new HashSet<HighSchoolReferenceDAO>();
            HighSchoolReferenceGrade12s = new HashSet<HighSchoolReferenceDAO>();
            RegisterInformations = new HashSet<RegisterInformationDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid ProvinceId { get; set; }

        public virtual ProvinceDAO Province { get; set; }
        public virtual ICollection<HighSchoolReferenceDAO> HighSchoolReferenceGrade10s { get; set; }
        public virtual ICollection<HighSchoolReferenceDAO> HighSchoolReferenceGrade11s { get; set; }
        public virtual ICollection<HighSchoolReferenceDAO> HighSchoolReferenceGrade12s { get; set; }
        public virtual ICollection<RegisterInformationDAO> RegisterInformations { get; set; }
    }
}
