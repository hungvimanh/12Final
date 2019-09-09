using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class HighSchoolReference : DataEntity
    {
        public Guid Id { get; set; }
        public Guid Grade10Id { get; set; }
        public Guid Grade11Id { get; set; }
        public Guid Grade12Id { get; set; }
    }
}
