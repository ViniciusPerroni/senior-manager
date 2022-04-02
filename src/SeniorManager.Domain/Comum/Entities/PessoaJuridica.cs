namespace SeniorManager.Domain.Comum.Entities
{
    public class PessoaJuridica : BaseEntity
    {
        public PessoaJuridica() { }

        public PessoaJuridica(string nomeFantasia, string razaoSocial, string cnpj, string inscricaoMunicipal, string inscricaoEstadual, 
            string telefone, string celular, string nomePessoaResponsavel, Endereco endereco)
        {
            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            InscricaoMunicipal = inscricaoMunicipal;
            InscricaoEstadual = inscricaoEstadual;
            Telefone = telefone;
            Celular = celular;
            NomePessoaResponsavel = nomePessoaResponsavel;
            Endereco = endereco;
            EnderecoId = endereco.Id;
        }

        public string NomeFantasia { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Cnpj { get; private set; }
        public string InscricaoMunicipal { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public string Telefone { get; private set; }
        public string Celular { get; private set; }
        public string NomePessoaResponsavel { get; private set; }
        public virtual int EnderecoId { get; private set; }
        public virtual Endereco Endereco { get; private set; }

        public void Editar(string nomeFantasia, string razaoSocial, string cnpj, string inscricaoMunicipal, string inscricaoEstadual,
            string telefone, string celular, string nomePessoaResponsavel)
        {
            NomeFantasia = nomeFantasia;
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
            InscricaoMunicipal = inscricaoMunicipal;
            InscricaoEstadual = inscricaoEstadual;
            Telefone = telefone;
            Celular = celular;
            NomePessoaResponsavel = nomePessoaResponsavel;
        }
    }
}