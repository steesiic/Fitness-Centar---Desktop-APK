using FitnessCentar.App.DTOs;
using FitnessCentar.Entities;

namespace FitnessCentar.App.Extensions
{
    public static class ClanarinaExtensions
    {
        public static ClanarinaDTO ToClanarinaDTO(this Clanarina clanarina)
        {
            return new ClanarinaDTO
            {
                ClanarinaId = clanarina.ClanarinaId,
                Naziv = clanarina.Naziv,
                Tip = clanarina.Tip,
                Cena = clanarina.Cena,
                PeriodVazenja = clanarina.PeriodVazenja,
                BrojTermina = clanarina.BrojTermina,
                Ogranicenja = clanarina.Ogranicenja
            };
        }

        public static Clanarina CreateNewEntity(this ClanarinaDTO dto)
        {
            return new Clanarina
            {
                Naziv = dto.Naziv,
                Tip = dto.Tip,
                Cena = dto.Cena,
                PeriodVazenja = dto.PeriodVazenja,
                BrojTermina = dto.BrojTermina,
                Ogranicenja = dto.Ogranicenja
            };
        }
    }
}
