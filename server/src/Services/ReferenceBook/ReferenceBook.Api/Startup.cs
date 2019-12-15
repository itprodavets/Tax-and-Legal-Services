using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReferenceBook.Api.Infrastructure.Configurations;
using ReferenceBook.Application.Queries.Implementations;
using ReferenceBook.Application.Queries.Interfaces;

namespace ReferenceBook.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                           .SetBasePath(env.ContentRootPath)
                           .AddJsonFile("appsettings.json", false, true)
                           .AddEnvironmentVariables().Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Configuration)
                    .AddCustomDbContext(Configuration);
                    // .AddCustomSwagger();

            var cfg = Configuration.Get<Configuration>();
            var connectionString = cfg.Db.ToConnectionString();

            services.AddScoped<ICountryQueries, CountryQueries>(_ => new CountryQueries(connectionString));
            services.AddScoped<ILanguageQueries, LanguageQueries>(_ => new LanguageQueries(connectionString));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();
            else app.UseHsts();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            // app.UseSwagger(o => { o.RouteTemplate = "api/docs/{documentName}/swagger.json"; });
            // app.UseSwaggerUI(o =>
            // {
            //     o.RoutePrefix = "api/docs";
            //     o.SwaggerEndpoint("v1/swagger.json",
            //                       "Reference Book Api v1");
            // });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}