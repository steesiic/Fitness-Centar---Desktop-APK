using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class GrupniTreningMapper : ClassMap<GrupniTrening>
    {
        public GrupniTreningMapper()
        {
            Table("GRUPNI_TRENING");

            Id(x => x.TreningId).Column("TRENING_ID").GeneratedBy.Foreign("Trening");

            HasOne(x => x.Trening).Constrained();

            Map(x => x.MaksUcesnika).Column("MAKS_UCESNIKA");

            HasManyToMany(x => x.Ucesnici)
                .Table("GRUPNI_TRENING_UCESNICI")
                .ParentKeyColumn("TRENING_ID")
                .ChildKeyColumn("KORISNIK_ID")
                .LazyLoad();

            HasManyToMany(x => x.Treneri)
                .Table("GRUPNI_TRENING_OSOBLJE")
                .ParentKeyColumn("TRENING_ID")
                .ChildKeyColumn("OSOBLJE_ID")
                .LazyLoad();
        }
    }
}
