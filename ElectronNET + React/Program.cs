using ElectronNET.API;
using ElectronNET.API.Entities;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
        builder.Services.AddElectron();
        builder.WebHost.UseElectron(args);

        if (HybridSupport.IsElectronActive)
            CreateElectronWindow();

        var app = builder.Build();

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller}/{action=Index}/{id?}"
        );

        app.MapFallbackToFile("index.html");
        app.Run();
    }

    private static async void CreateElectronWindow()
    {
        BrowserWindow window = await Electron.WindowManager.CreateWindowAsync(
            new BrowserWindowOptions
            {
                Width = 1280,
                Height = 720
            });

        window.OnClosed += () => Electron.App.Quit();
    }
}
