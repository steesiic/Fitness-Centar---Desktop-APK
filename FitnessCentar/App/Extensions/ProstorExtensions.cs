using FitnessCentar.App.DTOs;
using FitnessCentar.Entities;

namespace FitnessCentar.App.Extensions
{
    public static class ProstorExtensions
    {
        public static ProstorDTO ToProstorDTO(this Prostor prostor)
        {
            return new ProstorDTO
            {
                ProstorId = prostor.ProstorId,
                Naziv = prostor.Naziv,
                Tip = prostor.Tip,
                Kapacitet = prostor.Kapacitet,
                Lokacija = prostor.Lokacija,
                RadnoVreme = prostor.RadnoVreme,
                BrojOpreme = prostor.OpremaList.Count
            };
        }

        public static Prostor CreateNewEntity(this ProstorDTO dto)
        {
            return new Prostor
            {
                Naziv = dto.Naziv,
                Tip = dto.Tip,
                Kapacitet = dto.Kapacitet,
                Lokacija = dto.Lokacija,
                RadnoVreme = dto.RadnoVreme
            };
        }
    }
}
