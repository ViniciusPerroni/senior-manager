using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using FluentAssertions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SeniorManager.Crosscutting.Interfaces;
using SeniorManager.Crosscutting.Seguranca;

namespace SeniorManager.Test.Services
{
    [TestClass]
    public class JwtTokenServiceTest
    {
        private const string SECRET_KEY = "9c7db6ea-2a05-48ac-99b7-b805f05138f0";
        public const string CLAIM_NAME_USER_ID = "jwt-user-id";

        [TestMethod]
        public void GerarTokenJwtValido()
        {
            //Arrange
            var criptografiaService = Mock.Of<Crosscutting.Interfaces.ISigningConfigurations>();
            Mock.Get(criptografiaService).Setup(x => x.GerarHash("1234")).Returns("1234");
            Mock.Get(criptografiaService).Setup(x => x.GerarHash("username")).Returns("username");
            Mock.Get(criptografiaService).Setup(x => x.GerarHash("admin")).Returns("admin");

            var jwtTokenService = new JwtTokenService(criptografiaService);

            //Action
            var token = jwtTokenService.GerarToken("1234", "username", "admin");

            //Assert
            var tokenValido = ValidarJwtToken(token);
            token.Should().NotBeNullOrEmpty();
            tokenValido.Should().Be(1234);
        }

        private int? ValidarJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SECRET_KEY);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == CLAIM_NAME_USER_ID).Value);

                return accountId;
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}