using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class StarateljMapper : ClassMap<Staratelj>
    {
        public StarateljMapper()
        {
            Table("STARATELJ");

            Id(x => x.StarateljId).Column("STARATELJ_ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ime).Column("IME");
            Map(x => x.Prezime).Column("PREZIME");
            Map(x => x.KontaktTelefon).Column("KONTAKT_TELEFON");
            Map(x => x.Email).Column("EMAIL");

            References(x => x.Korisnik).Column("KORISNIK_ID").LazyLoad();
        }
    }
}
