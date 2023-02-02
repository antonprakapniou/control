namespace Control.WEB.Configuration;

public static class AppConfiguration
{
    public static void Set(IConfiguration configuration, IServiceCollection services)
    {
        services.AddControllersWithViews();

        #region Db connection

        string? connectionName = AppConstants.DevelopSqLiteConnection;
        string connectionString = configuration
            .GetConnectionString(connectionName!)

            ??throw new InvalidOperationException($"Connection \"{connectionName}\" not found");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(connectionString);
            options.EnableSensitiveDataLogging();
        });

        #endregion

        #region Utilities

        services.AddAutoMapper(typeof(MapPropfile));
        services.AddTransient<IFileManager, FileManager>();

        #endregion

        #region Services

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IMeasuringService, MeasuringService>();
        services.AddScoped<INominationService, NominationService>();
        services.AddScoped<IOperationService, OperationService>();
        services.AddScoped<IOwnerService, OwnerService>();
        services.AddScoped<IPeriodService, PeriodService>();
        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IMasterService, MasterService>();

        #endregion
    }
}