using FitnessCentar.App.DTOs;

namespace FitnessCentar.App.Services.Interfaces
{
    public interface IPlanIshraneService
    {
        Task<ServiceResult<IList<PlanIshraneDTO>>> GetAllAsync();

        Task<ServiceResult<bool>> AddPlanAsync(int korisnikId, int nutricionistaId, string naziv);

        Task<ServiceResult<bool>> AddObrokAsync(int planIshraneId, string naziv, decimal? kalorije, decimal? proteini);

        Task<ServiceResult<bool>> AddOgranicenjeAsync(int planIshraneId, string ogranicenje);
    }
}
