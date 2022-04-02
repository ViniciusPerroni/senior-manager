using System.Collections.Generic;

namespace SeniorManager.Application.Comum.Dtos
{
    public class ClienteDto
    {
        public ClienteDto()
        {
            PessoaJuridica = new PessoaJuridicaDto();
            Filiais = new List<FilialDto>();
        }

        public int Id { get; set; }
        public PessoaJuridicaDto PessoaJuridica { get; set; }
        public virtual List<FilialDto> Filiais { get; private set; }
    }
}