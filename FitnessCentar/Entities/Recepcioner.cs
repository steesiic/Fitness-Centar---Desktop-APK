namespace FitnessCentar.Entities
{
    public class Recepcioner
    {
        public virtual int OsobljeId { get; protected set; }

        public virtual required Osoblje Osoblje { get; set; }
    }
}
