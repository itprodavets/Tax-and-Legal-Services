using Microsoft.AspNetCore.Builder;
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
        public Startup(IHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                           .SetBasePath(env.ContentRootPath)
                           .AddJsonFile("appsettings.json", false, true)
                           .AddEnvironmentVariables().Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc(Configuration)
                    .AddCustomDbContext(Configuration)
                    .AddCustomSwagger();

            var cfg = Configuration.Get<Configuration>();
            var connectionString = cfg.Db.ToConnectionString();

            services.AddScoped<ICountryQueries, CountryQueries>(_ => new CountryQueries(connectionString));
            services.AddScoped<ICurrencyQueries, CurrencyQueries>(_ => new CurrencyQueries(connectionString));
            services.AddScoped<ILanguageQueries, LanguageQueries>(_ => new LanguageQueries(connectionString));
        }

        public void Configure(
            IApplicationBuilder app,
            IHostEnvironment env
        )
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}