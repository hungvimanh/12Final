using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class Student : DataEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public bool Gender { get; set; }
        public string Nation { get; set; }
        public string Identify { get; set; }
        public Guid TownId { get; set; }
        public bool? IsPermanentResidenceMore18 { get; set; }
        public bool? IsPermanentResidenceSpecialMore18 { get; set; }
        public Guid HighSchoolId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<Form> Forms { get; set; }
    }
}
