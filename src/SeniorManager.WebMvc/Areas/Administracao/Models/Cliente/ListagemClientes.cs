using SeniorManager.Application.Comum;
using SeniorManager.Application.Comum.Dtos;

using System.Collections.Generic;

namespace SeniorManager.WebMvc.Areas.Administracao.Models.Cliente
{
    public class ListagemClientes
    {
        public ListagemClientes()
        {
            Output = new GenericPagedOutput<IList<ClienteDto>>();
            Input = new BasePagedInput();
        }

        public GenericPagedOutput<IList<ClienteDto>> Output { get; set; }
        public BasePagedInput Input { get; set; }
    }
}