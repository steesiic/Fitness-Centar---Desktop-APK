using FitnessCentar.Entities;
using FluentNHibernate.Mapping;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class NedeljaPlanaMapper : ClassMap<NedeljaPlana>
    {
        public NedeljaPlanaMapper()
        {
            Table("NEDELJA_PLANA");

            Id(x => x.NedeljaId).Column("NEDELJA_ID").GeneratedBy.TriggerIdentity();

            References(x => x.FazaPlana).Column("FAZA_ID").Not.Nullable().LazyLoad();

            Map(x => x.BrojNedelje).Column("BROJ_NEDELJE");
            Map(x => x.Opis).Column("OPIS");

            HasMany(x => x.Vezbe)
                .KeyColumn("NEDELJA_ID")
                .OrderBy("REDOSLED")
                .LazyLoad()
                .Cascade.All()
                .Inverse();
        }
    }
}
