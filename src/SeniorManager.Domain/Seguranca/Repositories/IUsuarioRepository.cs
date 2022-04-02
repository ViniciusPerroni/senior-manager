using System.Threading.Tasks;
using SeniorManager.Domain.Comum.Repositories;
using SeniorManager.Domain.Seguranca.Entities;

namespace SeniorManager.Domain.Seguranca.Repositories
{
    public interface IUsuarioRepository : ICrudRepository<Usuario>
    {
        Task<Usuario> GetByUsername(string username);
    }
}