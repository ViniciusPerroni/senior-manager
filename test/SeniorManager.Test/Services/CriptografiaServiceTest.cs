using System;
using System.Collections.Generic;
using System.Text;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SeniorManager.Crosscutting.Seguranca;

namespace SeniorManager.Test.Services
{
    [TestClass]
    public class CriptografiaServiceTest
    {
        [TestMethod]
        public void GerarHashOk()
        {
            //Arrange
            var hashEsperado = "gpKLGwh35NE=";
            var stringValue = "teste";

            var service = new SigningConfigurations();

            //Action
            var result = service.GerarHash(stringValue);

            //Assert
            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be(hashEsperado);
        }

        [TestMethod]
        public void VarificarHashReturnTrue()
        {
            //Arrange
            var hashEsperado = "gpKLGwh35NE=";
            var stringValue = "teste";

            var service = new SigningConfigurations();

            //Action
            var result = service.VerificarHash(stringValue, hashEsperado);

            //Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void VarificarHashReturnFalse()
        {
            //Arrange
            var hashEsperado = "gpKLGwh35NE=";
            var stringValue = "test";

            var service = new SigningConfigurations();

            //Action
            var result = service.VerificarHash(stringValue, hashEsperado);

            //Assert
            result.Should().BeFalse();
        }
    }
}