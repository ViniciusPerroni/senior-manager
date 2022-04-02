using System;
using SeniorManager.Domain.Comum.Entities;
using SeniorManager.Domain.Seguranca.Enums;

namespace SeniorManager.Domain.Seguranca.Entities
{
    public class Usuario : BaseEntity
    {
        #region Construtor
        public Usuario() { }
        public Usuario(string username, string senha, string nome, string sobrenome, DateTime dataNascimento, Cliente cliente, bool ativo)
        {
            Username = username;
            Senha = senha;
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
            Cliente = cliente;
            CodCliente = cliente.Id;
            Ativo = ativo;
        }
        #endregion

        #region Propriedades
        public string Username { get; set; }
        public string Senha { get; private set; }
        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public TipoUsuario TipoUsuario { get; set; }
        public bool Ativo { get; private set; }
        #endregion

        #region Agregados
        public virtual int CodCliente { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        #endregion
    }
}