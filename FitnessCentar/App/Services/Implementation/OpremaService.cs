using FitnessCentar.App.DTOs;
using FitnessCentar.App.Extensions;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Data;
using FitnessCentar.Entities;
using NHibernate.Linq;

namespace FitnessCentar.App.Services.Implementation
{
    public class OpremaService : IOpremaService
    {
        private readonly IDataLayer _dataLayer;

        public OpremaService(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<ServiceResult<IList<OpremaDTO>>> GetAllAsync()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var opremaEntiteti = await session.Query<Oprema>().ToListAsync();
                var oprema = opremaEntiteti.Select(o => o.ToOpremaDTO()).ToList();

                return ServiceResult<IList<OpremaDTO>>.Success(oprema);
            }
            catch (Exception ex)
            {
                return ServiceResult<IList<OpremaDTO>>.Failure($"Greška pri dohvatanju opreme: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddOpremaAsync(OpremaDTO opremaDto)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                // Load umesto Get - pravi samo "referencu" (proxy) ka postojećem Prostoru,
                // bez nepotrebnog SELECT-a, jer nam treba samo za FK pri INSERT-u.
                Prostor prostor = await session.LoadAsync<Prostor>(opremaDto.ProstorId);

                Oprema novaOprema = opremaDto.CreateNewEntity(prostor);

                await session.SaveAsync(novaOprema);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju opreme: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }
    }
}
