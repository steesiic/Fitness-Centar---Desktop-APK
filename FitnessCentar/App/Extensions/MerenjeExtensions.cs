using FitnessCentar.App.DTOs;
using FitnessCentar.Entities;

namespace FitnessCentar.App.Extensions
{
    public static class MerenjeExtensions
    {
        public static MerenjeDTO ToMerenjeDTO(this Merenje merenje)
        {
            return new MerenjeDTO
            {
                MerenjeId = merenje.MerenjeId,
                KorisnikId = merenje.Korisnik.KorisnikId,
                OsobljeId = merenje.Osoblje.OsobljeId,
                Datum = merenje.Datum,
                TipMerenja = merenje.TipMerenja,
                NumerickaVrednost = merenje.NumerickaVrednost,
                JedinicaMere = merenje.JedinicaMere,
                TekstualniRezultat = merenje.TekstualniRezultat,
                Napomena = merenje.Napomena
            };
        }
    }
}
