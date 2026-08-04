namespace FitnessCentar.Entities
{
    public class Evidencija
    {
        public virtual int EvidencijaId { get; protected set; }

        public virtual required Trening Trening { get; set; }

        public virtual required Korisnik Korisnik { get; set; }

        public virtual DaNe? Prisustvo { get; set; }

        public virtual DaNe? Otkazivanje { get; set; }

        public virtual DaNe? Kasnjenje { get; set; }

        public virtual string? PovredeIncidenti { get; set; }
    }

    public enum DaNe
    {
        da,
        ne
    }
}
