using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SeniorManager.Domain.Seguranca.Repositories;
using ModelSeguranca = SeniorManager.Domain.Seguranca.Entities;
using ModelComum = SeniorManager.Domain.Comum.Entities;
using SeniorManager.Application.Seguranca.UseCases.Usuario.BuscarPorUsername;
using FluentAssertions;
using SeniorManager.Application.Comum;

namespace SeniorManager.Test.Application.Seguranca.UseCases.Usuario
{
    [TestClass]
    public class BuscarPorUsernameTest
    {
        [TestMethod]
        public async Task ReturnOkOutput()
        {
            //Arrange
            var repository = Mock.Of<IUsuarioRepository>();
            var endereco = new ModelComum.Endereco("logradouro", "numero", "bairro", "complemento", "cep", "cidade", "estado")
            {
                Id = 1
            };
            var pessoaJuridica = new ModelComum.PessoaJuridica("nomeFantasia", "razaoSocial", "cnpj", "inscricaoMunicipal", "inscricaoEstadual", "telefone", "celular", "nomePessoaResponsavel", endereco)
            {
                Id = 1
            };
            var cliente = new ModelComum.Cliente(pessoaJuridica)
            {
                Id = 1
            };
            var usuario = new ModelSeguranca.Usuario("teste.username", "senha", "nome", "sobrenome", new DateTime(1987, 07, 18), cliente, true)
            {
                Id = 1
            };

            Mock.Get(repository).Setup(x => x.GetByUsername("teste.username")).Returns(Task.FromResult(usuario));

            var input = new Input
            {
                Username = "teste.username"
            };
            var useCase = new UseCase(repository);

            //Action
            var output = await useCase.Execute(input);

            //Assert
            output.Ok.Should().BeTrue();
            output.Data.Id.Should().Be(usuario.Id);
            output.Data.Nome.Should().Be(usuario.Nome);
            output.Data.SobreNome.Should().Be(usuario.Sobrenome);
            output.Data.Username.Should().Be(usuario.Username);
            output.Data.CodCliente.Should().Be(usuario.Cliente.Id);
        }

        [TestMethod]
        public async Task InputInvalida()
        {
            //Arrange
            var repository = Mock.Of<IUsuarioRepository>();
            var input = new Input();
            var useCase = new UseCase(repository);

            //Action
            var output = await useCase.Execute(input);

            //Assert
            output.Ok.Should().BeFalse();
            output.Errors.Should().HaveCount(1);
            output.Errors.Should().Contain("O campo 'Username' é obrigatório");
        }

        [TestMethod]
        public async Task UsuarioNaoLocalizado()
        {
            //Arrange
            var repository = Mock.Of<IUsuarioRepository>();
            Mock.Get(repository).Setup(x => x.GetByUsername("teste.username")).Returns(Task.FromResult((ModelSeguranca.Usuario)null));

            var input = new Input
            {
                Username = "teste.username"
            };
            var useCase = new UseCase(repository);

            //Action
            var output = await useCase.Execute(input);

            //Assert
            output.Ok.Should().BeFalse();
            output.Errors.Should().HaveCount(1);
            output.Errors.Should().Contain("Usuário ou senha inválido.");
        }

        [TestMethod]
        public async Task ExceptionExecute()
        {
            //Arrange
            var repository = Mock.Of<IUsuarioRepository>();
            Mock.Get(repository).Setup(x => x.GetByUsername("teste.username")).Throws(new Exception("Generic error."));

            var input = new Input
            {
                Username = "teste.username"
            };
            var useCase = new UseCase(repository);

            //Action
            var output = await useCase.Execute(input);

            //Assert
            output.Ok.Should().BeFalse();
            output.Errors.Should().HaveCount(1);
            output.Errors.Should().Contain("Generic error.");
        }
    }
}