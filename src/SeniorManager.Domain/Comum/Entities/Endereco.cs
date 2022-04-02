namespace SeniorManager.Domain.Comum.Entities
{
    public class Endereco :  BaseEntity
    {
        public Endereco() { }

        public Endereco(string logradouro, string numero, string bairro, string complemento, 
            string cep, string cidade, string estado)
        {
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Complemento = complemento;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
        }

        public string Logradouro { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Complemento { get; private set; }
        public string Cep { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }

        public void Editar(string logradouro, string numero, string bairro, string complemento,
            string cep, string cidade, string estado)
        {
            Logradouro = logradouro;
            Numero = numero;
            Bairro = bairro;
            Complemento = complemento;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
        }

    }
}