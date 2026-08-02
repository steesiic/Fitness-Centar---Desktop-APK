namespace FitnessCentar.Entities
{
    public class RehabilitacioniKorisnik
    {
        public virtual int KorisnikId { get; protected set; }

        public virtual string? PreporukaLekara { get; set; }

        public virtual string? Ogranicenja { get; set; }

        public virtual string? TipRehabilitacije { get; set; }

        public virtual required Korisnik Korisnik { get; set; }
    }
}
