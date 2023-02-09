var builder = WebApplication.CreateBuilder(args);
var logger = AppConfiguration.SetLogger(builder.Configuration,builder.Logging);

AppConfiguration.SetDbContext(builder.Configuration,builder.Services);
AppConfiguration.SetServices(builder.Services);

var app = builder.Build();

AppConfiguration.SetMiddleware(app);

logger.Information("Application started");

app.Run();