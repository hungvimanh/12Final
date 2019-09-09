using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class ReserveMarkDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public int? Maths { get; set; }
        public int? Literature { get; set; }
        public int? History { get; set; }
        public int? Geography { get; set; }
        public int? CivicEducation { get; set; }
        public int? Physics { get; set; }
        public int? Chemistry { get; set; }
        public int? Biology { get; set; }
        public int? Languages { get; set; }
        public Guid GraduationId { get; set; }

        public virtual GraduationInformationDAO Graduation { get; set; }
    }
}
