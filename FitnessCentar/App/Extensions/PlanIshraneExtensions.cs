using FitnessCentar.App.DTOs;
using FitnessCentar.Entities;

namespace FitnessCentar.App.Extensions
{
    public static class PlanIshraneExtensions
    {
        public static PlanIshraneDTO ToPlanIshraneDTO(this PlanIshrane plan)
        {
            return new PlanIshraneDTO
            {
                PlanIshraneId = plan.PlanIshraneId,
                KorisnikId = plan.Korisnik.KorisnikId,
                NutricionistaId = plan.Nutricionista.OsobljeId,
                Naziv = plan.Naziv,
                DatumKreiranja = plan.DatumKreiranja,
                BrojObroka = plan.Obroci.Count,
                BrojOgranicenja = plan.Ogranicenja.Count
            };
        }
    }
}
