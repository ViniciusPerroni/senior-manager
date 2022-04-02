namespace SeniorManager.Application.Comum.Dtos
{
    public class PessoaJuridicaDto
    {
        public PessoaJuridicaDto()
        {
            Endereco = new EnderecoDto();
        }

        public long Id { get; set; }
        public string NomeFantasia { get; set; }
        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string NomePessoaResponsavel { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}