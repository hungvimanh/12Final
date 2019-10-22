using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Common
{
    public interface ICurrentContext : IServiceScoped
    {
        Guid UserId { get; set; }
        string Username { get; set; }
        bool IsAdmin { get; set; }
    }
    public class CurrentContext : ICurrentContext
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
    }
}
