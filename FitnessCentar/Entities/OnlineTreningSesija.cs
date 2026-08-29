namespace FitnessCentar.Entities
{
    public class OnlineTreningSesija
    {
        public virtual int TreningId { get; protected set; }

        public virtual required Trening Trening { get; set; }

        public virtual string? Platforma { get; set; }
        public virtual int? Uzivo { get; set; }

        public virtual string? PristupniPodaci { get; set; }

        public virtual int BrojPregleda { get; set; }
    }
}
