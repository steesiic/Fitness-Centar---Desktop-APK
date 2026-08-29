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

            services.AddSingleton<IDataLayer, DataLayer>();

            services.AddTransient<IKorisnikService, KorisnikService>();

            services.AddTransient<IOsobljeService, OsobljeService>();

            services.AddTransient<IProstorService, ProstorService>();
            services.AddTransient<IOpremaService, OpremaService>();

            services.AddTransient<IClanarinaService, ClanarinaService>();
            services.AddTransient<IFitnessProgramService, FitnessProgramService>();

            services.AddTransient<ITreningService, TreningService>();
            services.AddTransient<IPlanTreningaService, PlanTreningaService>();

            services.AddTransient<IPlanIshraneService, PlanIshraneService>();

            services.AddTransient<IMerenjeEvaluacijaService, MerenjeEvaluacijaService>();

            services.AddTransient<MainForm>();

            ServiceProvider = services.BuildServiceProvider();

            Application.Run(ServiceProvider.GetRequiredService<MainForm>());
        }
    }
}
