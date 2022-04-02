using SeniorManager.Application.Comum.Dtos;

using System;
using System.Collections.Generic;

namespace SeniorManager.Application.Comum
{
    public class GenericPagedOutput<T> : GenericOutput<T>
    {
        public PagedList.PagedList Summary { get; set; }
    }
}