using AutoMapper;
using SeniorManager.Application.Comum.Dtos;
using SeniorManager.Crosscutting.Interfaces;
using SeniorManager.Domain.Comum.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorManager.Application.Comum.UseCases.Cliente.Listar
{
    public class UseCase : BaseUseCase<IList<ClienteDto>, Input, GenericPagedOutput<IList<ClienteDto>>>, IUseCase
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IUrlBuilder urlBuilder;
        private readonly IMapper mapper;
        
        public UseCase(IClienteRepository clienteRepository, IUrlBuilder urlBuilder, IMapper mapper)
        {
            this.clienteRepository = clienteRepository;
            this.urlBuilder = urlBuilder;
            this.mapper = mapper;
        }

        internal async override Task<GenericPagedOutput<IList<ClienteDto>>> BussinesRole(Input input)
        {
            var output = new GenericPagedOutput<IList<ClienteDto>>();

            try
            {
                var pagedResult = await clienteRepository.ListPaged(input.FilterBy, input.PageSize, input.PageNumber, input.OrderBy, input.OrderOrientation);
                output.Data = mapper.Map<List<ClienteDto>>(pagedResult.Rows);

                var baseUrl = urlBuilder
                    .ToUrl(input.BaseUrl)
                    .AddParameter("filterBy", input.FilterBy)
                    .AddParameter("orderBy", input.OrderBy)
                    .AddParameter("orderOrientation", input.OrderOrientation)
                    .Build();

                output.Summary = new PagedList.PagedList(baseUrl, pagedResult.RowCount, input.PageNumber, input.PageSize);
            }
            catch(Exception ex)
            {
                output.AddError(ex.Message);
            }
             
            return output;
        }
    }
}