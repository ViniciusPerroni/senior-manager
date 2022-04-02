using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SeniorManager.Repository.Contexts;
using Dependencias = SeniorManager.Configuration;
using SeniorManager.Crosscutting.Seguranca;
using SeniorManager.Crosscutting.Interfaces;
using SeniorManager.Crosscutting.Settings;
using SeniorManager.Crosscutting.Settings.Impl;

namespace SeniorManager.WebMvc.IoC
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjectionExtensions
    {
        public const string ConnectionString = "DbSeniorManager";
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkNpgsql()
              .AddDbContext<SeniorManagerDbContext>(options =>
              {
                  options.UseLazyLoadingProxies();
                  options.UseNpgsql(
                       configuration.GetConnectionString(ConnectionString),
                       opts =>
                       {
                           opts.MigrationsAssembly(typeof(SeniorManagerDbContext).Assembly.GetName().Name);
                           opts.SetPostgresVersion(new Version(9, 6));
                       });
              });


            services.AddTransient<ISigningConfigurations, SigningConfigurations>();
            services.AddTransient<SigningConfigurations>();
            services.AddSingleton<IReaderSettingsWebMvc>(new ReaderSettingsWebMvc(configuration));

            Dependencias.Comum.Dependencias.Register(services);
            Dependencias.Seguranca.Dependencias.Register(services);

            return services;
        }
    }
}