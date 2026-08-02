using FitnessCentar.App.DTOs;

namespace FitnessCentar.App.Services.Interfaces
{
    public interface IKorisnikService
    {
        ServiceResult<IList<KorisnikDTO>> GetAll();

        ServiceResult<bool> AddKorisnik(KorisnikDTO korisnikDto);

        ServiceResult<bool> UpdateKorisnik(KorisnikDTO korisnikDto);

        ServiceResult<bool> DeleteKorisnik(int korisnikId);
    }
}
