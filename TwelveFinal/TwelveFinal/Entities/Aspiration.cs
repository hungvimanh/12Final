using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Aspiration : DataEntity
    {
        public Guid Id { get; set; }
        public string UniversityCode { get; set; }
        public string MajorsCode { get; set; }
        public string MajorsName { get; set; }
        public string SubjectGroupType { get; set; }
        public Guid UniversityAdmissionId { get; set; }
    }

    public class AspirationFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public Guid UniversityAdmissionId { get; set; }
        public StringFilter UniversityCode { get; set; }
        public StringFilter MajorsCode { get; set; }
        public StringFilter MajorsName { get; set; }
        public StringFilter SubjectGroupType { get; set; }
        public List<Guid> ExceptIds { get; set; }
        public List<Guid> Ids { get; set; }
        public AspirationOrder OrderBy { get; set; }
        public AspirationFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum AspirationOrder
    {
        CX,
        UniversityCode,
        MajorsCode,
        MajorsName
    }
}
