using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class University_Majors : DataEntity
    {
        public Guid UniversityId { get; set; }
        public string UniversityCode { get; set; }
        public string UniversityName { get; set; }
        public string UniversityAddress { get; set; }
        public Guid MajorsId { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public double? Benchmark { get; set; }
        public Guid SubjectGroupId { get; set; }
        public string SubjectGroupCode { get; set; }
        public string SubjectGroupName { get; set; }
        public string Year { get; set; }
        public int? Quantity { get; set; }
        public string Descreption { get; set; }
    }

    public class University_MajorsFilter : FilterEntity
    {
        public Guid? UniversityId { get; set; }
        public StringFilter UniversityCode { get; set; }
        public StringFilter UniversityName { get; set; }
        public Guid? MajorsId { get; set; }
        public StringFilter MajorsCode { get; set; }
        public StringFilter MajorsName { get; set; }
        public DoubleFilter BenchmarkHigh { get; set; }
        public DoubleFilter BenchmarkLow { get; set; }
        public GuidFilter SubjectGroupId { get; set; }
        public StringFilter SubjectGroupCode { get; set; }
        public StringFilter SubjectGroupName { get; set; }
        public string Year { get; set; }
        public University_MajorsOrder OrderBy { get; set; }
        public University_MajorsFilter() : base()
        {

        }
    }

    public enum University_MajorsOrder
    {
        CX,
        UniversityCode,
        UniversityName,
        MajorsCode,
        MajorsName,
        Benchmark,
        Year
    }
}
