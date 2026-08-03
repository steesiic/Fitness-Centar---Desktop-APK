using FitnessCentar.App.DTOs;

namespace FitnessCentar.App.Services.Interfaces
{
    public interface IKorisnikService
    {
        Task<ServiceResult<IList<KorisnikDTO>>> GetAllAsync();

        Task<ServiceResult<bool>> AddKorisnikAsync(KorisnikDTO korisnikDto);

        Task<ServiceResult<bool>> UpdateKorisnikAsync(KorisnikDTO korisnikDto);

        Task<ServiceResult<bool>> DeleteKorisnikAsync(int korisnikId);
    }
}
