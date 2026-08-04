using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class PlanIshraneMapper : ClassMap<PlanIshrane>
    {
        public PlanIshraneMapper()
        {
            Table("PLAN_ISHRANE");

            Id(x => x.PlanIshraneId).Column("PLAN_ISHRANE_ID").GeneratedBy.TriggerIdentity();

            References(x => x.Korisnik).Column("KORISNIK_ID").Not.Nullable().LazyLoad();

            // OSOBLJE_ID kolona u bazi i dalje postoji, ali FK ograničenje cilja NUTRICIONISTA -
            // References ovde mora da cilja isti C# tip (Nutricionista) da se poklopi sa FK.
            References(x => x.Nutricionista).Column("OSOBLJE_ID").Not.Nullable().LazyLoad();

            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.DatumKreiranja).Column("DATUM_KREIRANJA");

            HasMany(x => x.Obroci)
                .KeyColumn("PLAN_ISHRANE_ID")
                .LazyLoad()
                .Cascade.All()
                .Inverse();

            HasMany(x => x.Ogranicenja)
                .Table("PLAN_ISHRANE_OGRANICENJA")
                .KeyColumn("PLAN_ISHRANE_ID")
                .Element("OGRANICENJE")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();
        }
    }
}
