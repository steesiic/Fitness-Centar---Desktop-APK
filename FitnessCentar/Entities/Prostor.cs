namespace FitnessCentar.Entities
{
    public class Prostor
    {
        public virtual int ProstorId { get; protected set; }

        public virtual required string Naziv { get; set; }

        // Tip ima vrednosti sa razmakom (npr. "kardio zona", "sala za grupne treninge"),
        // pa je ostavljen kao string, ne enum (C# enum članovi ne mogu imati razmak).
        // Validnost i dalje čuva CHECK ograničenje na bazi.
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
