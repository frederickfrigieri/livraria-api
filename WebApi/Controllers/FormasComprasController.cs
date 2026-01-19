using Application;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormasComprasController(IFormaCompraAppService formaCompraAppService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await formaCompraAppService.Obter());
    }
}
