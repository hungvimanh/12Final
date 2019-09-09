using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class ReserveMark : DataEntity
    {
        public Guid Id { get; set; }
        public Guid GraduationId { get; set; }
        public int? Maths { get; set; }
        public int? Literature { get; set; }
        public int? History { get; set; }
        public int? Geography { get; set; }
        public int? CivicEducation { get; set; }
        public int? Physics { get; set; }
        public int? Chemistry { get; set; }
        public int? Biology { get; set; }
        public int? Languages { get; set; }
    }
}
