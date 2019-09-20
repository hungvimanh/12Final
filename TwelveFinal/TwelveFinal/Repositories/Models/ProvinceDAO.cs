using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class ProvinceDAO
    {
        public ProvinceDAO()
        {
            Districts = new HashSet<DistrictDAO>();
            Forms = new HashSet<FormDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public Guid AreaId { get; set; }

        public virtual AreaDAO Area { get; set; }
        public virtual ICollection<DistrictDAO> Districts { get; set; }
        public virtual ICollection<FormDAO> Forms { get; set; }
    }
}
