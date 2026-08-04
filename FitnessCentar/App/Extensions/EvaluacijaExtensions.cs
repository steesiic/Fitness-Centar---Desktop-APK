using FitnessCentar.App.DTOs;
using FitnessCentar.Entities;

namespace FitnessCentar.App.Extensions
{
    public static class EvaluacijaExtensions
    {
        public static EvaluacijaDTO ToEvaluacijaDTO(this Evaluacija evaluacija)
        {
            return new EvaluacijaDTO
            {
                EvaluacijaId = evaluacija.EvaluacijaId,
                KorisnikId = evaluacija.Korisnik.KorisnikId,
                OsobljeId = evaluacija.Osoblje.OsobljeId,
                Datum = evaluacija.Datum,
                Opis = evaluacija.Opis,
                Preporuka = evaluacija.Preporuka
            };
        }
    }
}
