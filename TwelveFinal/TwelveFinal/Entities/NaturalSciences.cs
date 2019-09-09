using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class NaturalSciences : DataEntity
    {
        public Guid Id { get; set; }
        public bool Physics { get; set; }
        public bool Chemistry { get; set; }
        public bool Biology { get; set; }
    }
}
