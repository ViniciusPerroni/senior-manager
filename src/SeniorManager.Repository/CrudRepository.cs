using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeniorManager.Domain.Comum.Entities;
using SeniorManager.Domain.Comum.Repositories;
using SeniorManager.Repository.Contexts;

namespace SeniorManager.Repository
{
    public class CrudRepository<T> : ICrudRepository<T> where T : BaseEntity
    {
        protected readonly SeniorManagerDbContext _dbContext;

        public CrudRepository(SeniorManagerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            await Delete(entity);
        }

        public async Task Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>()
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public async Task Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateMany(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().UpdateRange(entities);
            await _dbContext.SaveChangesAsync();
        }

        protected async Task<PagedResult<T>> GetPaged(IQueryable<T> query, int page, int pageSize)
        {
            var pagedResult = new PagedResult<T>();
            var skip = (page - 1) * pageSize;
            pagedResult.RowCount = query.Count();

            pagedResult.Rows = await query.Skip(skip).Take(pageSize).ToListAsync();

            return pagedResult;
        }
    }
}