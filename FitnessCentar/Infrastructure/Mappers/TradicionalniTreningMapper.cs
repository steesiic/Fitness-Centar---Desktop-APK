using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class TradicionalniTreningMapper : ClassMap<TradicionalniTrening>
    {
        public TradicionalniTreningMapper()
        {
            Table("TRADICIONALNI_TRENING");

            Id(x => x.TreningId).Column("TRENING_ID").GeneratedBy.Foreign("Trening");

            HasOne(x => x.Trening).Constrained();

            References(x => x.Prostor).Column("PROSTOR_ID").Not.Nullable().LazyLoad();
        }
    }
}
