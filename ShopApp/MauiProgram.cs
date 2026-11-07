
using Microsoft.Extensions.Logging;
using ShopApp.DataAcces;
using ShopApp.Service;
using ShopApp.Views;
using ShopApp.ViewModels;

namespace ShopApp
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
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Servicio de navegación
            builder.Services.AddSingleton<
                INavegacionService, NavegacionService>();

            // HelpSupport
            builder.Services.AddTransient<HelpSupportViewModel>();
            builder.Services.AddTransient<HelpSupportPage>();

            // HelpSupportDetail
            builder.Services.AddTransient<HelpSupportDetailViewModel>();
            builder.Services.AddTransient<HelpSupportDetailPage>();

            // Base de datos
            using (var dbContext = new ShopDbContext())
            {
                dbContext.Database.EnsureCreated();
            }

            // Registro de rutas
            Routing.RegisterRoute(
                nameof(ProductDetailPage),
                typeof(ProductDetailPage));

            Routing.RegisterRoute(
                nameof(ClientsDetailPage),
                typeof(ClientsDetailPage));

            Routing.RegisterRoute(
                nameof(CategoriesDetailPage),
                typeof(CategoriesDetailPage));

            Routing.RegisterRoute(
                nameof(HelpSupportDetailPage),
                typeof(HelpSupportDetailPage));

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
