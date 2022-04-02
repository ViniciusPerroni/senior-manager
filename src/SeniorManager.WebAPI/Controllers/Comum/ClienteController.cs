using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json.Linq;

using SeniorManager.Application.Comum;
using SeniorManager.WebAPI.Controllers.Comum.Exemples.Cliente;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

using System.Collections.Generic;
using System.Threading.Tasks;

using ComumCliente = SeniorManager.Application.Comum.UseCases.Cliente;
using ComumDtos = SeniorManager.Application.Comum.Dtos;

namespace SeniorManager.WebAPI.Controllers.Comum
{
    [ApiController]
    [Route("api/comum/[controller]")]
    [AllowAnonymous]
    public class ClienteController : ControllerBase
    {
        private readonly ComumCliente.Listar.IUseCase clienteListar;
        private readonly ComumCliente.BuscarPorId.IUseCase clienteBuscarPorId;
        private readonly ComumCliente.Salvar.IUseCase clienteSalvar;

        public ClienteController(ComumCliente.Listar.IUseCase clienteListar, ComumCliente.BuscarPorId.IUseCase clienteBuscarPorId, ComumCliente.Salvar.IUseCase clienteSalvar)
        {
            this.clienteListar = clienteListar;
            this.clienteBuscarPorId = clienteBuscarPorId;
            this.clienteSalvar = clienteSalvar;
        }

        /// <summary>
        /// Obter listagem de clientes paginada
        /// </summary>
        /// <remarks>
        /// OrderOrientation: ["asc", "desc"]
        /// 
        /// OrderBy: ["cnpj", "namoFantasia", "razaoSocial"]
        /// </remarks>
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericPagedOutput<IList<ComumCliente.Listar.Output>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(GenericOutput<IList<ComumCliente.Listar.Output>>))]
        [SwaggerRequestExample(typeof(JObject), typeof(GetPagedClienteInputExample))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetPagedClienteOkOutputExample))]
        [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(GetPagedClienteErrorOutputExample))]
        [HttpGet]
        [Route("paged")]
        public async Task<IActionResult> Paged([FromQuery] ComumCliente.Listar.Input input)
        {
            var output = await clienteListar.Execute(input);

            if (!output.Ok)
                return BadRequest(output);

            return Ok(output);
        }
        /// <summary>
        /// Obter cliente por id
        /// </summary>
        /// <remarks>
        /// TODO: Validar se usuário tem acesso a esse cliente
        /// </remarks>
        [HttpGet]
        [Route("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(GenericOutput<ComumDtos.ClienteDto>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(GenericOutput<ComumDtos.ClienteDto>))]
        public async Task<IActionResult> Get(int id)
        {
            var input = new ComumCliente.BuscarPorId.Input { Id = id };
            var output = await clienteBuscarPorId.Execute(input);

            if (!output.Ok)
                return BadRequest(output);

            return Ok(output);
        }
        /// <summary>
        /// Criar cliente
        /// </summary>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(ComumCliente.Salvar.Input))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(GenericOutput<ComumDtos.ClienteDto>))]
        public async Task<IActionResult> Post([FromBody] ComumCliente.Salvar.Input input)
        {
            var output = await clienteSalvar.Execute(input);

            if (!output.Ok)
                return BadRequest(output);

            return Ok(output);
        }
        /// <summary>
        /// Editar cliente
        /// </summary>
        [HttpPut]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(ComumCliente.Salvar.Input))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(GenericOutput<ComumDtos.ClienteDto>))]
        public async Task<IActionResult> Put([FromBody] ComumCliente.Salvar.Input input)
        {
            var output = await clienteSalvar.Execute(input);

            if (!output.Ok)
                return BadRequest(output);

            return Ok(output);
        }
    }
}