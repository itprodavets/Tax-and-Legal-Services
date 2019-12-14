using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ReferenceBook.Api.Infrastructure.Configurations
{
	public static class StartupConfiguration
	{
		#region Custom Mvc

		/// <summary>
		///  Add Mvc
		/// </summary>
		/// <param name="services"></param>
		/// <param name="configuration"></param>
		/// <returns></returns>
		public static IServiceCollection AddMvc(this IServiceCollection services, IConfiguration configuration)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			services.Configure<Configuration>(configuration);
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest).AddControllersAsServices();

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

		#region DbContext

		public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
		{
			if (configuration == null) throw new ArgumentNullException(nameof(configuration));

			// var config = configuration.Get<Configuration>();
			return services;
		}

		#endregion
	}
}