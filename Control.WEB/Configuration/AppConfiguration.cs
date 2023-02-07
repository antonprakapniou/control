using ILogger = Serilog.ILogger;

namespace Control.WEB.Configuration;

public static class AppConfiguration
{
    public static ILogger SetLogger(IConfiguration configuration,ILoggingBuilder logging)
    {
        #region Logging configuration

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        logging.ClearProviders();
        logging.AddSerilog(logger);

        #endregion

        return logger;
    }
    public static void SetDbContext(IConfiguration configuration, IServiceCollection services)
    {
        #region Db connection

        string connectionName = ConnectionConst.DevelopSqLiteConnection;
        string connectionString = configuration
            .GetConnectionString(connectionName!)
            ??throw new InvalidOperationException($"Connection \"{connectionName}\" not found");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(connectionString);
            options.EnableSensitiveDataLogging();
        });

        #endregion
    }
    public static void SetServices(IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllersWithViews();

        #region Utilities

        services.AddTransient<ExceptionHandlingMiddleware>();
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
    public static void SetMiddleware(WebApplication app)
    {
        #region Middleware

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        #endregion


    }
}