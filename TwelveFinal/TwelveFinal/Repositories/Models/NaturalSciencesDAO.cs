using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class NaturalSciencesDAO
    {
        public NaturalSciencesDAO()
        {
            Tests = new HashSet<TestDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public bool Physics { get; set; }
        public bool Chemistry { get; set; }
        public bool Biology { get; set; }

        public virtual ICollection<TestDAO> Tests { get; set; }
    }
}
