using FitnessCentar.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class ClanarinaMapper : ClassMap<Clanarina>
    {
        public ClanarinaMapper()
        {
            Table("CLANARINA");

            Id(x => x.ClanarinaId).Column("CLANARINA_ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.Tip).Column("TIP").CustomType<EnumStringType<TipClanarine>>();
            Map(x => x.Cena).Column("CENA");
            Map(x => x.PeriodVazenja).Column("PERIOD_VAZENJA");
            Map(x => x.BrojTermina).Column("BROJ_TERMINA");
            Map(x => x.Ogranicenja).Column("OGRANICENJA");
        }
    }
}
