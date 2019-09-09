using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Graduation : DataEntity
    {
        public Guid Id { get; set; }
        public string ExceptLanguages { get; set; }
        public int? Mark { get; set; }
        public Guid? ReserveId { get; set; }
    }

    public class GraduationFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter ExceptLanguages { get; set; }
        public IntFilter Mark { get; set; }
        public GuidFilter ReserveId { get; set; }
        public List<Guid> ExceptIds { get; set; }
        public List<Guid> Ids { get; set; }
        public GraduationOrder OrderBy { get; set; }
        public GraduationFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]

    public enum GraduationOrder
    {
        CX
    }
}
