using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class AreaDAO
    {
        public AreaDAO()
        {
            Provinces = new HashSet<ProvinceDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProvinceDAO> Provinces { get; set; }
    }
}
