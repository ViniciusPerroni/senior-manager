using Microsoft.Extensions.DependencyInjection;
using SeniorManager.Domain.Seguranca.Repositories;
using SeniorManager.Repository.Seguranca;

using System.Diagnostics.CodeAnalysis;

using Usuario = SeniorManager.Application.Seguranca.UseCases.Usuario;

namespace SeniorManager.Configuration.Seguranca
{
    [ExcludeFromCodeCoverage]
    public static class Dependencias
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            #region Repositorios 
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            #endregion

            #region UseCases
            services.AddTransient<Usuario.BuscarPorUsername.IUseCase, Usuario.BuscarPorUsername.UseCase>();
            services.AddTransient<Usuario.Autenticar.IUseCase, Usuario.Autenticar.UseCase>();
            #endregion

            return services;
        }
    }
}