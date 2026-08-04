namespace FitnessCentar.Entities
{
    public class TradicionalniTrening
    {
        public virtual int TreningId { get; protected set; }

        public virtual required Trening Trening { get; set; }

        public virtual required Prostor Prostor { get; set; }
    }
}
