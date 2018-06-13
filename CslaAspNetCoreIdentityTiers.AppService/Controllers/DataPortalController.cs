using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Csla.Server.Hosts;
using Microsoft.AspNetCore.Mvc;

namespace CslaAspNetCoreIdentityTiers.AppService
{
    [Route("api/[controller]")]
    public class DataPortalController
        : HttpPortalController
    {
    }
}
