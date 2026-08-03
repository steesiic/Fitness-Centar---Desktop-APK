using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class LekarMapper : ClassMap<Lekar>
    {
        public LekarMapper()
        {
            Table("LEKAR");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.Foreign("Osoblje");

            Map(x => x.LicencaDatum).Column("LICENCA_DATUM");
            Map(x => x.LicencaInstitucija).Column("LICENCA_INSTITUCIJA");

            HasOne(x => x.Osoblje).Constrained();

            HasMany(x => x.Termini)
                .Table("LEKAR_TERMINI")
                .KeyColumn("OSOBLJE_ID")
                .Element("TERMIN")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.OblastiPregleda)
                .Table("LEKAR_OBLASTI")
                .KeyColumn("OSOBLJE_ID")
                .Element("OBLAST")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();
        }
    }
}
