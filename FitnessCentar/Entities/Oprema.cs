namespace FitnessCentar.Entities
{
    public class Oprema
    {
        public virtual int OpremaId { get; protected set; }

        public virtual required string Naziv { get; set; }

        public virtual required string Tip { get; set; }

        public virtual string? Proizvodjac { get; set; }

        public virtual DateTime? DatumNabavke { get; set; }

        public virtual string? Status { get; set; }

        public virtual string? Lokacija { get; set; }

        public virtual string? RasporedOdrzavanja { get; set; }

        public virtual required Prostor Prostor { get; set; }

        public virtual IList<Odrzavanje> OdrzavanjaList { get; set; }

        public Oprema()
        {
            OdrzavanjaList = new List<Odrzavanje>();
        }
    }
}
