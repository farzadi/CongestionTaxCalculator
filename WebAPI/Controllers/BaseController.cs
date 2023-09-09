using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/v{version}/[controller]")]
public class BaseController : ControllerBase
{
}