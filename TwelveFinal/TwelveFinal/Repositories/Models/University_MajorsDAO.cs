using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class University_MajorsDAO
    {
        public long CX { get; set; }
        public Guid UniversityId { get; set; }
        public Guid MajorsId { get; set; }
        public double? Benchmark { get; set; }
        public Guid SubjectGroupId { get; set; }
        public string Year { get; set; }
        public string Descreption { get; set; }
        public int? Quantity { get; set; }

        public virtual MajorsDAO Majors { get; set; }
        public virtual SubjectGroupDAO SubjectGroup { get; set; }
        public virtual UniversityDAO University { get; set; }
    }
}
