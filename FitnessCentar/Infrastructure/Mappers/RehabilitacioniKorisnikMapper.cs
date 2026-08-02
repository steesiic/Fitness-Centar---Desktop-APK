using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class RehabilitacioniKorisnikMapper : ClassMap<RehabilitacioniKorisnik>
    {
        public RehabilitacioniKorisnikMapper()
        {
            Table("REHABILITACIONI_KORISNIK");

            Id(x => x.KorisnikId).Column("KORISNIK_ID").GeneratedBy.Foreign("Korisnik");

            Map(x => x.PreporukaLekara).Column("PREPORUKA_LEKARA");
            Map(x => x.Ogranicenja).Column("OGRANICENJA");
            Map(x => x.TipRehabilitacije).Column("TIP_REHABILITACIJE");

            HasOne(x => x.Korisnik).Constrained();
        }
    }
}
