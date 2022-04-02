using SeniorManager.Application.Comum;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using ComumCliente = SeniorManager.Application.Comum.UseCases.Cliente;

namespace SeniorManager.WebAPI.Controllers.Comum.Exemples.Cliente
{
    [ExcludeFromCodeCoverage]
    public class GetPagedClienteErrorOutputExample : IExamplesProvider<GenericPagedOutput<IList<ComumCliente.Listar.Output>>>
    {
        public GenericPagedOutput<IList<ComumCliente.Listar.Output>> GetExamples()
        {
            var output = new GenericPagedOutput<IList<ComumCliente.Listar.Output>>();
            output.AddError("Mensagem de erro");

            return output;
        }
    }
}