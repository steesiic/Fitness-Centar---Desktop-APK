using FitnessCentar.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class EvidencijaMapper : ClassMap<Evidencija>
    {
        public EvidencijaMapper()
        {
            Table("EVIDENCIJA");

            Id(x => x.EvidencijaId).Column("EVIDENCIJA_ID").GeneratedBy.TriggerIdentity();

            References(x => x.Trening).Column("TRENING_ID").Not.Nullable().LazyLoad();
            References(x => x.Korisnik).Column("KORISNIK_ID").Not.Nullable().LazyLoad();

            Map(x => x.Prisustvo).Column("PRISUSTVO").CustomType<EnumStringType<DaNe>>();
            Map(x => x.Otkazivanje).Column("OTKAZIVANJE").CustomType<EnumStringType<DaNe>>();
            Map(x => x.Kasnjenje).Column("KASNJENJE").CustomType<EnumStringType<DaNe>>();
            Map(x => x.PovredeIncidenti).Column("POVREDE_INCIDENTI");
        }
    }
}
