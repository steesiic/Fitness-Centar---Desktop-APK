using FitnessCentar.App.Services.Implementation;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Data;
using FitnessCentar.Presentation;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessCentar
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var services = new ServiceCollection();

            // Data Layer
            services.AddSingleton<IDataLayer, DataLayer>();

            // Servisi - Klaster 1
            services.AddTransient<IKorisnikService, KorisnikService>();

            // Servisi - Klaster 2
            services.AddTransient<IOsobljeService, OsobljeService>();

            // Servisi - Klaster 3
            services.AddTransient<IProstorService, ProstorService>();
            services.AddTransient<IOpremaService, OpremaService>();

            // Forme
            services.AddTransient<MainForm>();

            ServiceProvider = services.BuildServiceProvider();

            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
        }
    }
}
