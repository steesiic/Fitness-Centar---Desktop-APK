using FitnessCentar.App.DTOs;

namespace FitnessCentar.App.Services.Interfaces
{
    public interface IOpremaService
    {
        Task<ServiceResult<IList<OpremaDTO>>> GetAllAsync();

        Task<ServiceResult<bool>> AddOpremaAsync(OpremaDTO opremaDto);
    }
}
