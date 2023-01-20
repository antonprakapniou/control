using Control.DAL.Configuration;
using Control.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace Control.WEB.Configuration
{
	public sealed class AppConfiguration
	{
		public static void Set(IConfiguration configuration, IServiceCollection services)
		{
			services.AddControllersWithViews();

			string? connectionName = AppConstants.DevelopmentConnection;
			string connectionString=configuration
				.GetConnectionString(connectionName!)
				??throw new InvalidOperationException($"Connection \"{connectionName}\" not found");
			services.AddDbContext<AppDbContext>(options=>
				options.UseNpgsql(connectionString));
		}
	}
}
