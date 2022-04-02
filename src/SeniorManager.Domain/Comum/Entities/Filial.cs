namespace SeniorManager.Domain.Comum.Entities
{
    public class Filial : BaseEntity
    {
        public Filial() { }

        public Filial(int clienteId, int pessoaJuridicaId)
        {
            ClienteId = clienteId;
            PessoaJuridicaId = pessoaJuridicaId;
        }

        public virtual Cliente Cliente { get; private set; }
        public virtual int ClienteId { get; private set; }
        public virtual PessoaJuridica PessoaJuridica { get; private set; }
        public virtual int PessoaJuridicaId { get; private set; }
    }
}
