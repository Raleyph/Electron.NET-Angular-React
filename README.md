[![NET Logo](https://www.vectorlogo.zone/logos/dotnet/dotnet-icon.svg)](https://learn.microsoft.com/dotnet)
[![Electron Logo](https://www.vectorlogo.zone/logos/electronjs/electronjs-icon.svg)](https://www.electronjs.org)
[![Angular Logo](https://www.vectorlogo.zone/logos/angular/angular-icon.svg)](https://angular.io)
[![React Logo](https://www.vectorlogo.zone/logos/reactjs/reactjs-icon.svg)](https://react.dev)

Templates desktop [Electron.NET](https://github.com/ElectronNET/Electron.NET) applications using [Angular](https://angular.io) and [React](https://react.dev) (ASP.NET Core 6).

## ⚙️ How to install Electron.NET and include Angular or React yourself

The Electron installation process contains some subtleties, if observed, the installation of the library and its further
use will be successful. You just need to strictly follow the procedure described below.

1. Create **ASP.NET Core with Angular/React** template project (naturally, you can create a regular ASP.NET Core project and connect Angular/React there yourself).
2. Install [ElectronNET.API](https://www.nuget.org/packages/ElectronNET.API) from NuGet.
3. Run the project by pressing F5 (very important).
4. Paste this code to **Program.cs**:
    ```csharp
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
    ```
5. Enter the following command into the terminal:
    ```
    electronize init
    ```

6. Installation of Electron.NET and connection of Angular/React is completed. You can customize the **electron.manifest.json** file by entering
   the name of the application, its version and information about the developer. Also you can change window options (size, minimal and maximal size etc).

7. To run the application, enter the following command:
    ```
    electronize start
    ```
