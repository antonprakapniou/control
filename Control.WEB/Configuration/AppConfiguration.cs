using Control.BLL.Interfaces;
using Control.BLL.Services;
using Control.BLL.Utilities;
using Control.DAL.Configuration;
using Control.DAL.EF;
using Control.DAL.Interfaces;
using Control.DAL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Control.WEB.Configuration
{
	public static class AppConfiguration
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

            services.AddAutoMapper(typeof(MapPropfile));
            services.AddTransient<IUnitOfWork,UnitOfWork>();
			services.AddTransient<IMeasuringService, MeasuringService>();
			services.AddTransient<INominationService, NominationService>();
			services.AddTransient<IOperationService, OperationService>();
			services.AddTransient<IOwnerService, OwnerService>();
			services.AddTransient<IPeriodService, PeriodService>();
			services.AddTransient<IPositionService, PositionService>();
		}
	}
}
