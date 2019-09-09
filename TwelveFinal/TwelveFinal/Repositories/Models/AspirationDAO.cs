using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class AspirationDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public string UniversityCode { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public string SubjectGroupType { get; set; }
        public Guid UniversityAdmissionId { get; set; }

        public virtual UniversityAdmissionDAO UniversityAdmission { get; set; }
    }
}
