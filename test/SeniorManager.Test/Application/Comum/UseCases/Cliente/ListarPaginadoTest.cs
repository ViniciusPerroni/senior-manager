using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeniorManager.Domain.Comum.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using SeniorManager.Application.Comum.UseCases.Cliente.Listar;
using ComumEntities = SeniorManager.Domain.Comum.Entities;
using SeniorManager.Domain.Comum.Repositories;
using Moq;
using FluentAssertions;
using SeniorManager.Crosscutting.Helpers;
using AutoMapper;
using SeniorManager.Application.Comum.Dtos;

namespace SeniorManager.Test.Application.Comum.UseCases.Cliente
{
    [TestClass]
    public class ListarPaginadoTest
    {
        [TestMethod]
        public async Task ListarComOsParametrosPadraoOk()
        {
            //Arrange
            var input = new Input();
            var resultQuery = new PagedResult<ComumEntities.Cliente>();
            var repository = Mock.Of<IClienteRepository>();
            Mock.Get(repository)
                .Setup(x => x.ListPaged(input.FilterBy, input.PageSize, input.PageNumber, input.OrderBy, input.OrderOrientation))
                .Returns(Task.FromResult(resultQuery));

            var urlBuilder = new UrlBuilder();
            var mapper = Mock.Of<IMapper>();
            Mock.Get(mapper)
                .Setup(m => m.Map<List<ComumEntities.Cliente>, List<ClienteDto>>(It.IsAny<List<ComumEntities.Cliente>>()))
                .Returns(new List<ClienteDto>());

            var useCase = new UseCase(repository, urlBuilder, mapper);

            //Action
            var output = await useCase.Execute(input);

            //Assert
            output.Ok.Should().BeTrue();
        }
    }
}