using SeniorManager.Application.Comum.Dtos;
using System.Collections.Generic;

namespace SeniorManager.Application.Comum.UseCases.Cliente.Salvar
{
    public class Input : BaseInput
    {
        public ClienteDto Data { get; set; }

        internal override void Validate()
        {
            Errors = new List<string>();

            if (string.IsNullOrEmpty(Data.PessoaJuridica.RazaoSocial))
                Errors.Add("Razão Social é obrigatório.");
            if (string.IsNullOrEmpty(Data.PessoaJuridica?.NomeFantasia))
                Errors.Add("Nome Fantasia é obrigatório.");
            if (string.IsNullOrEmpty(Data.PessoaJuridica?.Cnpj))
                Errors.Add("CNPJ é obrigatório.");
            if (string.IsNullOrEmpty(Data.PessoaJuridica?.Endereco?.Logradouro))
                Errors.Add("Logradouro é obrigatório.");
            if (string.IsNullOrEmpty(Data.PessoaJuridica?.Endereco?.Numero))
                Errors.Add("Número é obrigatório.");
            if (string.IsNullOrEmpty(Data.PessoaJuridica?.Endereco?.Bairro))
                Errors.Add("Bairro é obrigatório.");
            if (string.IsNullOrEmpty(Data.PessoaJuridica?.Endereco?.Cep))
                Errors.Add("CEP é obrigatório.");
            if (string.IsNullOrEmpty(Data.PessoaJuridica?.Endereco?.Cidade))
                Errors.Add("Cidade é obrigatório.");
            if (string.IsNullOrEmpty(Data.PessoaJuridica?.Endereco?.Estado))
                Errors.Add("Estado é obrigatório.");
        }
    }
}