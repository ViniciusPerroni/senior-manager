using AutoMapper;
using Model = SeniorManager.Domain.Comum.Entities;
using Dto = SeniorManager.Application.Comum.Dtos;
using System.Diagnostics.CodeAnalysis;

namespace SeniorManager.Configuration.Comum
{
    [ExcludeFromCodeCoverage]
    public class Mapeamentos : Profile
    {
        public Mapeamentos()
        {
            CreateMap<Model.Cliente, Dto.ClienteDto>();
            CreateMap<Model.PessoaJuridica, Dto.PessoaJuridicaDto>();
            CreateMap<Model.Endereco, Dto.EnderecoDto>();
        }
    }
}