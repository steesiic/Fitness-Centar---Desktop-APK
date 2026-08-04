using FitnessCentar.App.DTOs;

namespace FitnessCentar.App.Services.Interfaces
{
    public interface IPlanTreningaService
    {
        Task<ServiceResult<IList<PlanTreningaDTO>>> GetAllAsync();

        Task<ServiceResult<bool>> AddPlanAsync(int korisnikId, int osobljeId, string naziv);

        Task<ServiceResult<bool>> AddFazuAsync(int planId, string naziv, int redosled, string? opis);

        Task<ServiceResult<bool>> AddNedeljuAsync(int fazaId, int brojNedelje, string? opis);

        Task<ServiceResult<bool>> AddVezbuAsync(int nedeljaId, string naziv, string? tip, int? brojSerija, int? brojPonavljanja);
    }
}
