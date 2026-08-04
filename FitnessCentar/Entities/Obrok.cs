namespace FitnessCentar.Entities
{
    public class Obrok
    {
        public virtual int ObrokId { get; protected set; }

        public virtual required PlanIshrane PlanIshrane { get; set; }

        public virtual required string Naziv { get; set; }

        public virtual decimal? Kalorije { get; set; }

        public virtual decimal? Proteini { get; set; }

        public virtual decimal? UgljeniHidrati { get; set; }

        public virtual decimal? Masti { get; set; }

        public virtual string? PreporuceniTermin { get; set; }
    }
}
