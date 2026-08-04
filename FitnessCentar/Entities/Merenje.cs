namespace FitnessCentar.Entities
{
    public class Merenje
    {
        public virtual int MerenjeId { get; protected set; }

        public virtual required Korisnik Korisnik { get; set; }

        public virtual required Osoblje Osoblje { get; set; }

        public virtual required DateTime Datum { get; set; }

        public virtual string? TipMerenja { get; set; }

        public virtual decimal? NumerickaVrednost { get; set; }

        public virtual string? JedinicaMere { get; set; }

        public virtual string? TekstualniRezultat { get; set; }

        public virtual string? Napomena { get; set; }
    }
}
