using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class PriorityTypeDTO : DataDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
    }

    public class PriorityTypeFilterDTO : FilterDTO
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
    }
}
