using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SeniorManager.Application.Comum;

namespace SeniorManager.Test.Application.Comum
{
    [TestClass]
    public class GenericOutputTest
    {
        [TestMethod]
        public void CheckGenericOutputInvalida()
        {
            //Arrange
            var output = new GenericOutput<string>();
            output.AddError("Mensagem erro");

            //Assert
            output.Ok.Should().BeFalse();
            output.Errors.Should().HaveCount(1);
            output.Errors.Should().Contain("Mensagem erro");
        }

        [TestMethod]
        public void CriarGenericOutputValida()
        {
            //Arrange
            var output = new GenericOutput<string>("Data");

            //Assert
            output.Ok.Should().BeTrue();
            output.Errors.Should().HaveCount(0);
            output.Data.Should().Be("Data");
        }

        [TestMethod]
        public void AddErrorsGenericOutput()
        {
            //Arrange
            var output = new GenericOutput<string>();
            var errors = new List<string> { "Erro 1", "Erro 2" };

            //Action
            output.AddErrors(errors);

            //Assert
            output.Ok.Should().BeFalse();
            output.Errors.Should().HaveCount(2);
            output.Errors.Should().Contain("Erro 1");
            output.Errors.Should().Contain("Erro 2");
        }
    }
}