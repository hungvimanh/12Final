using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.student
{
    public class Student_IdentifyDTO : DataDTO
    {
        public Guid? StudentId { get; set; }
        public string Identify { get; set; }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public string Email { get; set; }
    }
}
