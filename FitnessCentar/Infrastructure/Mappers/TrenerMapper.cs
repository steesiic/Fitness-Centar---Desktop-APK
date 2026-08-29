using FitnessCentar.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class TrenerMapper : ClassMap<Trener>
    {
        public TrenerMapper()
        {
            Table("TRENER");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.Foreign("Osoblje");

            Map(x => x.NivoIskustva).Column("NIVO_ISKUSTVA").CustomType<EnumStringType<NivoIskustva>>();

            HasOne(x => x.Osoblje).Constrained();

            HasMany(x => x.Licence)
                .Table("TRENER_LICENCE")
                .KeyColumn("OSOBLJE_ID")
                .Element("LICENCA")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.Specijalizacije)
                .Table("TRENER_SPECIJALIZACIJE")
                .KeyColumn("OSOBLJE_ID")
                .Element("SPECIJALIZACIJA")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();

            HasMany(x => x.Sertifikati)
                .Table("TRENER_SERTIFIKATI")
                .KeyColumn("OSOBLJE_ID")
                .Element("SERTIFIKAT")
                .LazyLoad()
                .Cascade.AllDeleteOrphan();

            HasOne(x => x.PersonalniTrener).Cascade.All().LazyLoad();
            HasOne(x => x.GrupniTrener).Cascade.All().LazyLoad();
            HasOne(x => x.InstruktorPlivanja).Cascade.All().LazyLoad();
            HasOne(x => x.YogaInstruktor).Cascade.All().LazyLoad();
            HasOne(x => x.OnlineTrener).Cascade.All().LazyLoad();
        }
    }
}
