namespace FitnessCentar.Entities
{
    public class Trening
    {
        public virtual int TreningId { get; protected set; }

        public virtual required string Naziv { get; set; }

        public virtual required string Tip { get; set; }

        public virtual required DateTime DatumVreme { get; set; }

        public virtual required StatusTreninga Status { get; set; }

        public virtual FitnessProgram? Program { get; set; }

        public virtual IndividualniTrening? IndividualniTrening { get; set; }

        public virtual GrupniTrening? GrupniTrening { get; set; }

        public virtual TradicionalniTrening? TradicionalniTrening { get; set; }

        public virtual OnlineTreningSesija? OnlineTreningSesija { get; set; }

        public virtual IList<Evidencija> Evidencije { get; set; }

        public Trening()
        {
            Evidencije = new List<Evidencija>();
        }
    }

    public enum StatusTreninga
    {
        zakazan,
        odrzan,
        otkazan
    }
}
