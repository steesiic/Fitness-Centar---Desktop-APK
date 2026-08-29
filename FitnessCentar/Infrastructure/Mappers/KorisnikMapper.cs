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


            HasOne(x => x.Rekreativac).Cascade.All().LazyLoad();
            HasOne(x => x.ProfesionalniSportista).Cascade.All().LazyLoad();
            HasOne(x => x.RehabilitacioniKorisnik).Cascade.All().LazyLoad();

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

            HasMany(x => x.Staratelji)
                .KeyColumn("KORISNIK_ID")
                .LazyLoad()
                .Cascade.All()
                .Inverse();
        }
    }
}