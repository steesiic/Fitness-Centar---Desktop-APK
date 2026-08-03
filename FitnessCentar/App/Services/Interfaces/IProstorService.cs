using FitnessCentar.App.DTOs;

namespace FitnessCentar.App.Services.Interfaces
{
    public interface IProstorService
    {
        Task<ServiceResult<IList<ProstorDTO>>> GetAllAsync();

        Task<ServiceResult<bool>> AddProstorAsync(ProstorDTO prostorDto);
    }
}
