namespace FitnessCentar.Entities
{
    public class Nutricionista
    {
        public virtual int OsobljeId { get; protected set; }

        public virtual required Osoblje Osoblje { get; set; }

        public virtual IList<string> OblastiRada { get; set; }

        public Nutricionista()
        {
            OblastiRada = new List<string>();
        }
    }
}
