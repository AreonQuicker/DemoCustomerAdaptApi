using Microsoft.AspNetCore.Mvc;

namespace DemoCustomerAdaptApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}