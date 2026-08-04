using FitnessCentar.App.DTOs;
using FitnessCentar.Entities;

namespace FitnessCentar.App.Extensions
{
    public static class TreningExtensions
    {
        public static TreningDTO ToTreningDTO(this Trening trening)
        {
            return new TreningDTO
            {
                TreningId = trening.TreningId,
                Naziv = trening.Naziv,
                Tip = trening.Tip,
                DatumVreme = trening.DatumVreme,
                Status = trening.Status,
                ProgramId = trening.Program?.ProgramId,

                JeIndividualni = trening.IndividualniTrening != null,
                KorisnikId = trening.IndividualniTrening?.Korisnik.KorisnikId,
                CiljeviTreninga = trening.IndividualniTrening?.CiljeviTreninga,
                KorisnikImePrezime = trening.IndividualniTrening != null
                    ? $"{trening.IndividualniTrening.Korisnik.Ime} {trening.IndividualniTrening.Korisnik.Prezime}"
                    : null,

                JeGrupni = trening.GrupniTrening != null,
                MaksUcesnika = trening.GrupniTrening?.MaksUcesnika,
                BrojUcesnika = trening.GrupniTrening?.Ucesnici.Count ?? 0,

                JeTradicionalni = trening.TradicionalniTrening != null,
                ProstorId = trening.TradicionalniTrening?.Prostor.ProstorId,
                ProstorNaziv = trening.TradicionalniTrening?.Prostor.Naziv,

                JeOnline = trening.OnlineTreningSesija != null,
                Platforma = trening.OnlineTreningSesija?.Platforma,

                BrojEvidencija = trening.Evidencije.Count
            };
        }
    }
}
