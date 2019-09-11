using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class University_MajorsDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public Guid UniversityId { get; set; }
        public Guid MajorsId { get; set; }
        public double? Benchmark { get; set; }
        public string SubjectGroupType { get; set; }
        public Guid FormId { get; set; }
        public string Year { get; set; }

        public virtual FormDAO Form { get; set; }
        public virtual MajorsDAO Majors { get; set; }
        public virtual UniversityDAO University { get; set; }
    }
}
