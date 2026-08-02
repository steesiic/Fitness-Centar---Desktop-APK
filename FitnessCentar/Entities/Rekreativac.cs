namespace FitnessCentar.Entities
{
    public class Rekreativac
    {
        // Deljeni PK sa Korisnik (PK+FK u DDL-u) - popunjava ga NHibernate preko
        // GeneratedBy.Foreign(...) definisanog u mapiranju, ne unosi se ručno.
        public virtual int KorisnikId { get; protected set; }

        public virtual required Korisnik Korisnik { get; set; }
    }
}
