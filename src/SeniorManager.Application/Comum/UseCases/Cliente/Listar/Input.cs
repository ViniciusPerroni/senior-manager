using System.Collections.Generic;

namespace SeniorManager.Application.Comum.UseCases.Cliente.Listar
{
    public class Input : BasePagedInput
    {
        public Input() : base()
        {
            Columns = new List<string> { "cnpj", "razaoSocial", "nomeFantasia" };
            OrderBy = "nomeFantasia";
        }
    }
}