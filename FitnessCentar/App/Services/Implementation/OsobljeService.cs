using FitnessCentar.App.DTOs;
using FitnessCentar.App.Extensions;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Data;
using FitnessCentar.Entities;
using NHibernate.Linq;

namespace FitnessCentar.App.Services.Implementation
{
    public class OsobljeService : IOsobljeService
    {
        private readonly IDataLayer _dataLayer;

        public OsobljeService(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<ServiceResult<IList<OsobljeDTO>>> GetAllAsync()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var osobljeEntiteti = await session.Query<Osoblje>().ToListAsync();
                var osoblje = osobljeEntiteti.Select(o => o.ToOsobljeDTO()).ToList();

                return ServiceResult<IList<OsobljeDTO>>.Success(osoblje);
            }
            catch (Exception ex)
            {
                return ServiceResult<IList<OsobljeDTO>>.Failure($"Greška pri dohvatanju osoblja: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddOsobljeAsync(OsobljeDTO osobljeDto)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Osoblje novoOsoblje = osobljeDto.CreateNewEntity();

                await session.SaveAsync(novoOsoblje);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju osoblja: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> DeleteOsobljeAsync(int osobljeId)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Osoblje? osoblje = await session.GetAsync<Osoblje>(osobljeId);
                if (osoblje == null)
                {
                    return ServiceResult<bool>.Failure("Osoblje sa datim ID-jem ne postoji.");
                }

                await session.DeleteAsync(osoblje);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri brisanju osoblja: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }
    }
}
