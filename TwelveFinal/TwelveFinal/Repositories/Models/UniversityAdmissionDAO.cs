using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class UniversityAdmissionDAO
    {
        public UniversityAdmissionDAO()
        {
            Forms = new HashSet<FormDAO>();
            University_Majors = new HashSet<University_MajorsDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string PriorityType { get; set; }
        public string Area { get; set; }
        public string GraduateYear { get; set; }
        public int? Connected { get; set; }

        public virtual ICollection<FormDAO> Forms { get; set; }
        public virtual ICollection<University_MajorsDAO> University_Majors { get; set; }
    }
}
