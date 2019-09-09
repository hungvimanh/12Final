using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class RegisterInformation : DataEntity
    {
        public Guid Id { get; set; }
        public bool? ResultForUniversity { get; set; }
        public bool StudyAtHighSchool { get; set; }
        public bool Passed { get; set; }
        public Guid ContestGroupId { get; set; }
        public Guid ContestUnitId { get; set; }
        public Guid TestId { get; set; }
    }
}
