using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SeniorManager.Application.Comum;
using SeniorManager.Application.Comum.Dtos;
using SeniorManager.WebAPI.Controllers.Comum;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComumCliente = SeniorManager.Application.Comum.UseCases.Cliente;
using ComumDtos = SeniorManager.Application.Comum.Dtos;

namespace SeniorManager.Test.WebAPI.Controllers.Comum
{

    [TestClass]
    public class ClienteControllerTest
    {
        [TestMethod]
        public async Task GetPagedListReturnOk()
        {
            //Arrange
            var input = new ComumCliente.Listar.Input();
            var output = new GenericPagedOutput<IList<ClienteDto>>();
            output.Data = new List<ClienteDto>();

            var useCase = Mock.Of<ComumCliente.Listar.IUseCase>();
            Mock.Get(useCase).Setup(x => x.Execute(input)).Returns(Task.FromResult(output));

            var useCaseBuscarPorId = Mock.Of<ComumCliente.BuscarPorId.IUseCase>();
            var useCaseSalvar = Mock.Of<ComumCliente.Salvar.IUseCase>();

            var controller = new ClienteController(useCase, useCaseBuscarPorId, useCaseSalvar);

            //Action
            var response = await controller.Paged(input);

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();

            var data = okResult.Value as GenericPagedOutput<IList<ClienteDto>>;

            data.Should().NotBeNull();
            data.Ok.Should().BeTrue();
            data.Data.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetPagedListReturnGenericError()
        {
            //Arrange
            var input = new ComumCliente.Listar.Input();
            var output = new GenericPagedOutput<IList<ClienteDto>>();
            output.AddError("Mensagem de Erro");

            var useCase = Mock.Of<ComumCliente.Listar.IUseCase>();
            Mock.Get(useCase).Setup(x => x.Execute(input)).Returns(Task.FromResult(output));

            var useCaseBuscarPorId = Mock.Of<ComumCliente.BuscarPorId.IUseCase>();
            var useCaseSalvar = Mock.Of<ComumCliente.Salvar.IUseCase>();

            var controller = new ClienteController(useCase, useCaseBuscarPorId, useCaseSalvar);

            //Action
            var response = await controller.Paged(input);

            //Assert
            var badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();

            var data = badResult.Value as GenericPagedOutput<IList<ClienteDto>>;

            data.Should().NotBeNull();
            data.Ok.Should().BeFalse();
            data.Data.Should().BeNull();
            data.Errors.Should().HaveCount(1);
            data.Errors.Should().Contain("Mensagem de Erro");
        }

        [TestMethod]
        public async Task GetReturnOk()
        {
            //Arrange
            var output = new GenericOutput<ComumDtos.ClienteDto>();
            output.Data = new ComumDtos.ClienteDto();
            var useCase = Mock.Of<ComumCliente.BuscarPorId.IUseCase>();
            Mock.Get(useCase).Setup(x => x.Execute(It.IsAny<ComumCliente.BuscarPorId.Input>())).Returns(Task.FromResult(output));

            var useCaseListar = Mock.Of<ComumCliente.Listar.IUseCase>();
            var useCaseSalvar = Mock.Of<ComumCliente.Salvar.IUseCase>();

            var controller = new ClienteController(useCaseListar, useCase, useCaseSalvar);

            //Action
            var response = await controller.Get(1);

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();

            var data = okResult.Value as GenericOutput<ComumDtos.ClienteDto>;

            data.Should().NotBeNull();
            data.Ok.Should().BeTrue();
            data.Data.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetReturnGenericError()
        {
            //Arrange
            var output = new GenericOutput<ComumDtos.ClienteDto>();
            output.AddError("Mensagem de Erro");
            var useCase = Mock.Of<ComumCliente.BuscarPorId.IUseCase>();
            Mock.Get(useCase).Setup(x => x.Execute(It.IsAny<ComumCliente.BuscarPorId.Input>())).Returns(Task.FromResult(output));

            var useCaseListar = Mock.Of<ComumCliente.Listar.IUseCase>();
            var useCaseSalvar = Mock.Of<ComumCliente.Salvar.IUseCase>();

            var controller = new ClienteController(useCaseListar, useCase, useCaseSalvar);

            //Action
            var response = await controller.Get(1);

            //Assert
            var badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();

            var data = badResult.Value as GenericOutput<ComumDtos.ClienteDto>;

            data.Should().NotBeNull();
            data.Ok.Should().BeFalse();
            data.Data.Should().BeNull();
            data.Errors.Should().HaveCount(1);
            data.Errors.Should().Contain("Mensagem de Erro");
        }

        [TestMethod]
        public async Task PostReturnOk()
        {
            //Arrange
            var input = new ComumCliente.Salvar.Input();
            var output = new GenericOutput<ComumDtos.ClienteDto>();
            output.Data = new ComumDtos.ClienteDto();
            var useCase = Mock.Of<ComumCliente.Salvar.IUseCase>();
            Mock.Get(useCase).Setup(x => x.Execute(It.IsAny<ComumCliente.Salvar.Input>())).Returns(Task.FromResult(output));

            var useCaseListar = Mock.Of<ComumCliente.Listar.IUseCase>();
            var useCaseBuscarPorId = Mock.Of<ComumCliente.BuscarPorId.IUseCase>();

            var controller = new ClienteController(useCaseListar, useCaseBuscarPorId, useCase);

            //Action
            var response = await controller.Post(input);

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();

            var data = okResult.Value as GenericOutput<ComumDtos.ClienteDto>;

            data.Should().NotBeNull();
            data.Ok.Should().BeTrue();
            data.Data.Should().NotBeNull();
        }

        [TestMethod]
        public async Task PostReturnGenericError()
        {
            //Arrange
            var input = new ComumCliente.Salvar.Input();
            var output = new GenericOutput<ComumDtos.ClienteDto>();
            output.AddError("Mensagem de Erro");
            var useCase = Mock.Of<ComumCliente.Salvar.IUseCase>();
            Mock.Get(useCase).Setup(x => x.Execute(It.IsAny<ComumCliente.Salvar.Input>())).Returns(Task.FromResult(output));

            var useCaseListar = Mock.Of<ComumCliente.Listar.IUseCase>();
            var useCaseBuscarPorId = Mock.Of<ComumCliente.BuscarPorId.IUseCase>();

            var controller = new ClienteController(useCaseListar, useCaseBuscarPorId, useCase);

            //Action
            var response = await controller.Post(input);

            //Assert
            var badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();

            var data = badResult.Value as GenericOutput<ComumDtos.ClienteDto>;

            data.Should().NotBeNull();
            data.Ok.Should().BeFalse();
            data.Data.Should().BeNull();
            data.Errors.Should().HaveCount(1);
            data.Errors.Should().Contain("Mensagem de Erro");
        }

        [TestMethod]
        public async Task PutReturnOk()
        {
            //Arrange
            var input = new ComumCliente.Salvar.Input();
            var output = new GenericOutput<ComumDtos.ClienteDto>();
            output.Data = new ComumDtos.ClienteDto();
            var useCase = Mock.Of<ComumCliente.Salvar.IUseCase>();
            Mock.Get(useCase).Setup(x => x.Execute(It.IsAny<ComumCliente.Salvar.Input>())).Returns(Task.FromResult(output));

            var useCaseListar = Mock.Of<ComumCliente.Listar.IUseCase>();
            var useCaseBuscarPorId = Mock.Of<ComumCliente.BuscarPorId.IUseCase>();

            var controller = new ClienteController(useCaseListar, useCaseBuscarPorId, useCase);

            //Action
            var response = await controller.Put(input);

            //Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();

            var data = okResult.Value as GenericOutput<ComumDtos.ClienteDto>;

            data.Should().NotBeNull();
            data.Ok.Should().BeTrue();
            data.Data.Should().NotBeNull();
        }

        [TestMethod]
        public async Task PutReturnGenericError()
        {
            //Arrange
            var input = new ComumCliente.Salvar.Input();
            var output = new GenericOutput<ComumDtos.ClienteDto>();
            output.AddError("Mensagem de Erro");
            var useCase = Mock.Of<ComumCliente.Salvar.IUseCase>();
            Mock.Get(useCase).Setup(x => x.Execute(It.IsAny<ComumCliente.Salvar.Input>())).Returns(Task.FromResult(output));

            var useCaseListar = Mock.Of<ComumCliente.Listar.IUseCase>();
            var useCaseBuscarPorId = Mock.Of<ComumCliente.BuscarPorId.IUseCase>();

            var controller = new ClienteController(useCaseListar, useCaseBuscarPorId, useCase);

            //Action
            var response = await controller.Put(input);

            //Assert
            var badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();

            var data = badResult.Value as GenericOutput<ComumDtos.ClienteDto>;

            data.Should().NotBeNull();
            data.Ok.Should().BeFalse();
            data.Data.Should().BeNull();
            data.Errors.Should().HaveCount(1);
            data.Errors.Should().Contain("Mensagem de Erro");
        }
    }
}