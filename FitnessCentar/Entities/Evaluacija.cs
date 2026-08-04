namespace FitnessCentar.Entities
{
    public class Evaluacija
    {
        public virtual int EvaluacijaId { get; protected set; }

        public virtual required Korisnik Korisnik { get; set; }

        public virtual required Osoblje Osoblje { get; set; }

        public virtual required DateTime Datum { get; set; }

        public virtual required string Opis { get; set; }

        public virtual string? Preporuka { get; set; }
    }
}
