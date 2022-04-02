using SeniorManager.Application.Comum;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

using ComumCliente = SeniorManager.Application.Comum.UseCases.Cliente;

namespace SeniorManager.WebAPI.Controllers.Comum.Exemples.Cliente
{
    [ExcludeFromCodeCoverage]
    public class GetPagedClienteOkOutputExample : IExamplesProvider<GenericPagedOutput<IList<ComumCliente.Listar.Output>>>
    {
        public GenericPagedOutput<IList<ComumCliente.Listar.Output>> GetExamples()
        {
            var output = new GenericPagedOutput<IList<ComumCliente.Listar.Output>>();

            var data = new List<ComumCliente.Listar.Output>();
            data.Add(new ComumCliente.Listar.Output
            {
                Id = 1,
                Cnpj = "71518453000152",
                NomeFantasia = "Residencial Senior",
                RazaoSocial = "Residencial Senior .LTDA"
            });

            output.Data = data;
            output.Summary = new PagedList.PagedList("https://apiurl.com.br/api/cliente/paged", 1, 1, 10);

            return output;
        }
    }
}