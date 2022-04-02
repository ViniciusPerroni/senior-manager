using Microsoft.VisualStudio.TestTools.UnitTesting;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SeniorManager.Application.Comum.UseCases.Cliente.BuscarPorId;

using ComumEntities = SeniorManager.Domain.Comum.Entities;
using SeniorManager.Domain.Comum.Repositories;
using Moq;
using AutoMapper;
using SeniorManager.Application.Comum.Dtos;
using ComumModel = SeniorManager.Domain.Comum.Entities;
using FluentAssertions;

namespace SeniorManager.Test.Application.Comum.UseCases.Cliente
{
    [TestClass]
    public class BuscarPorIdTest
    {
        private List<ComumEntities.Cliente> _clientes;

        [TestInitialize]
        public void Initialize()
        {
            _clientes = new List<ComumEntities.Cliente>();

            var endereco1 = new ComumEntities.Endereco("logradouro", "numero", "bairro", "complemento", "cep", "cidade", "estado")
            {
                Id = 1
            };
            var pessoaJuridica1 = new ComumEntities.PessoaJuridica("nomeFantasia1", "razaoSocial1", "cnpj1", "inscricaoMunicipal", "inscricaoEstadual", "telefone", "celular", "nomePessoaResponsavel", endereco1)
            {
                Id = 1
            };
            _clientes.Add(new ComumEntities.Cliente(pessoaJuridica1)
            {
                Id = 1
            });


            var endereco2 = new ComumEntities.Endereco("logradouro", "numero", "bairro", "complemento", "cep", "cidade", "estado")
            {
                Id = 2
            };
            var pessoaJuridica2 = new ComumEntities.PessoaJuridica("nomeFantasia2", "razaoSocial2", "cnpj2", "inscricaoMunicipal", "inscricaoEstadual", "telefone", "celular", "nomePessoaResponsavel", endereco2)
            {
                Id = 2
            };
            _clientes.Add(new ComumEntities.Cliente(pessoaJuridica2)
            {
                Id = 2
            });
        }

        [TestMethod]
        public async Task BuscarPorIdReturnOk()
        {
            //Arrange
            var input = new Input() { Id = 1 };
            var repository = Mock.Of<IClienteRepository>();
            Mock.Get(repository)
                .Setup(x => x.GetById(input.Id))
                .Returns(Task.FromResult(_clientes.First(x=>x.Id == input.Id)));
            var mapper = Mock.Of<IMapper>();
            Mock.Get(mapper)
                .Setup(x => x.Map<ClienteDto>(It.IsAny<ComumModel.Cliente>()))
                .Returns(new ClienteDto());

            var useCase = new UseCase(repository, mapper);

            //Action
            var output = await useCase.Execute(input);

            //Assert
            output.Ok.Should().BeTrue();
            output.Data.Should().NotBeNull();
        }

        [TestMethod]
        public async Task BuscarPorIdReturnIdObrigatorioError()
        {
            //Arrange
            var input = new Input();
            var repository = Mock.Of<IClienteRepository>();
            var mapper = Mock.Of<IMapper>();

            var useCase = new UseCase(repository, mapper);

            //Action
            var output = await useCase.Execute(input);

            //Assert
            output.Ok.Should().BeFalse();
            output.Data.Should().BeNull();
            output.Errors.Should().HaveCount(1);
            output.Errors.Should().Contain("Parâmetro Id inválido.");
        }

        [TestMethod]
        public async Task BuscarPorIdReturnGenericError()
        {
            //Arrange
            var input = new Input() { Id = 1 };
            var repository = Mock.Of<IClienteRepository>();
            Mock.Get(repository)
                .Setup(x => x.GetById(input.Id))
                .Throws(new Exception("Mensagem de erro"));
            var mapper = Mock.Of<IMapper>();

            var useCase = new UseCase(repository, mapper);

            //Action
            var output = await useCase.Execute(input);

            //Assert
            output.Ok.Should().BeFalse();
            output.Data.Should().BeNull();
            output.Errors.Should().HaveCount(1);
            output.Errors.Should().Contain("Mensagem de erro");
        }
    }
}