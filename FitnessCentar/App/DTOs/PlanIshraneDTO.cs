namespace FitnessCentar.App.DTOs
{
    public class PlanIshraneDTO
    {
        public int PlanIshraneId { get; set; }

        public int KorisnikId { get; set; }

        public int NutricionistaId { get; set; }

        public string Naziv { get; set; } = "";

        public DateTime DatumKreiranja { get; set; }

        public int BrojObroka { get; set; }

        public int BrojOgranicenja { get; set; }
    }
}
