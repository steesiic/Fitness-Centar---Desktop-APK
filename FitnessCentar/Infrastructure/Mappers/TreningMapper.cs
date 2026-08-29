using FitnessCentar.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class TreningMapper : ClassMap<Trening>
    {
        public TreningMapper()
        {
            Table("TRENING");

            Id(x => x.TreningId).Column("TRENING_ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.Tip).Column("TIP").Not.Nullable();
            Map(x => x.DatumVreme).Column("DATUM_VREME");
            Map(x => x.Status).Column("STATUS").CustomType<EnumStringType<StatusTreninga>>();

            References(x => x.Program).Column("PROGRAM_ID").Nullable().LazyLoad();

            HasOne(x => x.IndividualniTrening).Cascade.All().LazyLoad();
            HasOne(x => x.GrupniTrening).Cascade.All().LazyLoad();


            HasOne(x => x.TradicionalniTrening).Cascade.All().LazyLoad();
            HasOne(x => x.OnlineTreningSesija).Cascade.All().LazyLoad();

            HasMany(x => x.Evidencije)
                .KeyColumn("TRENING_ID")
                .LazyLoad()
                .Cascade.All()
                .Inverse();
        }
    }
}
