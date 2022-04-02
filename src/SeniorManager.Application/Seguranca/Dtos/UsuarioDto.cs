using SeniorManager.Domain.Seguranca.Enums;
using System;

namespace SeniorManager.Application.Seguranca.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public virtual int CodCliente { get; set; }
    }
}