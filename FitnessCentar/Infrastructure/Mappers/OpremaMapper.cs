using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class OpremaMapper : ClassMap<Oprema>
    {
        public OpremaMapper()
        {
            Table("OPREMA");

            Id(x => x.OpremaId).Column("OPREMA_ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.Tip).Column("TIP");
            Map(x => x.Proizvodjac).Column("PROIZVODJAC");
            Map(x => x.DatumNabavke).Column("DATUM_NABAVKE");
            Map(x => x.Status).Column("STATUS");
            Map(x => x.Lokacija).Column("LOKACIJA");
            Map(x => x.RasporedOdrzavanja).Column("RASPORED_ODRZAVANJA");

            References(x => x.Prostor).Column("PROSTOR_ID").Not.Nullable().LazyLoad();

            // DDL ima ON DELETE CASCADE za odrzavanje->oprema, pa i ovde Cascade.All()
            HasMany(x => x.OdrzavanjaList)
                .KeyColumn("OPREMA_ID")
                .LazyLoad()
                .Cascade.All()
                .Inverse();
        }
    }
}
