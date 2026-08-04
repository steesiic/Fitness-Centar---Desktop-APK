namespace FitnessCentar.Entities
{
    // Nazvano FitnessProgram, ne "Program" - kolizija sa FitnessCentar.Program (Main ulazna tačka).
    // Tabela u bazi je i dalje PROGRAM (vidi Table("PROGRAM") u mapiranju).
    public class FitnessProgram
    {
        public virtual int ProgramId { get; protected set; }

        public virtual required string Naziv { get; set; }

        public virtual required TipPrograma Tip { get; set; }

        public virtual string? Cilj { get; set; }

        public virtual string? Trajanje { get; set; }

        public virtual string? PreporuceniProfil { get; set; }

        public virtual string? Ogranicenja { get; set; }

        // M:N bez dodatnih atributa (OSOBLJE_PROGRAM) - "nosioci programa" iz teksta zadatka
        public virtual IList<Osoblje> Voditelji { get; set; }

        public FitnessProgram()
        {
            Voditelji = new List<Osoblje>();
        }
    }

    public enum TipPrograma
    {
        individualni,
        grupni,
        online,
        hibridni,
        rehabilitacioni,
        takmicarski,
        decji
    }
}
