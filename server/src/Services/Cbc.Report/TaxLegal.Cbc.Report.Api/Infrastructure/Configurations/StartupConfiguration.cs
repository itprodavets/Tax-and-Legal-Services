using System;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
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

            var cfg = configuration.Get<Configuration>();

            services.AddControllers().AddJsonOptions(options =>
            {
                // options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                options.JsonSerializerOptions.WriteIndented = true;
                // options.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .SetIsOriginAllowed(host => true)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .WithExposedHeaders("Content-Disposition"));
            });

            var storagePath = Path.GetFullPath(cfg.StoredFilesPath);
            Directory.CreateDirectory(storagePath);
            var physicalProvider = new PhysicalFileProvider(storagePath);
            services.AddSingleton<IFileProvider>(physicalProvider);

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