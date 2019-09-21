using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class HighSchoolDAO
    {
        public HighSchoolDAO()
        {
            FormHighSchoolGrade10s = new HashSet<FormDAO>();
            FormHighSchoolGrade11s = new HashSet<FormDAO>();
            FormHighSchoolGrade12s = new HashSet<FormDAO>();
            FormRegisterPlaceOfExams = new HashSet<FormDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid DistrictId { get; set; }
        public string Address { get; set; }
        public Guid AreaId { get; set; }

        public virtual AreaDAO Area { get; set; }
        public virtual DistrictDAO District { get; set; }
        public virtual ICollection<FormDAO> FormHighSchoolGrade10s { get; set; }
        public virtual ICollection<FormDAO> FormHighSchoolGrade11s { get; set; }
        public virtual ICollection<FormDAO> FormHighSchoolGrade12s { get; set; }
        public virtual ICollection<FormDAO> FormRegisterPlaceOfExams { get; set; }
    }
}
