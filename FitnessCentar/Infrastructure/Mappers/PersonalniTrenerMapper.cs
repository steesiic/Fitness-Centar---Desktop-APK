using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class PersonalniTrenerMapper : ClassMap<PersonalniTrener>
    {
        public PersonalniTrenerMapper()
        {
            Table("PERSONALNI_TRENER");

            Id(x => x.OsobljeId).Column("OSOBLJE_ID").GeneratedBy.Foreign("Trener");

            HasOne(x => x.Trener).Constrained();
        }
    }
}
