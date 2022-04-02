using System.Collections.Generic;
using SeniorManager.Application.Comum;

namespace SeniorManager.Application.Seguranca.UseCases.Usuario.BuscarPorUsername
{
    public class Input : BaseInput
    {
        public string Username { get; set; }

        internal override void Validate()
        {
            Errors = new List<string>();
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrEmpty(Username))
                Errors.Add("O campo 'Username' é obrigatório");
        }
    }
}