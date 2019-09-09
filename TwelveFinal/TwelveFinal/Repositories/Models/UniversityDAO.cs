using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class UniversityDAO
    {
        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual University_MajorsDAO IdNavigation { get; set; }
    }
}
