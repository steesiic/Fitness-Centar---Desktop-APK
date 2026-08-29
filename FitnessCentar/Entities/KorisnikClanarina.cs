namespace FitnessCentar.Entities
{
    public class KorisnikClanarina
    {
        public virtual required Korisnik Korisnik { get; set; }

        public virtual required Clanarina Clanarina { get; set; }

        public virtual DateTime DatumPocetka { get; set; }

        public virtual DateTime? DatumIsteka { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not KorisnikClanarina other) return false;

            return Korisnik.KorisnikId == other.Korisnik.KorisnikId
                && Clanarina.ClanarinaId == other.Clanarina.ClanarinaId
                && DatumPocetka == other.DatumPocetka;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Korisnik.KorisnikId, Clanarina.ClanarinaId, DatumPocetka);
        }
    }
}
