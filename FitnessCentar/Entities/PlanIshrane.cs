namespace FitnessCentar.Entities
{
    public class PlanIshrane
    {
        public virtual int PlanIshraneId { get; protected set; }

        public virtual required Korisnik Korisnik { get; set; }
        public virtual required Nutricionista Nutricionista { get; set; }

        public virtual required string Naziv { get; set; }

        public virtual required DateTime DatumKreiranja { get; set; }

        public virtual IList<Obrok> Obroci { get; set; }

        public virtual IList<string> Ogranicenja { get; set; }

        public PlanIshrane()
        {
            Obroci = new List<Obrok>();
            Ogranicenja = new List<string>();
        }
    }
}
