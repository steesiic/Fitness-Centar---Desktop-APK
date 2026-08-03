using FitnessCentar.App.DTOs;

namespace FitnessCentar.App.Services.Interfaces
{
    public interface IOsobljeService
    {
        Task<ServiceResult<IList<OsobljeDTO>>> GetAllAsync();

        Task<ServiceResult<bool>> AddOsobljeAsync(OsobljeDTO osobljeDto);

        Task<ServiceResult<bool>> DeleteOsobljeAsync(int osobljeId);
    }
}
