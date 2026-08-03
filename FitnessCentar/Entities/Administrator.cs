namespace FitnessCentar.Entities
{
    public class Administrator
    {
        public virtual int OsobljeId { get; protected set; }

        public virtual required Osoblje Osoblje { get; set; }
    }
}
