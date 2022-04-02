using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using SeniorManager.Domain.Seguranca.Entities;
using SeniorManager.Repository.Contexts;
using Dependencias = SeniorManager.Configuration;

using Swashbuckle.AspNetCore.Filters;
using SeniorManager.WebAPI.IoC;
using SeniorManager.Crosscutting.Seguranca;

namespace SeniorManager.WebAPI
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDependencies(Configuration)
                    .AddSwaggerDependency()
                    .AddSolutionDependency();

            services.AddCors();
            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            #region JWT

            var signingConfigurations = new SigningConfigurations();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {

                    IssuerSigningKey = new SymmetricSecurityKey(JwtTokenService.Key),
                    ValidateAudience = false,
                    ValidIssuer = JwtTokenService.ISSUER,
                    ValidateIssuer = true,
                    

                    // Valida a assinatura de um token recebido
                    ValidateIssuerSigningKey = true,
                    // Verifica se um token recebido ainda é válido
                    ValidateLifetime = true,
                    // Tempo de tolerância para a expiração de um token (utilizado
                    // caso haja problemas de sincronismo de horário entre diferentes
                    // computadores envolvidos no processo de comunicação)
                    ClockSkew = System.TimeSpan.Zero,

                
                };
            });

            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(authOptions =>
            {
                authOptions.AddPolicy("Bearer", new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
                /*
                authOptions.AddPolicy(TipoUsuario.SeniorTeam.ToString(), policy => policy.RequireClaim(JwtTokenService.CLAIM_NAME_TYPE, signingConfigurations.GerarHash(TipoUsuario.SeniorTeam.ToString())));
                authOptions.AddPolicy(TipoUsuario.Administrador.ToString(), policy => policy.RequireClaim(JwtTokenService.CLAIM_NAME_TYPE, signingConfigurations.GerarHash(TipoUsuario.Administrador.ToString())));
                authOptions.AddPolicy(TipoUsuario.TecnicoEnfermagem.ToString(), policy => policy.RequireClaim(JwtTokenService.CLAIM_NAME_TYPE, signingConfigurations.GerarHash(TipoUsuario.TecnicoEnfermagem.ToString())));
                authOptions.AddPolicy(TipoUsuario.Enfermeira.ToString(), policy => policy.RequireClaim(JwtTokenService.CLAIM_NAME_TYPE, signingConfigurations.GerarHash(TipoUsuario.Enfermeira.ToString())));
                authOptions.AddPolicy(TipoUsuario.Medico.ToString(), policy => policy.RequireClaim(JwtTokenService.CLAIM_NAME_TYPE, signingConfigurations.GerarHash(TipoUsuario.Medico.ToString())));
                */
            });

            #endregion




        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                    {
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SeniorManager.WebAPI v1");
                        options.DefaultModelsExpandDepth(-1);
                    });
            }

            var supportedCultures = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(culture: "pt-BR", uiCulture: "pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("x-successcount", "x-errorcount", "x-filename", "content-disposition"));


            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}