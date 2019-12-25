using System.Text;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaxLegal.Cbc.Report.Api.Infrastructure.AutofacModules;
using TaxLegal.Cbc.Report.Api.Infrastructure.Configurations;

namespace TaxLegal.Cbc.Report.Api
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }

        public Startup(IHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                           .SetBasePath(env.ContentRootPath)
                           .AddJsonFile("appsettings.json", true, true)
                           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                           .AddEnvironmentVariables().Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            services.AddCustomMvc(Configuration)
                    .AddCustomSwagger();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var cfg = Configuration.Get<Configuration>();
            var connectionString = cfg.Db.ToConnectionString();
            builder.RegisterModule(new ApplicationModule(connectionString));
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