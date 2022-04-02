using System.Threading.Tasks;
using SeniorManager.Application.Comum;
using SeniorManager.Crosscutting.Interfaces;
using SeniorManager.Domain.Seguranca.Repositories;

namespace SeniorManager.Application.Seguranca.UseCases.Usuario.Autenticar
{
    public class UseCase : BaseUseCase<Output, Input, GenericOutput<Output>>, IUseCase
    {
        private readonly ISigningConfigurations criptografiaService;
        private readonly IJwtTokenService jwtTokenService;
        private readonly IUsuarioRepository usuarioRepository;

        public UseCase(ISigningConfigurations criptografiaService, IJwtTokenService jwtTokenService, IUsuarioRepository usuarioRepository)
        {
            this.criptografiaService = criptografiaService;
            this.jwtTokenService = jwtTokenService;
            this.usuarioRepository = usuarioRepository;
        }

        internal override async Task<GenericOutput<Output>> BussinesRole(Input input)
        {
            var genericOutput = new GenericOutput<Output>();


            string senhaCriptografada = criptografiaService.GerarHash(input.Senha);
            Domain.Seguranca.Entities.Usuario usuario = await usuarioRepository.GetByUsername(input.Username);

            if (usuario == null || usuario.Senha != senhaCriptografada)
            {
                genericOutput.AddError("Usuário ou senha incorretos.");
                return genericOutput;
            }

            string token = jwtTokenService.GerarToken(usuario.Id.ToString(), usuario.Username, usuario.TipoUsuario.ToString());

            var output = new Output()
            {
                Token = token,
                Username = usuario.Username,
            };


            genericOutput.Data = output;

            return genericOutput;
        }
    }
}