using FitnessCentar.App.DTOs;

namespace FitnessCentar.App.Services.Interfaces
{
    public interface ITreningService
    {
        Task<ServiceResult<IList<TreningDTO>>> GetAllAsync();

        Task<ServiceResult<bool>> AddTreningAsync(TreningDTO treningDto);
    }
}
