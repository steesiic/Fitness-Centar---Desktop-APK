using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class IndividualniTreningMapper : ClassMap<IndividualniTrening>
    {
        public IndividualniTreningMapper()
        {
            Table("INDIVIDUALNI_TRENING");

            Id(x => x.TreningId).Column("TRENING_ID").GeneratedBy.Foreign("Trening");

            HasOne(x => x.Trening).Constrained();

            References(x => x.Korisnik).Column("KORISNIK_ID").Not.Nullable().LazyLoad();

            Map(x => x.CiljeviTreninga).Column("CILJEVI_TRENINGA");

            HasManyToMany(x => x.Treneri)
                .Table("INDIVIDUALNI_TRENING_OSOBLJE")
                .ParentKeyColumn("TRENING_ID")
                .ChildKeyColumn("OSOBLJE_ID")
                .LazyLoad();
        }
    }
}
