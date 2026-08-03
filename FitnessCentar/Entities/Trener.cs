namespace FitnessCentar.Entities
{
    public class Trener
    {
        public virtual int OsobljeId { get; protected set; }

        public virtual NivoIskustva? NivoIskustva { get; set; }

        public virtual required Osoblje Osoblje { get; set; }

        // Multivalentni atributi
        public virtual IList<string> Licence { get; set; }

        public virtual IList<string> Specijalizacije { get; set; }

        public virtual IList<string> Sertifikati { get; set; }

        // Trener ima svoju dalju preklapajuću specijalizaciju (pod-uloge) - opet kompozicija,
        // vezana za Trener.OsobljeId (ne direktno za Osoblje.OsobljeId)
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
