using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Area : DataEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class AreaFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public StringFilter Name { get; set; }
        public AreaOrder OrderBy { get; set; }
        public AreaFilter() : base()
        {

        }
    }

    public enum AreaOrder
    {
        CX,
        Code,
        Name
    }
}
