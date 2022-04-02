using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using SeniorManager.Domain.Comum.Entities;
using SeniorManager.Domain.Seguranca.Entities;

namespace SeniorManager.Repository.Contexts
{
    [ExcludeFromCodeCoverage]
    public class SeniorManagerDbContext : DbContext
    {
        public SeniorManagerDbContext(DbContextOptions<SeniorManagerDbContext> options) : base(options)
        {
            if (Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
                Database.Migrate();
        }

        #region Comum
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<PessoaJuridica> PessoaJuridicas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Filial> Filiais { get; set; }
        #endregion

        #region Seguranca
        public DbSet<Usuario> Usuarios { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SeniorManagerDbContext).Assembly);
        }
    }
}