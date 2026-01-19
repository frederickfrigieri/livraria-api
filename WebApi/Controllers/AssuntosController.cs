using Application;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssuntosController(IAssuntoAppService assuntoAppService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CadastrarAssuntoRequest request)
        {
            await assuntoAppService.Cadastrar(request);

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await assuntoAppService.ObterTodos());
        }
    }
}
