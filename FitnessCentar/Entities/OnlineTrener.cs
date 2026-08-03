namespace FitnessCentar.Entities
{
    public class OnlineTrener
    {
        public virtual int OsobljeId { get; protected set; }

        public virtual required Trener Trener { get; set; }
    }
}
