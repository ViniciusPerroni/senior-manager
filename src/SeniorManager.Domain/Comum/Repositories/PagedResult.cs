using SeniorManager.Domain.Comum.Entities;
using System.Collections.Generic;

namespace SeniorManager.Domain.Comum.Repositories
{
    public class PagedResult<T> where T : BaseEntity
    {
        public int RowCount { get; set; }
        public IList<T> Rows { get; set; }
    }
}