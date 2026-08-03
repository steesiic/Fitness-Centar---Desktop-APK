using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class OdrzavanjeMapper : ClassMap<Odrzavanje>
    {
        public OdrzavanjeMapper()
        {
            Table("ODRZAVANJE");

            Id(x => x.OdrzavanjeId).Column("ODRZAVANJE_ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Datum).Column("DATUM");
            Map(x => x.Serviser).Column("SERVISER");
            Map(x => x.OpisIntervencije).Column("OPIS_INTERVENCIJE");
            Map(x => x.StatusNakonServisa).Column("STATUS_NAKON_SERVISA");

            References(x => x.Oprema).Column("OPREMA_ID").Not.Nullable().LazyLoad();
        }
    }
}
