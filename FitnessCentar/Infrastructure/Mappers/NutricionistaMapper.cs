using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class NutricionistaMapper : ClassMap<Nutricionista>
    {
        public NutricionistaMapper()
        {
            Table("NUTRICIONISTA");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.Foreign("Osoblje");

            HasOne(x => x.Osoblje).Constrained();

            HasMany(x => x.OblastiRada)
                .Table("NUTRICIONISTA_OBLASTI")
                .KeyColumn("OSOBLJE_ID")
                .Element("OBLAST")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();
        }
    }
}
