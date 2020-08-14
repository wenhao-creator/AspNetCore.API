using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.API.Controllers
{
    /// <summary>
    /// Base 控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}