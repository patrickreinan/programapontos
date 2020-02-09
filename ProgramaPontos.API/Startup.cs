using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProgramaPontos.Infra.Ioc.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.Swagger;

namespace ProgramaPontos.API
{
    public class Startup
    {

        private readonly string version = "1.0";
        private readonly string applicationName = "ProgramaPontos";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(s=>s.EnableEndpointRouting=false);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ProgramaPontos",Version= version });
            });

            

            services.AddProgramaPontosServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{applicationName} {version}");
            });
        }
    }
}
