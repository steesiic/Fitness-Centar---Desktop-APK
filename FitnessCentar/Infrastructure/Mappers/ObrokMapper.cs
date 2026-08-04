using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class ObrokMapper : ClassMap<Obrok>
    {
        public ObrokMapper()
        {
            Table("OBROK");

            Id(x => x.ObrokId).Column("OBROK_ID").GeneratedBy.TriggerIdentity();

            References(x => x.PlanIshrane).Column("PLAN_ISHRANE_ID").Not.Nullable().LazyLoad();

            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.Kalorije).Column("KALORIJE");
            Map(x => x.Proteini).Column("PROTEINI");
            Map(x => x.UgljeniHidrati).Column("UGLJENI_HIDRATI");
            Map(x => x.Masti).Column("MASTI");
            Map(x => x.PreporuceniTermin).Column("PREPORUCENI_TERMIN");
        }
    }
}
