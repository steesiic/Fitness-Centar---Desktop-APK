namespace FitnessCentar.Entities
{
    public class IndividualniTrening
    {
        public virtual int TreningId { get; protected set; }

        public virtual required Trening Trening { get; set; }

        public virtual required Korisnik Korisnik { get; set; }

        public virtual string? CiljeviTreninga { get; set; }

        // Cista M:N (INDIVIDUALNI_TRENING_OSOBLJE) - trener(i) koji vode ovaj trening
        public virtual IList<Osoblje> Treneri { get; set; }

        public IndividualniTrening()
        {
            Treneri = new List<Osoblje>();
        }
    }
}
