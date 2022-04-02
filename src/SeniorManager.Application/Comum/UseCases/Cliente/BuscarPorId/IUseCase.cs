using SeniorManager.Application.Comum.Dtos;
using System.Threading.Tasks;

namespace SeniorManager.Application.Comum.UseCases.Cliente.BuscarPorId
{
    public interface IUseCase
    {
        Task<GenericOutput<ClienteDto>> Execute(Input input);
    }
}