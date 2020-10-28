using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Servico.Produto
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public static IWebHostEnvironment Environment { get; private set; }

        string version = "Versão 1.0";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors();

            services.Configure<AppSettings>(appSettings =>
            {
                appSettings.ConnectionString = Configuration[$"{Environment.EnvironmentName}:ConnectionString"];
            });

            // Register the Swagger generator, defining 1 or more Swagger documents if on developer mode
            if (Environment.EnvironmentName == "Development")
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("V1", new OpenApiInfo
                    {
                        Title = "Servico.Produto",
                        Version = version,
                        Description = "Servico.Produto"
                    });
                });
            }

            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddSingleton(Environment);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            if (env.EnvironmentName == "Development")
            {
                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                    c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/V1/swagger.json", $"Servico.Produto {version}");

                });
            }

            app.UseHttpsRedirection();

            IList<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("pt-BR"),
                new CultureInfo("en-US"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });


            app.UseCors("CorsPolicy");
        }
    }
}