using System.Collections.Generic;

namespace SeniorManager.Domain.Comum.Entities
{
    public class Cliente : BaseEntity
    {
        public Cliente() { }
        public Cliente(PessoaJuridica pessoaJuridica)
        {
            PessoaJuridica = pessoaJuridica;
            PessoaJuridicaId = pessoaJuridica.Id;
        }

        public virtual int PessoaJuridicaId { get; private set; }
        public virtual PessoaJuridica PessoaJuridica { get; private set; }
        public virtual List<Filial> Filiais { get; private set; }
    }
}