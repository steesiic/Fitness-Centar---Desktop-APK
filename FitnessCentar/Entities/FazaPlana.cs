namespace FitnessCentar.Entities
{
    public class FazaPlana
    {
        public virtual int FazaId { get; protected set; }

        public virtual required PlanTreninga PlanTreninga { get; set; }

        public virtual string? Naziv { get; set; }

        public virtual required int Redosled { get; set; }

        public virtual string? Opis { get; set; }

        public virtual IList<NedeljaPlana> Nedelje { get; set; }

        public FazaPlana()
        {
            Nedelje = new List<NedeljaPlana>();
        }
    }
}
