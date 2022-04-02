using System.Threading.Tasks;
using SeniorManager.Application.Comum;

namespace SeniorManager.Application.Seguranca.UseCases.Usuario.Autenticar
{
    public interface IUseCase
    {
        Task<GenericOutput<Output>> Execute(Input input);
    }
}