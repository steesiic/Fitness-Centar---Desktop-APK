namespace FitnessCentar.Entities
{
    public class Odrzavanje
    {
        public virtual int OdrzavanjeId { get; protected set; }

        public virtual required DateTime Datum { get; set; }

        public virtual string? Serviser { get; set; }

        public virtual string? OpisIntervencije { get; set; }

        public virtual string? StatusNakonServisa { get; set; }

        public virtual required Oprema Oprema { get; set; }
    }
}
