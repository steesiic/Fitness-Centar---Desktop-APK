using FitnessCentar.App.DTOs;
using FitnessCentar.Entities;

namespace FitnessCentar.App.Extensions
{
    public static class OpremaExtensions
    {
        public static OpremaDTO ToOpremaDTO(this Oprema oprema)
        {
            return new OpremaDTO
            {
                OpremaId = oprema.OpremaId,
                Naziv = oprema.Naziv,
                Tip = oprema.Tip,
                Proizvodjac = oprema.Proizvodjac,
                DatumNabavke = oprema.DatumNabavke,
                Status = oprema.Status,
                Lokacija = oprema.Lokacija,
                RasporedOdrzavanja = oprema.RasporedOdrzavanja,
                ProstorNaziv = oprema.Prostor.Naziv,
                BrojOdrzavanja = oprema.OdrzavanjaList.Count,
                ProstorId = oprema.Prostor.ProstorId
            };
        }

        // Napomena: Prostor se ovde ne kreira (mora vec da postoji u bazi) - dodeljuje se
        // u servisu preko session.Load<Prostor>(dto.ProstorId), zato ova metoda ne postavlja
        // svojstvo Prostor direktno.
        public static Oprema CreateNewEntity(this OpremaDTO dto, Prostor prostor)
        {
            return new Oprema
            {
                Naziv = dto.Naziv,
                Tip = dto.Tip,
                Proizvodjac = dto.Proizvodjac,
                DatumNabavke = dto.DatumNabavke,
                Status = dto.Status,
                Lokacija = dto.Lokacija,
                RasporedOdrzavanja = dto.RasporedOdrzavanja,
                Prostor = prostor
            };
        }
    }
}
