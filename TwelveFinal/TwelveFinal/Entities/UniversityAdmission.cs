using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class UniversityAdmission : DataEntity
    {
        public Guid Id { get; set; }
        public string PriorityType { get; set; }
        public string Area { get; set; }
        public string GraduateYear { get; set; }
        public int? Connected { get; set; }
        public List<University_Majors> University_Majorses { get; set; }
    }
}
