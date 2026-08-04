namespace FitnessCentar.Entities
{
    public class GrupniTrening
    {
        public virtual int TreningId { get; protected set; }

        public virtual required Trening Trening { get; set; }

        public virtual int? MaksUcesnika { get; set; }

        public virtual IList<Korisnik> Ucesnici { get; set; }

        public virtual IList<Osoblje> Treneri { get; set; }

        public GrupniTrening()
        {
            Ucesnici = new List<Korisnik>();
            Treneri = new List<Osoblje>();
        }
    }
}
