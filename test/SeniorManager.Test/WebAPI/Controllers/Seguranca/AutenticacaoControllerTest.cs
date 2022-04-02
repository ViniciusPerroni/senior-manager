using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using Moq;

using SeniorManager.Application.Comum;
using SeniorManager.WebAPI.Controllers;

using Autenticar = SeniorManager.Application.Seguranca.UseCases.Usuario.Autenticar;
using FluentAssertions;

namespace SeniorManager.Test.WebAPI.Controllers.Seguranca
{
    [TestClass]
    public class AutenticacaoControllerTest
    {
        [TestMethod]
        public async Task GetUsuarioPorUsernameSenhaReturnOK()
        {
            //Arrange
            var useCase = Mock.Of<Autenticar.IUseCase>();
            var input = new Autenticar.Input
            {
                Username = "usuario.teste"
            };
            var outputData = new Autenticar.Output
            {
                Token = "xxxxx",
                Username = "usuario.test"
            };
            var output = new GenericOutput<Autenticar.Output>(outputData);
            Mock.Get(useCase).Setup(x => x.Execute(input)).Returns(Task.FromResult(output));

            var controller = new AutenticarController(useCase);

            //Action
            var response = await controller.Autenticar(input);

            // Assert
            var okResult = response as OkObjectResult;
            okResult.Should().NotBeNull();

            var data = okResult.Value as GenericOutput<Autenticar.Output>;
            data.Should().NotBeNull();
            data.Ok.Should().BeTrue();
            data.Data.Token.Should().Be(output.Data.Token);
            data.Data.Username.Should().Be(output.Data.Username);
        }

        [TestMethod]
        public async Task GetUsuarioPorUsernameSenhaReturnError()
        {
            //Arrange
            var useCase = Mock.Of<Autenticar.IUseCase>();
            var input = new Autenticar.Input
            {
                Username = "usurio.teste"
            };
            var output = new GenericOutput<Autenticar.Output>();
            output.AddError("Mensagem de Erro");
            Mock.Get(useCase).Setup(x => x.Execute(input)).Returns(Task.FromResult(output));

            var controller = new AutenticarController(useCase);

            //Action
            var response = await controller.Autenticar(input);

            // Assert
            var badResult = response as BadRequestObjectResult;
            badResult.Should().NotBeNull();

            var data = badResult.Value as GenericOutput<Autenticar.Output>;
            data.Should().NotBeNull();
            data.Ok.Should().BeFalse();
            data.Data.Should().BeNull();
            data.Errors.Should().HaveCount(1);
            data.Errors.Should().Contain("Mensagem de Erro");
        }
    }
}