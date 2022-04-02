using System.Threading.Tasks;
using SeniorManager.Application.Comum;

namespace SeniorManager.Application.Seguranca.UseCases.Usuario.BuscarPorUsername
{
    public interface IUseCase
    {
        Task<GenericOutput<Output>> Execute(Input input);
    }
}