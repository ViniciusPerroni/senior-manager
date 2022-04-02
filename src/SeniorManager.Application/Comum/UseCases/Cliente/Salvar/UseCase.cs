using AutoMapper;

using SeniorManager.Application.Comum.Dtos;
using SeniorManager.Domain.Comum.Repositories;

using System;
using System.Threading.Tasks;

using ModelComum = SeniorManager.Domain.Comum.Entities;

namespace SeniorManager.Application.Comum.UseCases.Cliente.Salvar
{
    public class UseCase : BaseUseCase<ClienteDto, Input, GenericOutput<ClienteDto>>, IUseCase
    {
        private IClienteRepository clienteRepository;
        private IMapper mapper;

        public UseCase(IMapper mapper, IClienteRepository clienteRepository)
        {
            this.mapper = mapper;
            this.clienteRepository = clienteRepository;
        }

        internal async override Task<GenericOutput<ClienteDto>> BussinesRole(Input input)
        {
            try
            {
                var output = new GenericOutput<ClienteDto>();
                ModelComum.Cliente cliente = null;

                if (input.Data.Id <= 0)
                    cliente = await CriarCliente(input);
                else
                    cliente = await Editar(input);

                if (cliente == null)
                {
                    output.AddError("Cliente não localizado");
                    return output;
                }

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

        private async Task<ModelComum.Cliente> CriarCliente(Input input)
        {
            var enderecoDto = input.Data.PessoaJuridica.Endereco;
            var endereco = new ModelComum.Endereco(enderecoDto.Logradouro, enderecoDto.Numero, enderecoDto.Bairro, enderecoDto.Complemento, 
                enderecoDto.Cep, enderecoDto.Cidade, enderecoDto.Estado);

            var matrizDto = input.Data.PessoaJuridica;
            var matriz = new ModelComum.PessoaJuridica(matrizDto.NomeFantasia, matrizDto.RazaoSocial, matrizDto.Cnpj, matrizDto.InscricaoEstadual, matrizDto.InscricaoEstadual,
                matrizDto.Telefone, matrizDto.Celular, matrizDto.NomePessoaResponsavel, endereco);
            
            var cliente = new ModelComum.Cliente(matriz);

            await clienteRepository.Create(cliente);

            return cliente;
        }

        private async Task<ModelComum.Cliente> Editar(Input input)
        {
            var cliente = await clienteRepository.GetById(input.Data.Id);

            if (cliente == null)
                return null;

            var matrizDto = input.Data.PessoaJuridica;
            cliente.PessoaJuridica.Editar(matrizDto.NomeFantasia, matrizDto.RazaoSocial, matrizDto.Cnpj, matrizDto.InscricaoEstadual, matrizDto.InscricaoEstadual,
                matrizDto.Telefone, matrizDto.Celular, matrizDto.NomePessoaResponsavel);

            var enderecoDto = input.Data.PessoaJuridica.Endereco;
            cliente.PessoaJuridica.Endereco.Editar(enderecoDto.Logradouro, enderecoDto.Numero, enderecoDto.Bairro, enderecoDto.Complemento,
                enderecoDto.Cep, enderecoDto.Cidade, enderecoDto.Estado);

            await clienteRepository.Update(cliente);

            return cliente;
        }


    }
}