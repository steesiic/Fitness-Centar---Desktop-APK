using FitnessCentar.App.DTOs;

namespace FitnessCentar.App.Services.Interfaces
{
    public interface IClanarinaService
    {
        Task<ServiceResult<IList<ClanarinaDTO>>> GetAllAsync();

        Task<ServiceResult<bool>> AddClanarinaAsync(ClanarinaDTO clanarinaDto);

        Task<ServiceResult<bool>> UpisiKorisnikaAsync(int korisnikId, int clanarinaId, DateTime datumPocetka, DateTime? datumIsteka);
    }
}
