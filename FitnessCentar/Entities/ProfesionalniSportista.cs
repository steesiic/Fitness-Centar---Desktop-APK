namespace FitnessCentar.Entities
{
    public class ProfesionalniSportista
    {
        public virtual int KorisnikId { get; protected set; }

        public virtual required string Sport { get; set; }

        public virtual string? Klub { get; set; }

        public virtual string? NivoTakmicenja { get; set; }

        public virtual required Korisnik Korisnik { get; set; }
    }
}
