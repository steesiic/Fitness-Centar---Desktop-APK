using FitnessCentar.Entities;

namespace FitnessCentar.App.DTOs
{
    public class FitnessProgramDTO
    {
        public int ProgramId { get; set; }

        public string Naziv { get; set; } = "";

        public TipPrograma Tip { get; set; }

        public string? Cilj { get; set; }

        public string? Trajanje { get; set; }

        public string? PreporuceniProfil { get; set; }

        public string? Ogranicenja { get; set; }

        public int BrojVoditelja { get; set; }
    }
}
