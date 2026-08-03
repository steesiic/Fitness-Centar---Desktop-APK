namespace FitnessCentar.Entities
{
    public class Fizioterapeut
    {
        public virtual int OsobljeId { get; protected set; }

        public virtual DateTime? LicencaDatum { get; set; }

        public virtual string? LicencaInstitucija { get; set; }

        public virtual required Osoblje Osoblje { get; set; }

        public virtual IList<string> Termini { get; set; }

        public virtual IList<string> OblastiPregleda { get; set; }

        public Fizioterapeut()
        {
            Termini = new List<string>();
            OblastiPregleda = new List<string>();
        }
    }
}
