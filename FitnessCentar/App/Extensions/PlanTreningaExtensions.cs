using FitnessCentar.App.DTOs;
using FitnessCentar.Entities;

namespace FitnessCentar.App.Extensions
{
    public static class PlanTreningaExtensions
    {
        public static PlanTreningaDTO ToPlanTreningaDTO(this PlanTreninga plan)
        {
            return new PlanTreningaDTO
            {
                PlanId = plan.PlanId,
                KorisnikId = plan.Korisnik.KorisnikId,
                OsobljeId = plan.Osoblje.OsobljeId,
                Naziv = plan.Naziv,
                DatumKreiranja = plan.DatumKreiranja,
                BrojFaza = plan.Faze.Count
            };
        }
    }
}
