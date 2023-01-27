using Control.BLL.Interfaces;
using Control.BLL.Services;
using Control.BLL.Utilities;
using Control.DAL.Configuration;
using Control.DAL.EF;
using Control.DAL.Interfaces;
using Control.DAL.Repositories;
using Control.WEB.Interfaces;
using Control.WEB.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Control.WEB.Configuration
{
	public static class AppConfiguration
	{
		public static void Set(IConfiguration configuration, IServiceCollection services)
		{
			services.AddControllersWithViews();

            string? connectionName = AppConstants.DevelopSqLiteConnection;
            string connectionString =configuration
				.GetConnectionString(connectionName!)

				??throw new InvalidOperationException($"Connection \"{connectionName}\" not found");

			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseSqlite(connectionString);
				options.EnableSensitiveDataLogging();
			});

            services.AddAutoMapper(typeof(MapPropfile));
			services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddTransient<IFileManager, FileManager>();
            services.AddScoped<ICategoryService,CategoryService>();
            services.AddScoped<IMeasuringService, MeasuringService>();
            services.AddScoped<INominationService, NominationService>();
            services.AddScoped<IOperationService, OperationService>();
            services.AddScoped<IOwnerService, OwnerService>();
            services.AddScoped<IPeriodService, PeriodService>();
            services.AddScoped<IPositionService, PositionService>();
		}
	}
}
