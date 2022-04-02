using System.Collections.Generic;

namespace SeniorManager.Application.Comum.UseCases.Cliente.BuscarPorId
{
    public class Input : BaseInput
    {
        public int Id { get; set; }
        internal override void Validate()
        {
            Errors = new List<string>();

            if (Id <= 0)
                Errors.Add("Parâmetro Id inválido.");
        }
    }
}