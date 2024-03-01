using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using MudBlazor;
using MudBlazor.Services;
using Planner.Abstractions;
using Planner.Auxiliary;
using Planner.DataAccessLayer;
using Planner.DataAccessLayer.DAO;
using Planner.Managers;
using Planner.Models;
using Planner.Services;

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

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            //Add services
            builder.Services.AddMauiBlazorWebView()
                   .Services.AddMudServices()
                   .AddSingleton(new DbConnectionOptions { ConnectionString = Path.Combine(FileSystem.AppDataDirectory, "planner.db") })
                   .AddSingleton<IDialogService, DialogService>()
                   .AddSingleton<IDataProvider<CompanyDAO>, CompanyLiteDbProvider>()

                   .AddSingleton<IDataManager<CompanyModel>, CompanyManager>()
                   .AddSingleton<ICustomDialogService, CustomDialogService>();


            var app = builder.Build();

            // a workaround to initialize the LiteDb providers before pages
            _ = app.Services.GetRequiredService<IDataProvider<CompanyDAO>>();

            return app;
        }
    }
}
