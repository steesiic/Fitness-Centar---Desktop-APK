using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class InstruktorPlivanjaMapper : ClassMap<InstruktorPlivanja>
    {
        public InstruktorPlivanjaMapper()
        {
            Table("INSTRUKTOR_PLIVANJA");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.Foreign("Trener");

            HasOne(x => x.Trener).Constrained();
        }
    }
}
