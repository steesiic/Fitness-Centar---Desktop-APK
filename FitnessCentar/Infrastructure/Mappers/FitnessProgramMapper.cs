using FitnessCentar.Entities;
using FluentNHibernate.Mapping;
using NHibernate.Type;

namespace FitnessCentar.Infrastructure.Mappers
{
    public class FitnessProgramMapper : ClassMap<FitnessProgram>
    {
        public FitnessProgramMapper()
        {
            Table("PROGRAM");

            Id(x => x.ProgramId).Column("PROGRAM_ID").GeneratedBy.TriggerIdentity();

            Map(x => x.Naziv).Column("NAZIV");
            Map(x => x.Tip).Column("TIP").CustomType<EnumStringType<TipPrograma>>();
            Map(x => x.Cilj).Column("CILJ");
            Map(x => x.Trajanje).Column("TRAJANJE");
            Map(x => x.PreporuceniProfil).Column("PREPORUCENI_PROFIL");
            Map(x => x.Ogranicenja).Column("OGRANICENJA");

            // Cista M:N (OSOBLJE_PROGRAM nema dodatnih atributa) - mapirano samo sa ove strane,
            // ne mora se dirati OsobljeMapper iz Klastera 2.
            HasManyToMany(x => x.Voditelji)
                .Table("OSOBLJE_PROGRAM")
                .ParentKeyColumn("PROGRAM_ID")
                .ChildKeyColumn("OSOBLJE_ID")
                .LazyLoad();
        }
    }
}
