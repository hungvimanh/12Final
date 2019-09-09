using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class RegisterInformationDAO
    {
        public RegisterInformationDAO()
        {
            Forms = new HashSet<FormDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool? ResultForUniversity { get; set; }
        public bool StudyAtHighSchool { get; set; }
        public bool Passed { get; set; }
        public Guid ContestGroupId { get; set; }
        public Guid ContestUnitId { get; set; }
        public Guid TestId { get; set; }

        public virtual ProvinceDAO ContestGroup { get; set; }
        public virtual HighSchoolDAO ContestUnit { get; set; }
        public virtual TestExamDAO Test { get; set; }
        public virtual ICollection<FormDAO> Forms { get; set; }
    }
}
