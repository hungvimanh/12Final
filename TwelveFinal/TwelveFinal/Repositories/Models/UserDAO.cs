using System;
using System.Collections.Generic;

namespace TwelveFinal.Repositories.Models
{
    public partial class UserDAO
    {
        public UserDAO()
        {
            Forms = new HashSet<FormDAO>();
        }

        public Guid Id { get; set; }
        public long CX { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsAdmin { get; set; }
        public bool? Gender { get; set; }
        public string Identify { get; set; }
        public DateTime? Dob { get; set; }
        public string Ethnic { get; set; }

        public virtual ICollection<FormDAO> Forms { get; set; }
    }
}
