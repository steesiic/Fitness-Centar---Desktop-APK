using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class VezbaMapper : ClassMap<Vezba>
    {
        public VezbaMapper()
        {
            Table("VEZBA");

            Id(x => x.VezbaId).Column("VEZBA_ID").GeneratedBy.TriggerIdentity();

            References(x => x.NedeljaPlana).Column("NEDELJA_ID").Not.Nullable().LazyLoad();

            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.Tip).Column("TIP");
            Map(x => x.CiljnaMisicnaGrupa).Column("CILJNA_MISICNA_GRUPA");
            Map(x => x.BrojSerija).Column("BROJ_SERIJA");
            Map(x => x.BrojPonavljanja).Column("BROJ_PONAVLJANJA");
            Map(x => x.Trajanje).Column("TRAJANJE");
            Map(x => x.Intenzitet).Column("INTENZITET");
            Map(x => x.Redosled).Column("REDOSLED");
        }
    }
}
