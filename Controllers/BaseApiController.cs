using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HeroDatingApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //  route to : ../api/users

    public class BaseApiController : ControllerBase
    {
        
    }
}