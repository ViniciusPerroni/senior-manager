using SeniorManager.Domain.Comum.Entities;

using System.Threading.Tasks;

namespace SeniorManager.Domain.Comum.Repositories
{
    public interface IClienteRepository : ICrudRepository<Cliente>
    {
        Task<PagedResult<Cliente>> ListPaged(string filter, int pageSize, int page, string orderBy, string orderOrientation);
    }
}