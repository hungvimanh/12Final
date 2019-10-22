using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwelveFinal.Controller
{
    public class Root
    {
        public const string Base = "api/TF";
    }

    public class AdminRoute : Root
    {

    }

    public class StudentRoute : Root
    {

    }

    [Authorize]
    [Authorize(Policy = "Permission")]
    public class ApiController : ControllerBase
    {

    }
}
