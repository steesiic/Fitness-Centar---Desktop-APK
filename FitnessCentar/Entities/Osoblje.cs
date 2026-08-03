namespace FitnessCentar.Entities
{
    public class Osoblje
    {
        public virtual int OsobljeId { get; protected set; }

        public virtual required string Ime { get; set; }

        public virtual required string Prezime { get; set; }

        public virtual string? Telefon { get; set; }

        public virtual string? Email { get; set; }

        public virtual required DateTime DatumZaposlenja { get; set; }

        public virtual required StatusOsoblja Status { get; set; }

        // Preklapajuće uloge - kompozicija (jedna osoba može biti i Trener i Nutricionista istovremeno)
        public virtual Trener? Trener { get; set; }

        public virtual Nutricionista? Nutricionista { get; set; }

        public virtual Fizioterapeut? Fizioterapeut { get; set; }

        public virtual Lekar? Lekar { get; set; }

        public virtual Recepcioner? Recepcioner { get; set; }

        public virtual Administrator? Administrator { get; set; }
    }

    public enum StatusOsoblja
    {
        aktivan,
        neaktivan
    }
}
