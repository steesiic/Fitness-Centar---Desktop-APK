using FitnessCentar.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class OsobljeMapper : ClassMap<Osoblje>
    {
        public OsobljeMapper()
        {
            Table("OSOBLJE");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Ime).Column("IME");
            Map(x => x.Prezime).Column("PREZIME");
            Map(x => x.Telefon).Column("TELEFON");
            Map(x => x.Email).Column("EMAIL");
            Map(x => x.DatumZaposlenja).Column("DATUM_ZAPOSLENJA");
            Map(x => x.Status).Column("STATUS").CustomType<EnumStringType<StatusOsoblja>>();

            HasOne(x => x.Trener).Cascade.All().LazyLoad();
            HasOne(x => x.Nutricionista).Cascade.All().LazyLoad();
            HasOne(x => x.Fizioterapeut).Cascade.All().LazyLoad();
            HasOne(x => x.Lekar).Cascade.All().LazyLoad();
            HasOne(x => x.Recepcioner).Cascade.All().LazyLoad();
            HasOne(x => x.Administrator).Cascade.All().LazyLoad();
        }
    }
}
