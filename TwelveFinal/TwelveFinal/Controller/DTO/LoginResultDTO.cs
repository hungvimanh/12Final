using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller.DTO
{
    public class LoginResultDTO
    {
        public string FullName { get; set; }
        public string Token { get; set; }
        public DateTime? ExpiredTime { get; set; }
    }
}
