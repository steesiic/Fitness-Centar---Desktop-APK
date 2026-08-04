namespace FitnessCentar.Entities
{
    public class NedeljaPlana
    {
        public virtual int NedeljaId { get; protected set; }

        public virtual required FazaPlana FazaPlana { get; set; }

        public virtual required int BrojNedelje { get; set; }

        public virtual string? Opis { get; set; }

        public virtual IList<Vezba> Vezbe { get; set; }

        public NedeljaPlana()
        {
            Vezbe = new List<Vezba>();
        }
    }
}
