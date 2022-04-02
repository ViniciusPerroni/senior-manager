using Microsoft.IdentityModel.Tokens;
using SeniorManager.Crosscutting.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SeniorManager.Crosscutting.Seguranca
{
    public class JwtTokenService : IJwtTokenService
    {
        public const string ISSUER = "SeniorManager";
        private const string SECRET_KEY = "9c7db6ea-2a05-48ac-99b7-b805f05138f0";
        public const string CLAIM_NAME_USERNAME = "jwt-username";
        public const string CLAIM_NAME_USER_ID = "jwt-user-id";
        public const string CLAIM_NAME_TYPE = "9c7db6ea";

        public static byte[] Key => Encoding.UTF8.GetBytes(SECRET_KEY);

        private readonly ISigningConfigurations criptografiaService;
        public JwtTokenService(ISigningConfigurations criptografiaService)
        {
            this.criptografiaService = criptografiaService;
        }

        public string GerarToken(string usuarioId, string username, string tipoUsuario)
        {

            var claims = new Claim[]
                {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, tipoUsuario.ToString()),
                        new Claim(CLAIM_NAME_USERNAME, criptografiaService.GerarHash(username)),
                        new Claim(CLAIM_NAME_USER_ID, criptografiaService.GerarHash(usuarioId)),
                };

            var identity = new ClaimsIdentity(claims);

            var dataInicio = DateTime.Now;
            var dataExpiracao = DateTime.Now.AddDays(1);

            var handler = new JwtSecurityTokenHandler();

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                NotBefore = dataInicio,
                Expires = dataExpiracao,
                Issuer = ISSUER,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature),
                Subject = identity,

            });

            string token = handler.WriteToken(securityToken);

            return token;
        }

        public string CarregarDado(string claimName, string token)
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
                var data = jwtToken.Claims.First(x => x.Type == claimName).Value;

                return criptografiaService.ConverterHash(data);
            }
            catch
            {
                return null;
            }
        }
    }
}
