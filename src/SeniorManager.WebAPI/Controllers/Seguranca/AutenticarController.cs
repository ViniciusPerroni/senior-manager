using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SeniorManager.Application.Comum;

using SegurancaUsuario = SeniorManager.Application.Seguranca.UseCases.Usuario;

namespace SeniorManager.WebAPI.Controllers
{
    [ApiController]
    [Route("api/seguranca/[controller]")]
    public class AutenticarController : Controller
    {
        private readonly SegurancaUsuario.Autenticar.IUseCase autenticarUseCase;
        
        public AutenticarController(SegurancaUsuario.Autenticar.IUseCase autenticarUseCase)
        {
            this.autenticarUseCase = autenticarUseCase;
        }

        [HttpPost("autenticar")]
        [AllowAnonymous]
        public async Task<IActionResult> Autenticar([FromBody] SegurancaUsuario.Autenticar.Input input)
        {
            GenericOutput<SegurancaUsuario.Autenticar.Output> output = await autenticarUseCase.Execute(input);

            if (!output.Ok)
                return BadRequest(output);

            return Ok(output);
        }
    }
}