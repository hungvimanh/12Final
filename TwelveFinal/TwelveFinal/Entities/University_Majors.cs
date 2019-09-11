using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class University_Majors : DataEntity
    {
        public Guid Id { get; set; }
        public Guid UniversityId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public Guid MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public double? Benchmark { get; set; }
        public string SubjectGroupType { get; set; }
        public int TotalAspiration { get; set; }
        public string Year { get; set; }
        public Guid FormId { get; set; }
    }

    public class University_MajorsFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public Guid FormId { get; set; }
        public GuidFilter UniversityId { get; set; }
        public StringFilter UniversityCode { get; set; }
        public StringFilter UniversityName { get; set; }
        public GuidFilter MajorsId { get; set; }
        public StringFilter MajorsCode { get; set; }
        public StringFilter MajorsName { get; set; }
        public DoubleFilter Benchmark { get; set; }
        public StringFilter SubjectGroupType { get; set; }
        public GuidFilter UniversityAdmissionId { get; set; }
        public University_MajorsOrder OrderBy { get; set; }
        public University_MajorsFilter() : base()
        {

        }
    }

    public enum University_MajorsOrder
    {
        CX,
        UniversityCode,
        MajorsCode,
        Benchmark
    }
}
