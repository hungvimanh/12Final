using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Form : DataEntity
    {
        public Guid Id { get; set; }
        public string NumberForm { get; set; }
        public string DepartmentCode { get; set; }
        public Guid PersonalInfomationId { get; set; }
        public Guid RegisterInformationId { get; set; }
        public Guid GraduationInformationId { get; set; }
        public Guid UniversityAdmissionId { get; set; }
        public DateTime Date { get; set; }
    }

    public class FormFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter NumberForm { get; set; }
        public StringFilter DepartmentCode { get; set; }
        public GuidFilter PersonalInfomationId { get; set; }
        public GuidFilter RegisterInformationId { get; set; }
        public GuidFilter GraduationInformationId { get; set; }
        public GuidFilter UniversityAdmissionId { get; set; }
        public DateTimeFilter Date { get; set; }
        public List<Guid> ExceptIds { get; set; }
        public List<Guid> Ids { get; set; }
        public FormOrder OrderBy { get; set; }
        public FormFilter() : base()
        {

        }
    }
    [JsonConverter(typeof(StringEnumConverter))]
    public enum FormOrder
    {
        CX,
        NumberForm,
        DepartmentCode
    }
}
