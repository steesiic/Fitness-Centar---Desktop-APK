using FitnessCentar.App.DTOs;
using FitnessCentar.Entities;
namespace FitnessCentar.App.Extensions
{
    public static class OsobljeExtensions
    {
        public static OsobljeDTO ToOsobljeDTO(this Osoblje osoblje)
        {
            return new OsobljeDTO
            {
                OsobljeId = osoblje.OsobljeId,
                Ime = osoblje.Ime,
                Prezime = osoblje.Prezime,
                Telefon = osoblje.Telefon,
                Email = osoblje.Email,
                DatumZaposlenja = osoblje.DatumZaposlenja,
                Status = osoblje.Status,
                JeTrener = osoblje.Trener != null,
                JeNutricionista = osoblje.Nutricionista != null,
                JeFizioterapeut = osoblje.Fizioterapeut != null,
                JeLekar = osoblje.Lekar != null,
                JeRecepcioner = osoblje.Recepcioner != null,
                JeAdministrator = osoblje.Administrator != null,
                JePersonalniTrener = osoblje.Trener?.PersonalniTrener != null,
                JeGrupniTrener = osoblje.Trener?.GrupniTrener != null,
                JeInstruktorPlivanja = osoblje.Trener?.InstruktorPlivanja != null,
                JeYogaInstruktor = osoblje.Trener?.YogaInstruktor != null,
                JeOnlineTrener = osoblje.Trener?.OnlineTrener != null,
                NivoIskustvaTrenera = osoblje.Trener?.NivoIskustva,

                FizioterapeutLicencaDatum = osoblje.Fizioterapeut?.LicencaDatum,
                FizioterapeutLicencaInstitucija = osoblje.Fizioterapeut?.LicencaInstitucija,

                LekarLicencaDatum = osoblje.Lekar?.LicencaDatum,
                LekarLicencaInstitucija = osoblje.Lekar?.LicencaInstitucija
            };
        }
        public static Osoblje CreateNewEntity(this OsobljeDTO dto)
        {
            var osoblje = new Osoblje
            {
                Ime = dto.Ime,
                Prezime = dto.Prezime,
                Telefon = dto.Telefon,
                Email = dto.Email,
                DatumZaposlenja = dto.DatumZaposlenja,
                Status = dto.Status
            };
            if (dto.JeTrener)
            {
                var trener = new Trener { Osoblje = osoblje, NivoIskustva = dto.NivoIskustvaTrenera };
                if (dto.JePersonalniTrener) trener.PersonalniTrener = new PersonalniTrener { Trener = trener };
                if (dto.JeGrupniTrener) trener.GrupniTrener = new GrupniTrener { Trener = trener };
                if (dto.JeInstruktorPlivanja) trener.InstruktorPlivanja = new InstruktorPlivanja { Trener = trener };
                if (dto.JeYogaInstruktor) trener.YogaInstruktor = new YogaInstruktor { Trener = trener };
                if (dto.JeOnlineTrener) trener.OnlineTrener = new OnlineTrener { Trener = trener };
                osoblje.Trener = trener;
            }
            if (dto.JeNutricionista)
            {
                osoblje.Nutricionista = new Nutricionista { Osoblje = osoblje };
            }
            if (dto.JeRecepcioner)
            {
                osoblje.Recepcioner = new Recepcioner { Osoblje = osoblje };
            }
            if (dto.JeAdministrator)
            {
                osoblje.Administrator = new Administrator { Osoblje = osoblje };
            }

            if (dto.JeFizioterapeut)
            {
                osoblje.Fizioterapeut = new Fizioterapeut
                {
                    Osoblje = osoblje,
                    LicencaDatum = dto.FizioterapeutLicencaDatum,
                    LicencaInstitucija = dto.FizioterapeutLicencaInstitucija
                };
            }

            if (dto.JeLekar)
            {
                osoblje.Lekar = new Lekar
                {
                    Osoblje = osoblje,
                    LicencaDatum = dto.LekarLicencaDatum,
                    LicencaInstitucija = dto.LekarLicencaInstitucija
                };
            }

            return osoblje;
        }
    }
}