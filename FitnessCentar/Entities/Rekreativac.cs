namespace FitnessCentar.Entities
{
    public class Rekreativac
    {
        public virtual int KorisnikId { get; protected set; }

        public virtual required Korisnik Korisnik { get; set; }
    }
}
