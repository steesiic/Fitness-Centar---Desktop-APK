namespace FitnessCentar.Entities
{
    public class Vezba
    {
        public virtual int VezbaId { get; protected set; }

        public virtual required NedeljaPlana NedeljaPlana { get; set; }

        public virtual required string Naziv { get; set; }

        public virtual string? Tip { get; set; }

        public virtual string? CiljnaMisicnaGrupa { get; set; }

        public virtual int? BrojSerija { get; set; }

        public virtual int? BrojPonavljanja { get; set; }

        public virtual string? Trajanje { get; set; }

        public virtual string? Intenzitet { get; set; }

        public virtual int? Redosled { get; set; }
    }
}
