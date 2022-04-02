using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeniorManager.Domain.Seguranca.Entities;
using SeniorManager.Domain.Seguranca.Repositories;
using SeniorManager.Repository.Contexts;

namespace SeniorManager.Repository.Seguranca
{
    public class UsuarioRepository : CrudRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(SeniorManagerDbContext dbContext) : base(dbContext) { }

        public async Task<Usuario> GetByUsername(string username)
        {
            return await _dbContext.Set<Usuario>().FirstOrDefaultAsync(u => u.Username.Equals(username));
        }
    }
}