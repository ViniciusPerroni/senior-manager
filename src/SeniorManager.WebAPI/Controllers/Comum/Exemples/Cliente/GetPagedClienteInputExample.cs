using System.Diagnostics.CodeAnalysis;
using Swashbuckle.AspNetCore.Filters;


using ComumCliente = SeniorManager.Application.Comum.UseCases.Cliente;

namespace SeniorManager.WebAPI.Controllers.Comum.Exemples.Cliente
{
    [ExcludeFromCodeCoverage]
    public class GetPagedClienteInputExample : IExamplesProvider<ComumCliente.Listar.Input>
    {
        public ComumCliente.Listar.Input GetExamples()
        {
            var input = new ComumCliente.Listar.Input();
            input.PageNumber = 1;
            input.PageSize = 10;
            input.OrderBy = "razaoSocial";
            input.OrderOrientation = "asc";
            return input;
        }
    }
}