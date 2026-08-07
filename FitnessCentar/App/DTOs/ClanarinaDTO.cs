using FitnessCentar.Entities;

namespace FitnessCentar.App.DTOs
{
    public class ClanarinaDTO
    {
        public int ClanarinaId { get; set; }

        public string Naziv { get; set; } = "";

        public TipClanarine Tip { get; set; }

        public decimal Cena { get; set; }

        public int? PeriodVazenja { get; set; }

        public int? BrojTermina { get; set; }

        public string? Ogranicenja { get; set; }
    }
}
