namespace FitnessCentar.Entities
{
    public class Staratelj
    {
        public virtual int StarateljId { get; protected set; }

        public virtual required string Ime { get; set; }

        public virtual required string Prezime { get; set; }

        public virtual string? KontaktTelefon { get; set; }

        public virtual string? Email { get; set; }

        public virtual required Korisnik Korisnik { get; set; }
    }
}
