namespace FitnessCentar.Entities
{
    public class InstruktorPlivanja
    {
        public virtual int OsobljeId { get; protected set; }

        public virtual required Trener Trener { get; set; }
    }
}
