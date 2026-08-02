using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class ProfesionalniSportistaMapper : ClassMap<ProfesionalniSportista>
    {
        public ProfesionalniSportistaMapper()
        {
            Table("PROFESIONALNI_SPORTISTA");

            Id(x => x.KorisnikId).Column("KORISNIK_ID").GeneratedBy.Foreign("Korisnik");

            Map(x => x.Sport).Column("SPORT");
            Map(x => x.Klub).Column("KLUB");
            Map(x => x.NivoTakmicenja).Column("NIVO_TAKMICENJA");

            HasOne(x => x.Korisnik).Constrained();
        }
    }
}
