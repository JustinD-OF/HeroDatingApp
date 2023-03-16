using HeroDatingApp.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HeroDatingApp.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [ApiController]
    [Route("api/[controller]")] //  route to : ../api/users

    public class BaseApiController : ControllerBase
    {
        
    }
}