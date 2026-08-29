using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class RekreativacMapper : ClassMap<Rekreativac>
    {
        public RekreativacMapper()
        {
            Table("REKREATIVAC");

            Id(x => x.KorisnikId).Column("KORISNIK_ID").GeneratedBy.Foreign("Korisnik");

            HasOne(x => x.Korisnik).Constrained();
        }
    }
}
