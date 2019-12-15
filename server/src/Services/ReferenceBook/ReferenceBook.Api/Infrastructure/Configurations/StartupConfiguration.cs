using System;
using System.IO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReferenceBook.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace ReferenceBook.Api.Infrastructure.Configurations
{
    public static class StartupConfiguration
    {
        #region Custom Mvc

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.Configure<Configuration>(configuration);
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                                  builder => builder
                                            .SetIsOriginAllowed(host => true)
                                            .AllowAnyMethod()
                                            .AllowAnyHeader()
                                            .AllowCredentials());
            });
            return services;
        }

        #endregion

        #region Custom Db Context

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            var config = configuration.Get<Configuration>();

            services
                .AddEntityFrameworkNpgsql()
                .AddDbContext<ReferenceBookContext>(options => options.UseNpgsql(
                    config.Db.ToConnectionString(),
                    sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), new string[] { });
                    }
                ));

            return services;
        }

        #endregion

        #region Custom Swagger

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "TL - ReferenceBook API",
                    Contact = new Contact
                    {
                        Email = "info@dka.com",
                        Name = "DKA",
                        Url = "www.dka.com"
                    },
                    Version = "v1",
                    Description = "The Reference Book API",
                    TermsOfService = "Terms Of Service"
                });
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
            });
            return services;
        }

        #endregion
    }
}