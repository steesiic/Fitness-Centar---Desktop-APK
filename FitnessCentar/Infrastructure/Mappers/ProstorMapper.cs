using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class ProstorMapper : ClassMap<Prostor>
    {
        public ProstorMapper()
        {
            Table("PROSTOR");

            Id(x => x.ProstorId).Column("PROSTOR_ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.Tip).Column("TIP");
            Map(x => x.Kapacitet).Column("KAPACITET");
            Map(x => x.Lokacija).Column("LOKACIJA");
            Map(x => x.RadnoVreme).Column("RADNO_VREME");

            HasMany(x => x.OpremaList)
                .KeyColumn("PROSTOR_ID")
                .LazyLoad()
                .Inverse();
        }
    }
}
