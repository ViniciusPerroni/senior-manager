using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SeniorManager.Configuration.Comum;
using SeniorManager.Repository.Contexts;
using Swashbuckle.AspNetCore.Filters;

using SeniorManager.Domain.Seguranca.Entities;
using SeniorManager.Repository.Contexts;
using Dependencias = SeniorManager.Configuration;
using SeniorManager.Crosscutting.Seguranca;
using SeniorManager.Crosscutting.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SeniorManager.WebAPI.IoC
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

            return services;
        }

        public static IServiceCollection AddSwaggerDependency(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.ExampleFilters();
                options.SwaggerDoc(name: "v1", new OpenApiInfo { Title = "SeniorManager.WebAPI", Version = "v1" });
                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "Autorização JWT",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"

                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new System.Collections.Generic.List<string>()
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            services.ConfigureSwaggerGen(options => options.CustomSchemaIds(x => x.FullName));

            return services;
        }

        public static IServiceCollection AddSolutionDependency(this IServiceCollection services)
        {
            Dependencias.Comum.Dependencias.Register(services);
            Dependencias.Seguranca.Dependencias.Register(services);

            return services;
        }
    }
}
