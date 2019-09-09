using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class GraduationDAO
    {
        public GraduationDAO()
        {
            Forms = new HashSet<FormDAO>();
            ReserveMarks = new HashSet<ReserveMarkDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string ExceptLanguages { get; set; }
        public int? Mark { get; set; }

        public virtual ICollection<FormDAO> Forms { get; set; }
        public virtual ICollection<ReserveMarkDAO> ReserveMarks { get; set; }
    }
}
