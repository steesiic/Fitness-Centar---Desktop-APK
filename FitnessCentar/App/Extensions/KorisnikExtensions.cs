using FitnessCentar.App.DTOs;
using FitnessCentar.Entities;

namespace FitnessCentar.App.Extensions
{
    public static class KorisnikExtensions
    {
        public static KorisnikDTO ToKorisnikDTO(this Korisnik korisnik)
        {
            return new KorisnikDTO
            {
                KorisnikId = korisnik.KorisnikId,
                Ime = korisnik.Ime,
                Prezime = korisnik.Prezime,
                DatumRodjenja = korisnik.DatumRodjenja,
                Pol = korisnik.Pol,
                Telefon = korisnik.Telefon,
                Email = korisnik.Email,
                Ulica = korisnik.Ulica,
                Grad = korisnik.Grad,
                PostanskiBroj = korisnik.PostanskiBroj,
                DatumUclanjenja = korisnik.DatumUclanjenja,
                StatusClanstva = korisnik.StatusClanstva,
                JeRekreativac = korisnik.Rekreativac != null,
                JeProfesionalniSportista = korisnik.ProfesionalniSportista != null,
                JeRehabilitacioniKorisnik = korisnik.RehabilitacioniKorisnik != null,
                Ciljevi = korisnik.Ciljevi
            };
        }

        public static Korisnik CreateNewEntity(this KorisnikDTO dto)
        {
            var korisnik = new Korisnik
            {
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                DatumRodjenja = dto.DatumRodjenja,
                Pol = dto.Pol,
                Telefon = dto.Telefon,
                Email = dto.Email,
                Ulica = dto.Ulica,
                Grad = dto.Grad,
                PostanskiBroj = dto.PostanskiBroj,
                DatumUclanjenja = dto.DatumUclanjenja,
                StatusClanstva = dto.StatusClanstva
            };

            foreach (var cilj in dto.Ciljevi)
            {
                korisnik.Ciljevi.Add(cilj);
            }

            if (dto.JeRekreativac)
            {
                korisnik.Rekreativac = new Rekreativac { Korisnik = korisnik };
            }

            return korisnik;
        }
    }
}
