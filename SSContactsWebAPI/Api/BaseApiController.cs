using Microsoft.AspNetCore.Mvc;

namespace SSContacts.WebApi.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
    }
}
