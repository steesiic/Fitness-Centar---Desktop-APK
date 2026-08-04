using FitnessCentar.App.DTOs;

namespace FitnessCentar.App.Services.Interfaces
{
    public interface IFitnessProgramService
    {
        Task<ServiceResult<IList<FitnessProgramDTO>>> GetAllAsync();

        Task<ServiceResult<bool>> AddProgramAsync(FitnessProgramDTO programDto);
    }
}
