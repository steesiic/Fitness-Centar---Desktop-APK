using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class KorisnikProgramMapper : ClassMap<KorisnikProgram>
    {
        public KorisnikProgramMapper()
        {
            Table("KORISNIK_PROGRAM");

            CompositeId()
                .KeyReference(x => x.Korisnik, "KORISNIK_ID")
                .KeyReference(x => x.Program, "PROGRAM_ID")
                .KeyProperty(x => x.DatumUpisa, "DATUM_UPISA");
        }
    }
}
