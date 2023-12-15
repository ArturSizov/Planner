using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Planner.Abstractions;
using Planner.Auxiliary;
using Planner.DataAccessLayer;
using Planner.DataAccessLayer.DAO;

namespace Planner
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            //Add services
            builder.Services.AddMauiBlazorWebView()
                   .Services.AddMudServices()
                   .AddSingleton(new DbConnectionOptions { ConnectionString = Path.Combine(FileSystem.AppDataDirectory, "planner.db") })
                   .AddSingleton<IDataProvider<CompanyDAO>, CompanySQLiteProvider>()
                   .AddSingleton<IDataProvider<BranchDAO>, BranchSQLiteProvider>();


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            // a workaround to initialize the SQL providers before pages
            _ = app.Services.GetRequiredService<IDataProvider<CompanyDAO>>();
            _ = app.Services.GetRequiredService<IDataProvider<BranchDAO>>();

            return app;
        }
    }
}
