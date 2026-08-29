namespace FitnessCentar.Entities
{
    public class Clanarina
    {
        public virtual int ClanarinaId { get; protected set; }

        public virtual required string Naziv { get; set; }

        public virtual required TipClanarine Tip { get; set; }

        public virtual required decimal Cena { get; set; }

        public virtual int? PeriodVazenja { get; set; }

        public virtual int? BrojTermina { get; set; }

        public virtual string? Ogranicenja { get; set; }
    }

    public enum TipClanarine
    {
        mesecna,
        tromesecna,
        godisnja,
        studentska,
        porodicna,
        online,
        premium,
        rehabilitaciona
    }
}
