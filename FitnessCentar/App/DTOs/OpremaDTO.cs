namespace FitnessCentar.App.DTOs
{
    public class OpremaDTO
    {
        public int OpremaId { get; set; }

        public string Naziv { get; set; } = "";

        public string Tip { get; set; } = "";

        public string? Proizvodjac { get; set; }

        public DateTime? DatumNabavke { get; set; }

        public string? Status { get; set; }

        public string? Lokacija { get; set; }

        public string? RasporedOdrzavanja { get; set; }

        public string ProstorNaziv { get; set; } = "";

        public int BrojOdrzavanja { get; set; }

        public int ProstorId { get; set; }
    }
}
