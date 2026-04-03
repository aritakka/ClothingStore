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
    public static IHost AppHost { get; private set; } = null!;

    public App()
    {
        InitializeComponent();

        AppHost = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((ctx, cfg) =>
            {
                cfg.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((ctx, services) =>
            {
                var conn = ctx.Configuration.GetConnectionString("DefaultConnection")
                           ?? "Server=(localdb)\\mssqllocaldb;Database=ClothingStoreDb;Trusted_Connection=True;";

                services.AddDbContext<AppDbContext>(options => options.UseSqlServer(conn));

                services.AddScoped<AuthService>();
                services.AddScoped<ProductService>();

                services.AddSingleton<UserState>(); // <-- добавлено

                services.AddTransient<LoginWindow>();
                services.AddTransient<RegisterWindow>();
                services.AddTransient<MainWindow>();
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
        AppHost.Dispose(); // освобождаем ресурсы
        base.OnExit(e);
    }
}
