using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class District : DataEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid ProvinceId { get; set; }
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public List<Town> Towns { get; set; }
    }

    public class DistrictFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public Guid ProvinceId { get; set; }
        public StringFilter ProvinceCode { get; set; }
        public StringFilter ProvinceName { get; set; }
        public List<Guid> ExceptIds { get; set; }
        public List<Guid> Ids { get; set; }
        public DistrictOrder OrderBy { get; set; }
        public DistrictFilter() : base()
        {

        }
    }

    [JsonConverter(typeof(StringEnumConverter))]

    public enum DistrictOrder
    {
        CX,
        Code,
        Name
    }
}
