namespace FitnessCentar.Entities
{
    public class PlanTreninga
    {
        public virtual int PlanId { get; protected set; }

        public virtual required Korisnik Korisnik { get; set; }

        public virtual required Osoblje Osoblje { get; set; }

        public virtual required string Naziv { get; set; }

        public virtual required DateTime DatumKreiranja { get; set; }

        public virtual IList<FazaPlana> Faze { get; set; }

        public PlanTreninga()
        {
            Faze = new List<FazaPlana>();
        }
    }
}
