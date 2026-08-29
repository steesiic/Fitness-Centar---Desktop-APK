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

        public bool JeIndividualni { get; set; }

        public int? KorisnikId { get; set; }

        public string? CiljeviTreninga { get; set; }

        public bool JeGrupni { get; set; }

        public int? MaksUcesnika { get; set; }

        public bool JeTradicionalni { get; set; }

        public int? ProstorId { get; set; }  

        public bool JeOnline { get; set; }

        public string? Platforma { get; set; }

        public string? KorisnikImePrezime { get; set; }

        public string? ProstorNaziv { get; set; }

        public int BrojUcesnika { get; set; }

        public int BrojEvidencija { get; set; }
    }
}
