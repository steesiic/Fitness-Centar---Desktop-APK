using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class OnlineTreningSesijaMapper : ClassMap<OnlineTreningSesija>
    {
        public OnlineTreningSesijaMapper()
        {
            Table("ONLINE_TRENING_SESIJA");

            Id(x => x.TreningId).Column("TRENING_ID").GeneratedBy.Foreign("Trening");

            HasOne(x => x.Trening).Constrained();

            Map(x => x.Platforma).Column("PLATFORMA");
            Map(x => x.Uzivo).Column("UZIVO");
            Map(x => x.PristupniPodaci).Column("PRISTUPNI_PODACI");
            Map(x => x.BrojPregleda).Column("BROJ_PREGLEDA");
        }
    }
}