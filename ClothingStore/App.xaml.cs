using System;
using System.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ClothingStore.Data;
using ClothingStore.Services;
using ClothingStore.Views;

namespace ClothingStore;

public partial class App : Application
{
    public static IHost AppHost { get; private set; }

    public App()
    {
        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((ctx, cfg) =>
            {
                cfg.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((ctx, services) =>
            {
                var conn = ctx.Configuration.GetConnectionString("DefaultConnection");
                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));

                // Services
                services.AddTransient<AuthService>();
                services.AddTransient<ProductService>();

                // Windows (register as transient or singleton depending on desired lifetime)
                services.AddSingleton<LoginWindow>();
                services.AddSingleton<RegisterWindow>();
                services.AddSingleton<MainWindow>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost.StartAsync();
        var login = AppHost.Services.GetRequiredService<LoginWindow>();
        login.Show();
        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost.StopAsync();
        base.OnExit(e);
    }
}
