using FitnessCentar.App.DTOs;

namespace FitnessCentar.App.Services.Interfaces
{
    public interface IMerenjeEvaluacijaService
    {
        Task<ServiceResult<IList<MerenjeDTO>>> GetAllMerenjaAsync();

        Task<ServiceResult<bool>> AddMerenjeAsync(int korisnikId, int osobljeId, string? tipMerenja, decimal? numerickaVrednost, string? jedinicaMere);

        Task<ServiceResult<IList<EvaluacijaDTO>>> GetAllEvaluacijeAsync();

        Task<ServiceResult<bool>> AddEvaluacijuAsync(int korisnikId, int osobljeId, string opis, string? preporuka);
    }
}
