using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class PriorityType : DataEntity      //Đối tượng ưu tiên
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
    }

    public class PriorityTypeFilter : FilterEntity
    {
        public GuidFilter Id { get; set; }
        public StringFilter Code { get; set; }
        public PriorityTypeOrder OrderBy { get; set; }
        public PriorityTypeFilter() : base()
        {

        }
    }

    public enum PriorityTypeOrder
    {
        CX,
        Code
    }
}
