using CarSharingHamburg.Models;
using CarSharingHamburg.Services;
using CarSharingHamburg.ViewModels;
using CarSharingHamburg.Views;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace CarSharingHamburg;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterPages()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .UseMauiMaps();



#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<MainPageViewModel>();
        mauiAppBuilder.Services.AddTransient<MainPageWindowsViewModel>();

        mauiAppBuilder.Services.AddTransient<KundenViewModel>();
        mauiAppBuilder.Services.AddTransient<KundenDetailsViewModel>();
        mauiAppBuilder.Services.AddTransient<NewKundeViewModel>();

        mauiAppBuilder.Services.AddTransient<AutoViewModel>();
        mauiAppBuilder.Services.AddTransient<AutoDetailsViewModel>();
        mauiAppBuilder.Services.AddTransient<NewAutoViewModel>();
        return mauiAppBuilder;
    }
    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IDataStore<Kunde>, DbKundenStore>();
        mauiAppBuilder.Services.AddSingleton<IDataStore<Auto>,DbAutoStore>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterPages(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddTransient<MainPage>();
        mauiAppBuilder.Services.AddTransient<MainPageWindows>();

        mauiAppBuilder.Services.AddTransient<KundenPage>();
        mauiAppBuilder.Services.AddTransient<KundenDetailsPage>();
        mauiAppBuilder.Services.AddTransient<NewKundePage>();

        mauiAppBuilder.Services.AddTransient<AutoPage>();
        mauiAppBuilder.Services.AddTransient<AutoDetailsPage>();
        mauiAppBuilder.Services.AddTransient<NewAutoPage>();
        return mauiAppBuilder;
    }
}
