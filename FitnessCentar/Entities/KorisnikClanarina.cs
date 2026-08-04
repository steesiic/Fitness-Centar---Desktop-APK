namespace FitnessCentar.Entities
{
    // Slozen kljuc (korisnik_id + clanarina_id + datum_pocetka) - omogucava da isti korisnik
    // uzme istu clanarinu vise puta u razlicitim periodima. NHibernate zahteva da klase sa
    // CompositeId() imaju svoje Equals/GetHashCode - bez toga baca gresku pri cuvanju/citanju.
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
