namespace FitnessCentar.App.DTOs
{
    public class EvaluacijaDTO
    {
        public int EvaluacijaId { get; set; }

        public int KorisnikId { get; set; }

        public int OsobljeId { get; set; }

        public DateTime Datum { get; set; }

        public string Opis { get; set; } = "";

        public string? Preporuka { get; set; }
    }
}
