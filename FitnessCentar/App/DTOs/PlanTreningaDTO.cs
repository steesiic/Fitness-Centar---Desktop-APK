namespace FitnessCentar.App.DTOs
{
    public class PlanTreningaDTO
    {
        public int PlanId { get; set; }

        public int KorisnikId { get; set; }

        public int OsobljeId { get; set; }

        public string Naziv { get; set; } = "";

        public DateTime DatumKreiranja { get; set; }

        public int BrojFaza { get; set; }
    }
}
