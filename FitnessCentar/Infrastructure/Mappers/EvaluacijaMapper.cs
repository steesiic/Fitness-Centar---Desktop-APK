using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class EvaluacijaMapper : ClassMap<Evaluacija>
    {
        public EvaluacijaMapper()
        {
            Table("EVALUACIJA");

            Id(x => x.EvaluacijaId).Column("EVALUACIJA_ID").GeneratedBy.TriggerIdentity();

            References(x => x.Korisnik).Column("KORISNIK_ID").Not.Nullable().LazyLoad();
            References(x => x.Osoblje).Column("OSOBLJE_ID").Not.Nullable().LazyLoad();

            Map(x => x.Datum).Column("DATUM");
            Map(x => x.Opis).Column("OPIS");
            Map(x => x.Preporuka).Column("PREPORUKA");
        }
    }
}
