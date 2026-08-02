using FitnessCentar.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class KorisnikMapper : ClassMap<Korisnik>
    {
        public KorisnikMapper()
        {
            Table("KORISNIK");

            Id(x => x.KorisnikId).Column("KORISNIK_ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ime).Column("IME");
            Map(x => x.Prezime).Column("PREZIME");
            Map(x => x.DatumRodjenja).Column("DATUM_RODJENJA");
            Map(x => x.Pol).Column("POL").CustomType<EnumStringType<Pol>>();
            Map(x => x.Telefon).Column("TELEFON");
            Map(x => x.Email).Column("EMAIL");
            Map(x => x.Ulica).Column("ULICA");
            Map(x => x.Grad).Column("GRAD");
            Map(x => x.PostanskiBroj).Column("POSTANSKI_BROJ");
            Map(x => x.DatumUclanjenja).Column("DATUM_UCLANJENJA");
            Map(x => x.StatusClanstva).Column("STATUS_CLANSTVA").CustomType<EnumStringType<StatusClanstva>>();

            // Preklapajuća specijalizacija - 1:1 preko DELJENOG PK (ne preko property-ref,
            // jer je KorisnikId na podklasi istovremeno i PK i FK - standardni "constrained"
            // one-to-one se poklapa po primarnom ključu, property-ref tu nije potreban).
            HasOne(x => x.Rekreativac).Cascade.All().LazyLoad();
            HasOne(x => x.ProfesionalniSportista).Cascade.All().LazyLoad();
            HasOne(x => x.RehabilitacioniKorisnik).Cascade.All().LazyLoad();

            // Multivalentni atributi - kolekcije stringova, bez posebnih klasa
            // LazyLoad() je OBAVEZAN ovde - bez njega se ove kolekcije eager spajaju
            // u isti upit kao i HasOne veze, sto pravi kartezijanski proizvod i izaziva
            // "more than one row" gresku kod ucitavanja Rekreativac/ProfesionalniSportista/itd.
            HasMany(x => x.Ciljevi)
                .Table("KORISNIK_CILJEVI")
                .KeyColumn("KORISNIK_ID")
                .Element("CILJ")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.Alergije)
                .Table("KORISNIK_ALERGIJE")
                .KeyColumn("KORISNIK_ID")
                .Element("ALERGIJA")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.ZdravstvenaOgranicenja)
                .Table("KORISNIK_ZDRAV_OGRANICENJA")
                .KeyColumn("KORISNIK_ID")
                .Element("OGRANICENJE")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();

            // Jedan korisnik moze imati vise staratelja
            HasMany(x => x.Staratelji)
                .KeyColumn("KORISNIK_ID")
                .LazyLoad()
                .Cascade.All()
                .Inverse();
        }
    }
}