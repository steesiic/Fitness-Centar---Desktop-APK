namespace FitnessCentar.Entities
{
    public class Trener
    {
        public virtual int OsobljeId { get; protected set; }

        public virtual NivoIskustva? NivoIskustva { get; set; }

        public virtual required Osoblje Osoblje { get; set; }

        public virtual IList<string> Licence { get; set; }

        public virtual IList<string> Specijalizacije { get; set; }

        public virtual IList<string> Sertifikati { get; set; }

        public virtual PersonalniTrener? PersonalniTrener { get; set; }

        public virtual GrupniTrener? GrupniTrener { get; set; }

        public virtual InstruktorPlivanja? InstruktorPlivanja { get; set; }

        public virtual YogaInstruktor? YogaInstruktor { get; set; }

        public virtual OnlineTrener? OnlineTrener { get; set; }

        public Trener()
        {
            Licence = new List<string>();
            Specijalizacije = new List<string>();
            Sertifikati = new List<string>();
        }
    }

    public enum NivoIskustva
    {
        pocetnik,
        srednji,
        napredni
    }
}
