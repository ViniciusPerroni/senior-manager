using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeniorManager.Domain.Comum.Entities;

namespace SeniorManager.Domain.Comum.Repositories
{
    public interface ICrudRepository<T> where T : BaseEntity
    {
        Task Create(T entity);
        Task Delete(int id);
        Task Delete(T entity);
        Task<T> GetById(int id);
        IQueryable<T> GetAll();
        Task Update(T entity);
        Task UpdateMany(IEnumerable<T> entities);
    }
}