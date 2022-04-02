using AutoMapper;
using SeniorManager.Application.Comum.Dtos;
using SeniorManager.Domain.Comum.Repositories;
using System;
using System.Threading.Tasks;

namespace SeniorManager.Application.Comum.UseCases.Cliente.BuscarPorId
{
    public class UseCase : BaseUseCase<ClienteDto, Input, GenericOutput<ClienteDto>>, IUseCase
    {
        private readonly IClienteRepository clienteRepository;
        private readonly IMapper mapper;

        public UseCase(IClienteRepository clienteRepository, IMapper mapper)
        {
            this.clienteRepository = clienteRepository;
            this.mapper = mapper;
        }

        internal async override Task<GenericOutput<ClienteDto>> BussinesRole(Input input)
        {
            try
            {
                var output = new GenericOutput<ClienteDto>();
                var cliente = await clienteRepository.GetById(input.Id);

                if (cliente == null)
                    output.AddError("Cliente não localizado");
                else
                    output.Data = mapper.Map<ClienteDto>(cliente);

                return output;
            }
            catch(Exception ex)
            {
                var output = new GenericOutput<ClienteDto>();
                output.AddError(ex.Message);

                return output;
            }
        }
    }
}