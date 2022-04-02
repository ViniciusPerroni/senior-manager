using AutoMapper;
using Model = SeniorManager.Domain.Seguranca.Entities;
using Dto = SeniorManager.Application.Seguranca.Dtos;
using System.Diagnostics.CodeAnalysis;

namespace SeniorManager.Configuration.Seguranca
{
    [ExcludeFromCodeCoverage]
    public class Mapeamentos : Profile
    {
        public Mapeamentos()
        {
            CreateMap<Model.Usuario, Dto.UsuarioDto>();
        }
    }
}