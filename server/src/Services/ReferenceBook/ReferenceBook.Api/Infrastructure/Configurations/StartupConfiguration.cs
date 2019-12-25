using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;
using ReferenceBook.Infrastructure;

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
               .AddDbContext<ReferenceBookContext>(options =>
                                                       options.UseNpgsql(config.Db.ToConnectionString(), sqlOptions =>
                                                       {
                                                           sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                                           sqlOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), new string[] { });
                                                       }));
            return services;
        }

        #endregion

        #region Custom Swagger

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddOpenApiDocument(doc =>
            {
                doc.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}."
                });
                doc.OperationProcessors
                   .Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
            return services;
        }

        #endregion
    }
}