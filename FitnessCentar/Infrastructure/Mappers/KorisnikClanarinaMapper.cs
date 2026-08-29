using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class KorisnikClanarinaMapper : ClassMap<KorisnikClanarina>
    {
        public KorisnikClanarinaMapper()
        {
            Table("KORISNIK_CLANARINA");

            CompositeId()
                .KeyReference(x => x.Korisnik, "KORISNIK_ID")
                .KeyReference(x => x.Clanarina, "CLANARINA_ID")
                .KeyProperty(x => x.DatumPocetka, "DATUM_POCETKA");

            Map(x => x.DatumIsteka).Column("DATUM_ISTEKA");
        }
    }
}
