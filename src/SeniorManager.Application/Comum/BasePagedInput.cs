using System.Collections.Generic;

namespace SeniorManager.Application.Comum
{
    public class BasePagedInput : BaseInput
    {
        public BasePagedInput()
        {
            PageNumber = 1;
            PageSize = 10;
            OrderOrientation = "asc";
        }

        public string BaseUrl { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string OrderOrientation { get; set; }
        public string FilterBy { get; set; }
        internal IList<string> Columns { get; set; }

        internal override void Validate()
        {
            Errors = new List<string>();

            if (!string.IsNullOrEmpty(FilterBy) && !Columns.Contains(FilterBy))
                Errors.Add("Critério de filtro inválido.");
            if (!string.IsNullOrEmpty(OrderBy) && !Columns.Contains(OrderBy))
                Errors.Add("Critério de ordenação inválido.");
            if (!string.IsNullOrEmpty(OrderOrientation) && (OrderOrientation != "asc" && OrderOrientation != "desc"))
                Errors.Add("Critério de orientação da ordenação inválido.");
        }
    }
}