using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Test : DataEntity
    {
        public Guid Id { get; set; }
        public bool Maths { get; set; }
        public bool Literature { get; set; }
        public string ForeignLanguage { get; set; }
        public Guid? ScienceId { get; set; }
    }
}
