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
        public Guid? StudentId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsAdmin { get; set; }

        public virtual StudentDAO Student { get; set; }
        public virtual ICollection<FormDAO> Forms { get; set; }
    }
}
