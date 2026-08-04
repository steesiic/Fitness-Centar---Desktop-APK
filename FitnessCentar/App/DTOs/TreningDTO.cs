using FitnessCentar.Entities;

namespace FitnessCentar.App.DTOs
{
    public class TreningDTO
    {
        public int TreningId { get; set; }

        public string Naziv { get; set; } = "";

        public string? Tip { get; set; }

        public DateTime DatumVreme { get; set; }

        public StatusTreninga Status { get; set; }

        public int? ProgramId { get; set; }

        // Specijalizacija #1 - tačno jedno od dva mora biti true
        public bool JeIndividualni { get; set; }

        public int? KorisnikId { get; set; }  // obavezno ako JeIndividualni

        public string? CiljeviTreninga { get; set; }

        public bool JeGrupni { get; set; }

        public int? MaksUcesnika { get; set; }

        // Specijalizacija #2 (nezavisna od #1) - tačno jedno od dva mora biti true
        public bool JeTradicionalni { get; set; }

        public int? ProstorId { get; set; }  // obavezno ako JeTradicionalni

        public bool JeOnline { get; set; }

        public string? Platforma { get; set; }

        // Za prikaz
        public string? KorisnikImePrezime { get; set; }

        public string? ProstorNaziv { get; set; }

        public int BrojUcesnika { get; set; }

        public int BrojEvidencija { get; set; }
    }
}
