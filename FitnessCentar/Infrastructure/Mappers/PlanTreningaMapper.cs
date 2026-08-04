using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class PlanTreningaMapper : ClassMap<PlanTreninga>
    {
        public PlanTreningaMapper()
        {
            Table("PLAN_TRENINGA");

            Id(x => x.PlanId).Column("PLAN_ID").GeneratedBy.TriggerIdentity();

            References(x => x.Korisnik).Column("KORISNIK_ID").Not.Nullable().LazyLoad();
            References(x => x.Osoblje).Column("OSOBLJE_ID").Not.Nullable().LazyLoad();

            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.DatumKreiranja).Column("DATUM_KREIRANJA");

            HasMany(x => x.Faze)
                .KeyColumn("PLAN_ID")
                .OrderBy("REDOSLED")
                .LazyLoad()
                .Cascade.All()
                .Inverse();
        }
    }
}
