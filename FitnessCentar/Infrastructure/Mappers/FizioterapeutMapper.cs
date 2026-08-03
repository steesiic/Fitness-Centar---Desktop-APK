using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class FizioterapeutMapper : ClassMap<Fizioterapeut>
    {
        public FizioterapeutMapper()
        {
            Table("FIZIOTERAPEUT");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.Foreign("Osoblje");

            Map(x => x.LicencaDatum).Column("LICENCA_DATUM");
            Map(x => x.LicencaInstitucija).Column("LICENCA_INSTITUCIJA");

            HasOne(x => x.Osoblje).Constrained();

            HasMany(x => x.Termini)
                .Table("FIZIOTERAPEUT_TERMINI")
                .KeyColumn("OSOBLJE_ID")
                .Element("TERMIN")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.OblastiPregleda)
                .Table("FIZIOTERAPEUT_OBLASTI")
                .KeyColumn("OSOBLJE_ID")
                .Element("OBLAST")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();
        }
    }
}
