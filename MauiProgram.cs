using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using UseItUp.Services;
using UseItUp.Views;

namespace INFT_2051__USEITUP_
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // 1. 注册核心服务：将其设为单例（Singleton），保证整个应用只共用这一个数据库连接
            builder.Services.AddSingleton<DatabaseService>();

            // 2. 注册 UI 页面：将其设为瞬态（Transient），每次访问都会生成一个干净的新页面
            builder.Services.AddTransient<IngredientListPage>();
            builder.Services.AddTransient<AddIngredientPage>();

            return builder.Build();
        }
    }
}