using FitnessCentar.App.DTOs;
using FitnessCentar.App.Extensions;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Data;
using FitnessCentar.Entities;
using NHibernate.Linq;

namespace FitnessCentar.App.Services.Implementation
{
    public class FitnessProgramService : IFitnessProgramService
    {
        private readonly IDataLayer _dataLayer;

        public FitnessProgramService(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<ServiceResult<IList<FitnessProgramDTO>>> GetAllAsync()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var programiEntiteti = await session.Query<FitnessProgram>().ToListAsync();
                var programi = programiEntiteti.Select(p => p.ToFitnessProgramDTO()).ToList();

                return ServiceResult<IList<FitnessProgramDTO>>.Success(programi);
            }
            catch (Exception ex)
            {
                return ServiceResult<IList<FitnessProgramDTO>>.Failure($"Greška pri dohvatanju programa: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddProgramAsync(FitnessProgramDTO programDto)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                FitnessProgram noviProgram = programDto.CreateNewEntity();

                await session.SaveAsync(noviProgram);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju programa: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }
    }
}
