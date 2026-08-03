using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class RecepcionerMapper : ClassMap<Recepcioner>
    {
        public RecepcionerMapper()
        {
            Table("RECEPCIONER");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.Foreign("Osoblje");

            HasOne(x => x.Osoblje).Constrained();
        }
    }
}
