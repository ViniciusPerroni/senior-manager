using SeniorManager.Application.Comum.Dtos;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorManager.Application.Comum.UseCases.Cliente.Listar
{
    public interface IUseCase
    {
        Task<GenericPagedOutput<IList<ClienteDto>>> Execute(Input input);
    }
}