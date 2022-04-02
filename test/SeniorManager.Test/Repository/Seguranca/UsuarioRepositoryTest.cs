using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeniorManager.Repository.Contexts;
using SeniorManager.Repository.Seguranca;
using FluentAssertions;
using SeniorManager.Domain.Seguranca.Entities;
using SeniorManager.Domain.Comum.Entities;

namespace SeniorManager.Test.Repository.Seguranca
{
    [TestClass]
    public class UsuarioRepositoryTest
    {
        [TestMethod]
        public async Task GetByUsernameReturnUsuario()
        {
            //Arrange
            var endereco = new Endereco("logradouro", "numero", "bairro", "complemento", "cep", "cidade", "estado")
            {
                Id = 1
            };
            var pessoaJuridica = new PessoaJuridica("nomeFantasia", "razaoSocial", "cnpj", "inscricaoMunicipal", "inscricaoEstadual", "telefone", "celular", "nomePessoaResponsavel", endereco)
            {
                Id = 1
            };
            var cliente = new Cliente(pessoaJuridica)
            {
                Id = 1
            };
            var usuario = new Usuario("teste.username", "senha", "nome", "sobrenome", new DateTime(1987, 07, 18), cliente, true)
            {
                Id = 1
            };
            var options = new DbContextOptionsBuilder<SeniorManagerDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var dbContext = new SeniorManagerDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Usuarios.Add(usuario);
            dbContext.SaveChanges();
            var repository = new UsuarioRepository(dbContext);

            //Action
            var result = await repository.GetByUsername("teste.username");

            //Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }

        [TestMethod]
        public async Task GetByUsernameReturnNull()
        {
            //Arrange
            var endereco = new Endereco("logradouro", "numero", "bairro", "complemento", "cep", "cidade", "estado")
            {
                Id = 1
            };
            var pessoaJuridica = new PessoaJuridica("nomeFantasia", "razaoSocial", "cnpj", "inscricaoMunicipal", "inscricaoEstadual", "telefone", "celular", "nomePessoaResponsavel", endereco)
            {
                Id = 1
            };
            var cliente = new Cliente(pessoaJuridica)
            {
                Id = 1
            };
            var usuario = new Usuario("teste.username", "senha", "nome", "sobrenome", new DateTime(1987, 07, 18), cliente, true)
            {
                Id = 1
            };
            var options = new DbContextOptionsBuilder<SeniorManagerDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var dbContext = new SeniorManagerDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Usuarios.Add(usuario);
            dbContext.SaveChanges();
            var repository = new UsuarioRepository(dbContext);

            //Action
            var result = await repository.GetByUsername("teste.usernam");

            //Assert
            result.Should().BeNull();
        }
    }
}