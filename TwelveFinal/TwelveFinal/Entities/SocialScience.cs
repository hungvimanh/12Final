using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class SocialScience : DataEntity
    {
        public Guid Id { get; set; }
        public bool History { get; set; }
        public bool Geography { get; set; }
        public bool CivicEducation { get; set; }
    }
}
