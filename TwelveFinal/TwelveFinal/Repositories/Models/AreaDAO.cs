using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class AreaDAO
    {
        public AreaDAO()
        {
            HighSchools = new HashSet<HighSchoolDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<HighSchoolDAO> HighSchools { get; set; }
    }
}
