namespace SeniorManager.Application.Seguranca.UseCases.Usuario.BuscarPorUsername
{
    public class Output
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Nome { get; set; }
        public string SobreNome { get; set; }
        public bool Ativo { get; set; }
        public long CodCliente { get; set; }
    }
}