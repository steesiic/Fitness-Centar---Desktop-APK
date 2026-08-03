namespace FitnessCentar.App.DTOs
{
    public class ProstorDTO
    {
        public int ProstorId { get; set; }

        public string Naziv { get; set; } = "";

        public string Tip { get; set; } = "";

        public int? Kapacitet { get; set; }

        public string? Lokacija { get; set; }

        public string? RadnoVreme { get; set; }

        public int BrojOpreme { get; set; }
    }
}
