using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace TaxLegal.Cbc.Report.Api.Infrastructure.Configurations
{
    public static class StartupConfiguration
    {
        #region Custom Mvc

        public static IServiceCollection AddCustomMvc(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            services.Configure<Configuration>(configuration);
            services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.WriteIndented = true);

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
                    Description = "Type into the text box: Bearer {your JWT token}."
                });
                doc.OperationProcessors
                   .Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
            return services;
        }

        #endregion
    }
}