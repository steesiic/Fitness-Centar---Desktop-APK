namespace FitnessCentar.Entities
{
    public class IndividualniTrening
    {
        public virtual int TreningId { get; protected set; }

        public virtual required Trening Trening { get; set; }

        public virtual required Korisnik Korisnik { get; set; }

        public virtual string? CiljeviTreninga { get; set; }

        public virtual IList<Osoblje> Treneri { get; set; }

        public IndividualniTrening()
        {
            Treneri = new List<Osoblje>();
        }
    }
}
