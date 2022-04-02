using Microsoft.Extensions.DependencyInjection;

using SeniorManager.Domain.Comum.Repositories;
using SeniorManager.Repository.Comum;
using AutoMapper;
using System.Diagnostics.CodeAnalysis;
using Cliente = SeniorManager.Application.Comum.UseCases.Cliente;
using SeniorManager.Crosscutting.Interfaces;
using SeniorManager.Crosscutting.Helpers;
using SeniorManager.Crosscutting.Seguranca;

namespace SeniorManager.Configuration.Comum
{
    [ExcludeFromCodeCoverage]
    public static class Dependencias
    {
        public static IServiceCollection Register(this IServiceCollection services)
        {
            #region Repositorios 
            services.AddTransient<IClienteRepository, ClienteRepository>();
            #endregion

            #region UseCases
            services.AddTransient<Cliente.Listar.IUseCase, Cliente.Listar.UseCase>();
            services.AddTransient<Cliente.Salvar.IUseCase, Cliente.Salvar.UseCase>();
            services.AddTransient<Cliente.BuscarPorId.IUseCase, Cliente.BuscarPorId.UseCase>();

            #endregion

            #region ServicesInfra
            services.AddTransient<SigningConfigurations, SigningConfigurations>();
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddTransient<IUrlBuilder, UrlBuilder>();
            #endregion

            #region AutoMapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapeamentos());
                mc.AddProfile(new Seguranca.Mapeamentos());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            return services;
        }
    }
}