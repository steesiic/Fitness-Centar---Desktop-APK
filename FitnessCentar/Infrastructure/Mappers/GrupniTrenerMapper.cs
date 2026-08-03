using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class GrupniTrenerMapper : ClassMap<GrupniTrener>
    {
        public GrupniTrenerMapper()
        {
            Table("GRUPNI_TRENER");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.Foreign("Trener");

            HasOne(x => x.Trener).Constrained();
        }
    }
}
