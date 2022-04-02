using System;
using System.Threading.Tasks;
using SeniorManager.Application.Comum;
using SeniorManager.Domain.Seguranca.Repositories;

namespace SeniorManager.Application.Seguranca.UseCases.Usuario.BuscarPorUsername
{
    public class UseCase : BaseUseCase<Output, Input, GenericOutput<Output>>, IUseCase
    {
        private readonly IUsuarioRepository usuarioRepository;
        
        public UseCase(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        internal override async Task<GenericOutput<Output>> BussinesRole(Input input)
        {
            var output = new GenericOutput<Output>();

            try
            {
                var usuario = await usuarioRepository.GetByUsername(input.Username);

                if (usuario == null)
                    output.AddError("Usuário ou senha inválido.");
                else
                    output.Data = new Output
                    {
                        Id = usuario.Id,
                        Nome = usuario.Nome,
                        SobreNome = usuario.Sobrenome,
                        Username = usuario.Username,
                        Ativo = usuario.Ativo,
                        CodCliente = usuario.CodCliente
                    };
            }
            catch(Exception ex)
            {
                output.AddError(ex.Message);
            }
            return output;
        }
    }
}