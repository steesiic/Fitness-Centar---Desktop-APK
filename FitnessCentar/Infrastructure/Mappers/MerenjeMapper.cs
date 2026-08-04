using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class MerenjeMapper : ClassMap<Merenje>
    {
        public MerenjeMapper()
        {
            Table("MERENJE");

            Id(x => x.MerenjeId).Column("MERENJE_ID").GeneratedBy.TriggerIdentity();

            References(x => x.Korisnik).Column("KORISNIK_ID").Not.Nullable().LazyLoad();
            References(x => x.Osoblje).Column("OSOBLJE_ID").Not.Nullable().LazyLoad();

            Map(x => x.Datum).Column("DATUM");
            Map(x => x.TipMerenja).Column("TIP_MERENJA");
            Map(x => x.NumerickaVrednost).Column("NUMERICKA_VREDNOST");
            Map(x => x.JedinicaMere).Column("JEDINICA_MERE");
            Map(x => x.TekstualniRezultat).Column("TEKSTUALNI_REZULTAT");
            Map(x => x.Napomena).Column("NAPOMENA");
        }
    }
}
