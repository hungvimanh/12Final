using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Entities
{
    public class User : DataEntity
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string Jwt { get; set; }
        public DateTime? ExpiredTime { get; set; }
        public string Salt { get; set; }
        public bool? Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Identify { get; set; }
        public DateTime? Dob { get; set; }
        public string Ethnic { get; set; }
    }

    public class UserFilter
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public bool? IsAdmin { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Identify { get; set; }
        public DateTime? Dob { get; set; }
        public string Ethnic { get; set; }
    }
}
