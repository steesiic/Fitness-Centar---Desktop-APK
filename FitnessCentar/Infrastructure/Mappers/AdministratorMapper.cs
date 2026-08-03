using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class AdministratorMapper : ClassMap<Administrator>
    {
        public AdministratorMapper()
        {
            Table("ADMINISTRATOR");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.Foreign("Osoblje");

            HasOne(x => x.Osoblje).Constrained();
        }
    }
}
