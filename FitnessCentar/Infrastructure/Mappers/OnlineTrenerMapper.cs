using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class OnlineTrenerMapper : ClassMap<OnlineTrener>
    {
        public OnlineTrenerMapper()
        {
            Table("ONLINE_TRENER");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.Foreign("Trener");

            HasOne(x => x.Trener).Constrained();
        }
    }
}
