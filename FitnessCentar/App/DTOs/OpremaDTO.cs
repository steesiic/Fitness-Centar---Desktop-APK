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

        // Za prikaz
        public string ProstorNaziv { get; set; } = "";

        public int BrojOdrzavanja { get; set; }

        // Za unos - kom prostoru oprema pripada
        public int ProstorId { get; set; }
    }
}
