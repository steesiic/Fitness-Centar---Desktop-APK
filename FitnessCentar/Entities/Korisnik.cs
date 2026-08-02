namespace FitnessCentar.Entities
{
    public class Korisnik
    {
        public virtual int KorisnikId { get; protected set; }

        public virtual required string Ime { get; set; }

        public virtual required string Prezime { get; set; }

        public virtual required DateTime DatumRodjenja { get; set; }

        public virtual Pol? Pol { get; set; }

        public virtual string? Telefon { get; set; }

        public virtual string? Email { get; set; }

        public virtual string? Ulica { get; set; }

        public virtual string? Grad { get; set; }

        public virtual string? PostanskiBroj { get; set; }

        public virtual required DateTime DatumUclanjenja { get; set; }

        public virtual required StatusClanstva StatusClanstva { get; set; }

        // Preklapajuća specijalizacija - KOMPOZICIJA, ne nasleđivanje.
        // Korisnik može istovremeno biti Rekreativac I ProfesionalniSportista.
        public virtual Rekreativac? Rekreativac { get; set; }

        public virtual ProfesionalniSportista? ProfesionalniSportista { get; set; }

        public virtual RehabilitacioniKorisnik? RehabilitacioniKorisnik { get; set; }

        // Multivalentni atributi (KORISNIK_CILJEVI, KORISNIK_ALERGIJE, KORISNIK_ZDRAVSTVENA_OGRANICENJA)
        // - mapirani direktno kao kolekcije stringova, bez posebnih entitetskih klasa.
        public virtual IList<string> Ciljevi { get; set; }

        public virtual IList<string> Alergije { get; set; }

        public virtual IList<string> ZdravstvenaOgranicenja { get; set; }

        // Jedan korisnik (maloletni) može imati više staratelja (npr. oba roditelja).
        public virtual IList<Staratelj> Staratelji { get; set; }

        public Korisnik()
        {
            Ciljevi = new List<string>();
            Alergije = new List<string>();
            ZdravstvenaOgranicenja = new List<string>();
            Staratelji = new List<Staratelj>();
        }
    }

    public enum Pol
    {
        M,
        Z
    }

    public enum StatusClanstva
    {
        aktivan,
        neaktivan
    }
}
