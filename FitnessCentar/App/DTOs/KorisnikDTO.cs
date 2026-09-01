using FitnessCentar.Entities;
namespace FitnessCentar.App.DTOs
{
    public class KorisnikDTO
    {
        public int KorisnikId { get; set; }
        public string Ime { get; set; } = "";
        public string Prezime { get; set; } = "";
        public DateTime DatumRodjenja { get; set; }
        public Pol? Pol { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public string? Ulica { get; set; }
        public string? Grad { get; set; }
        public string? PostanskiBroj { get; set; }
        public DateTime DatumUclanjenja { get; set; }
        public StatusClanstva StatusClanstva { get; set; }
        public bool JeRekreativac { get; set; }
        public bool JeProfesionalniSportista { get; set; }
        public bool JeRehabilitacioniKorisnik { get; set; }
        public IList<string> Ciljevi { get; set; } = new List<string>();
        public string? Sport { get; set; }
        public string? Klub { get; set; }
        public string? NivoTakmicenja { get; set; }
        public string? PreporukaLekara { get; set; }
        public string? Ogranicenja { get; set; }
        public string? TipRehabilitacije { get; set; }
    }
}