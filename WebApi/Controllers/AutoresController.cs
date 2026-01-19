using Application;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController(IAutorAppService autorAppService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CadastrarAutorRequest request)
        {
            await autorAppService.Cadastrar(request);

            return Created();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await autorAppService.ObterTodos());
        }

    }
}
