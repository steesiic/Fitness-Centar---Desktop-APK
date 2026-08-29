using FitnessCentar.Entities;

namespace FitnessCentar.App.DTOs
{
    public class OsobljeDTO
    {
        public int OsobljeId { get; set; }

        public string Ime { get; set; } = "";

        public string Prezime { get; set; } = "";

        public string? Telefon { get; set; }

        public string? Email { get; set; }

        public DateTime DatumZaposlenja { get; set; }

        public StatusOsoblja Status { get; set; }

        public bool JeTrener { get; set; }

        public bool JeNutricionista { get; set; }

        public bool JeFizioterapeut { get; set; }

        public bool JeLekar { get; set; }

        public bool JeRecepcioner { get; set; }

        public bool JeAdministrator { get; set; }

        public bool JePersonalniTrener { get; set; }

        public bool JeGrupniTrener { get; set; }

        public bool JeInstruktorPlivanja { get; set; }

        public bool JeYogaInstruktor { get; set; }

        public bool JeOnlineTrener { get; set; }

        public NivoIskustva? NivoIskustvaTrenera { get; set; }
    }
}
