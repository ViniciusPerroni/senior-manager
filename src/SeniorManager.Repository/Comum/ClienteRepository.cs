using SeniorManager.Domain.Comum.Entities;
using SeniorManager.Domain.Comum.Repositories;
using SeniorManager.Repository.Contexts;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorManager.Repository.Comum
{
    public class ClienteRepository : CrudRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(SeniorManagerDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<PagedResult<Cliente>> ListPaged(string filter, int pageSize, int page, string orderBy, string orderOrientation)
        {
            var query = GetAll();

            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.PessoaJuridica.Cnpj.Contains(filter)
                                    || x.PessoaJuridica.NomeFantasia.ToUpper().Contains(filter.ToUpper())
                                    || x.PessoaJuridica.RazaoSocial.ToUpper().Contains(filter.ToUpper()));

            switch (orderBy)
            {
                case "cnpj":
                    query = orderOrientation.Equals("asc") 
                        ? query.OrderBy(x => x.PessoaJuridica.Cnpj)
                        : query.OrderByDescending(x => x.PessoaJuridica.Cnpj);
                    break;
                case "nomeFantasia":
                    query = orderOrientation.Equals("asc")
                        ? query.OrderBy(x => x.PessoaJuridica.NomeFantasia)
                        : query.OrderByDescending(x => x.PessoaJuridica.NomeFantasia);
                    break;
                default:
                    query = orderOrientation.Equals("asc")
                        ? query.OrderBy(x => x.PessoaJuridica.RazaoSocial)
                        : query.OrderByDescending(x => x.PessoaJuridica.RazaoSocial);
                    break;
            }

            return await GetPaged(query, page, pageSize);
        }
    }
}