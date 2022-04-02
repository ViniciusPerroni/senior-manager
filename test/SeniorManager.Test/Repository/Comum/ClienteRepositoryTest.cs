using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeniorManager.Domain.Comum.Entities;
using SeniorManager.Repository.Comum;
using SeniorManager.Repository.Contexts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeniorManager.Test.Repository.Comum
{
    [TestClass]
    public class ClienteRepositoryTest
    {
        private List<Cliente> _clientes;

        [TestInitialize]
        public void Initialize()
        {
            _clientes = new List<Cliente>();

            var endereco1 = new Endereco("logradouro", "numero", "bairro", "complemento", "cep", "cidade", "estado")
            {
                Id = 1
            };
            var pessoaJuridica1 = new PessoaJuridica("nomeFantasia1", "razaoSocial1", "cnpj1", "inscricaoMunicipal", "inscricaoEstadual", "telefone", "celular", "nomePessoaResponsavel", endereco1)
            {
                Id = 1
            };
            _clientes.Add(new Cliente(pessoaJuridica1)
            {
                Id = 1
            });


            var endereco2 = new Endereco("logradouro", "numero", "bairro", "complemento", "cep", "cidade", "estado")
            {
                Id = 2
            };
            var pessoaJuridica2 = new PessoaJuridica("nomeFantasia2", "razaoSocial2", "cnpj2", "inscricaoMunicipal", "inscricaoEstadual", "telefone", "celular", "nomePessoaResponsavel", endereco2)
            {
                Id = 2
            };
            _clientes.Add(new Cliente(pessoaJuridica2)
            {
                Id = 2
            });

            var endereco3 = new Endereco("logradouro", "numero", "bairro", "complemento", "cep", "cidade", "estado")
            {
                Id = 3
            };
            var pessoaJuridica3 = new PessoaJuridica("nomeFantasia3", "razaoSocial3", "cnpj3", "inscricaoMunicipal", "inscricaoEstadual", "telefone", "celular", "nomePessoaResponsavel", endereco3)
            {
                Id = 3
            };
            _clientes.Add(new Cliente(pessoaJuridica3)
            {
                Id = 3
            });

            var endereco4 = new Endereco("logradouro", "numero", "bairro", "complemento", "cep", "cidade", "estado")
            {
                Id = 4
            };
            var pessoaJuridica4 = new PessoaJuridica("nomeFantasia4", "razaoSocial4", "cnpj4", "inscricaoMunicipal", "inscricaoEstadual", "telefone", "celular", "nomePessoaResponsavel", endereco4)
            {
                Id = 4
            };
            _clientes.Add(new Cliente(pessoaJuridica4)
            {
                Id = 4
            });
        }

        [TestMethod]
        public async Task ListPagedNotFilterAndOrderAscDefaultShouldResultOk()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SeniorManagerDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var dbContext = new SeniorManagerDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Clientes.AddRange(_clientes);
            dbContext.SaveChanges();
            var repository = new ClienteRepository(dbContext);

            //Action
            var result = await repository.ListPaged(string.Empty, 2, 1, string.Empty, "asc");

            //Assert
            result.Should().NotBeNull();
            result.RowCount.Should().Be(4);
            result.Rows.Should().HaveCount(2);
            result.Rows.First().PessoaJuridica.Id.Should().Be(1);
        }

        [TestMethod]
        public async Task ListPagedFilterByNomeFantasiaAndOrderAscDefaultShouldResultOk()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SeniorManagerDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var dbContext = new SeniorManagerDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Clientes.AddRange(_clientes);
            dbContext.SaveChanges();
            var repository = new ClienteRepository(dbContext);

            //Action
            var result = await repository.ListPaged("razaoSocial2", 2, 1, string.Empty, "asc");

            //Assert
            result.Should().NotBeNull();
            result.RowCount.Should().Be(1);
            result.Rows.Should().HaveCount(1);
            result.Rows.First().PessoaJuridica.Id.Should().Be(2);
        }

        [TestMethod]
        public async Task ListPagedFilterByCnpjAndOrderAscDefaultShouldResultOk()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SeniorManagerDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var dbContext = new SeniorManagerDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Clientes.AddRange(_clientes);
            dbContext.SaveChanges();
            var repository = new ClienteRepository(dbContext);

            //Action
            var result = await repository.ListPaged("cnpj3", 2, 1, string.Empty, "asc");

            //Assert
            result.Should().NotBeNull();
            result.RowCount.Should().Be(1);
            result.Rows.Should().HaveCount(1);
            result.Rows.First().PessoaJuridica.Id.Should().Be(3);
        }

        [TestMethod]
        public async Task ListPagedFilterByRazaoSocialAndOrderAscDefaultShouldResultOk()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SeniorManagerDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var dbContext = new SeniorManagerDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Clientes.AddRange(_clientes);
            dbContext.SaveChanges();
            var repository = new ClienteRepository(dbContext);

            //Action
            var result = await repository.ListPaged("nomeFantasia1", 2, 1, string.Empty, "asc");

            //Assert
            result.Should().NotBeNull();
            result.RowCount.Should().Be(1);
            result.Rows.Should().HaveCount(1);
            result.Rows.First().PessoaJuridica.Id.Should().Be(1);
        }

        [TestMethod]
        public async Task ListPagedNotFilterAndOrderDesDefaultcShouldResultOk()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SeniorManagerDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var dbContext = new SeniorManagerDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Clientes.AddRange(_clientes);
            dbContext.SaveChanges();
            var repository = new ClienteRepository(dbContext);

            //Action
            var result = await repository.ListPaged(string.Empty, 2, 1, string.Empty, "desc");

            //Assert
            result.Should().NotBeNull();
            result.RowCount.Should().Be(4);
            result.Rows.Should().HaveCount(2);
            result.Rows.First().PessoaJuridica.Id.Should().Be(4);
        }

        [TestMethod]
        public async Task ListPagedNotFilterAndOrderAscByNomeFantasiaShouldResultOk()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SeniorManagerDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var dbContext = new SeniorManagerDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Clientes.AddRange(_clientes);
            dbContext.SaveChanges();
            var repository = new ClienteRepository(dbContext);

            //Action
            var result = await repository.ListPaged(string.Empty, 2, 1, "nomeFantasia", "asc");

            //Assert
            result.Should().NotBeNull();
            result.RowCount.Should().Be(4);
            result.Rows.Should().HaveCount(2);
            result.Rows.First().PessoaJuridica.Id.Should().Be(1);
        }

        [TestMethod]
        public async Task ListPagedNotFilterAndOrderDescByNomeFantasiaShouldResultOk()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SeniorManagerDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var dbContext = new SeniorManagerDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Clientes.AddRange(_clientes);
            dbContext.SaveChanges();
            var repository = new ClienteRepository(dbContext);

            //Action
            var result = await repository.ListPaged(string.Empty, 2, 1, "nomeFantasia", "desc");

            //Assert
            result.Should().NotBeNull();
            result.RowCount.Should().Be(4);
            result.Rows.Should().HaveCount(2);
            result.Rows.First().PessoaJuridica.Id.Should().Be(4);
        }

        [TestMethod]
        public async Task ListPagedNotFilterAndOrderAscByCnpjShouldResultOk()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SeniorManagerDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var dbContext = new SeniorManagerDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Clientes.AddRange(_clientes);
            dbContext.SaveChanges();
            var repository = new ClienteRepository(dbContext);

            //Action
            var result = await repository.ListPaged(string.Empty, 2, 1, "cnpj", "asc");

            //Assert
            result.Should().NotBeNull();
            result.RowCount.Should().Be(4);
            result.Rows.Should().HaveCount(2);
            result.Rows.First().PessoaJuridica.Id.Should().Be(1);
        }

        [TestMethod]
        public async Task ListPagedNotFilterAndOrderDescByCnpjShouldResultOk()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<SeniorManagerDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            var dbContext = new SeniorManagerDbContext(options);
            dbContext.Database.EnsureDeleted();
            dbContext.Clientes.AddRange(_clientes);
            dbContext.SaveChanges();
            var repository = new ClienteRepository(dbContext);

            //Action
            var result = await repository.ListPaged(string.Empty, 2, 1, "cnpj", "desc");

            //Assert
            result.Should().NotBeNull();
            result.RowCount.Should().Be(4);
            result.Rows.Should().HaveCount(2);
            result.Rows.First().PessoaJuridica.Id.Should().Be(4);
        }
    }
}