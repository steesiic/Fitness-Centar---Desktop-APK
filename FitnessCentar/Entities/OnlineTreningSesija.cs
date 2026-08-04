namespace FitnessCentar.Entities
{
    public class OnlineTreningSesija
    {
        public virtual int TreningId { get; protected set; }

        public virtual required Trening Trening { get; set; }

        public virtual string? Platforma { get; set; }

        // NUMBER(1) u bazi (0/1) - drzimo kao int?, ne bool, da izbegnemo
        // dvosmislenost oko toga kako NHibernate/Oracle driver prevodi bool tip
        // (Oracle nema nativan BOOLEAN kolonski tip).
        public virtual int? Uzivo { get; set; }

        public virtual string? PristupniPodaci { get; set; }

        public virtual int BrojPregleda { get; set; }
    }
}
