using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class FazaPlanaMapper : ClassMap<FazaPlana>
    {
        public FazaPlanaMapper()
        {
            Table("FAZA_PLANA");

            Id(x => x.FazaId).Column("FAZA_ID").GeneratedBy.TriggerIdentity();

            References(x => x.PlanTreninga).Column("PLAN_ID").Not.Nullable().LazyLoad();

            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.Redosled).Column("REDOSLED");
            Map(x => x.Opis).Column("OPIS");

            HasMany(x => x.Nedelje)
                .KeyColumn("FAZA_ID")
                .OrderBy("BROJ_NEDELJE")
                .LazyLoad()
                .Cascade.All()
                .Inverse();
        }
    }
}
