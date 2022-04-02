namespace SeniorManager.Application.Comum.Dtos
{
    public class FilialDto
    {
        public virtual ClienteDto Cliente { get; private set; }
        public virtual int ClienteId { get; private set; }
        public virtual PessoaJuridicaDto PessoaJuridica { get; private set; }
        public virtual int PessoaJuridicaId { get; private set; }
    }
}