using FitnessCentar.App.DTOs;
using FitnessCentar.App.Extensions;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Data;
using FitnessCentar.Entities;
using NHibernate.Linq;

namespace FitnessCentar.App.Services.Implementation
{
    public class ProstorService : IProstorService
    {
        private readonly IDataLayer _dataLayer;

        public ProstorService(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<ServiceResult<IList<ProstorDTO>>> GetAllAsync()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var prostoriEntiteti = await session.Query<Prostor>().ToListAsync();
                var prostori = prostoriEntiteti.Select(p => p.ToProstorDTO()).ToList();

                return ServiceResult<IList<ProstorDTO>>.Success(prostori);
            }
            catch (Exception ex)
            {
                return ServiceResult<IList<ProstorDTO>>.Failure($"Greška pri dohvatanju prostora: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddProstorAsync(ProstorDTO prostorDto)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Prostor noviProstor = prostorDto.CreateNewEntity();

                await session.SaveAsync(noviProstor);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju prostora: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }
    }
}
