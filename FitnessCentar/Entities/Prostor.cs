namespace FitnessCentar.Entities
{
    public class Prostor
    {
        public virtual int ProstorId { get; protected set; }

        public virtual required string Naziv { get; set; }

        public virtual required string Tip { get; set; }

        public virtual int? Kapacitet { get; set; }

        public virtual string? Lokacija { get; set; }

        public virtual string? RadnoVreme { get; set; }

        public virtual IList<Oprema> OpremaList { get; set; }

        public Prostor()
        {
            OpremaList = new List<Oprema>();
        }
    }
}
