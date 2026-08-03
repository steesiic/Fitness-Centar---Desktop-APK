using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class YogaInstruktorMapper : ClassMap<YogaInstruktor>
    {
        public YogaInstruktorMapper()
        {
            Table("YOGA_INSTRUKTOR");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.Foreign("Trener");

            HasOne(x => x.Trener).Constrained();
        }
    }
}
