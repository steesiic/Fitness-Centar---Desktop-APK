using FitnessCentar.App.DTOs;
using FitnessCentar.App.Extensions;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Data;
using FitnessCentar.Entities;
using NHibernate.Linq;

namespace FitnessCentar.App.Services.Implementation
{
    public class ClanarinaService : IClanarinaService
    {
        private readonly IDataLayer _dataLayer;

        public ClanarinaService(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<ServiceResult<IList<ClanarinaDTO>>> GetAllAsync()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var clanarineEntiteti = await session.Query<Clanarina>().ToListAsync();
                var clanarine = clanarineEntiteti.Select(c => c.ToClanarinaDTO()).ToList();

                return ServiceResult<IList<ClanarinaDTO>>.Success(clanarine);
            }
            catch (Exception ex)
            {
                return ServiceResult<IList<ClanarinaDTO>>.Failure($"Greška pri dohvatanju članarina: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddClanarinaAsync(ClanarinaDTO clanarinaDto)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Clanarina novaClanarina = clanarinaDto.CreateNewEntity();

                await session.SaveAsync(novaClanarina);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju članarine: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> UpisiKorisnikaAsync(int korisnikId, int clanarinaId, DateTime datumPocetka, DateTime? datumIsteka)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Korisnik korisnik = await session.LoadAsync<Korisnik>(korisnikId);
                Clanarina clanarina = await session.LoadAsync<Clanarina>(clanarinaId);

                var upis = new KorisnikClanarina
                {
                    Korisnik = korisnik,
                    Clanarina = clanarina,
                    DatumPocetka = datumPocetka,
                    DatumIsteka = datumIsteka
                };

                await session.SaveAsync(upis);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri upisu korisnika u članarinu: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }
    }
}
