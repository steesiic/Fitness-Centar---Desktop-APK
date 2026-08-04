namespace FitnessCentar.App.DTOs
{
    public class MerenjeDTO
    {
        public int MerenjeId { get; set; }

        public int KorisnikId { get; set; }

        public int OsobljeId { get; set; }

        public DateTime Datum { get; set; }

        public string? TipMerenja { get; set; }

        public decimal? NumerickaVrednost { get; set; }

        public string? JedinicaMere { get; set; }

        public string? TekstualniRezultat { get; set; }

        public string? Napomena { get; set; }
    }
}
