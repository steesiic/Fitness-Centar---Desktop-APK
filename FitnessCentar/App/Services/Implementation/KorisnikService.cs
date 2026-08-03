using FitnessCentar.App.DTOs;
using FitnessCentar.App.Extensions;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Data;
using FitnessCentar.Entities;
using NHibernate.Linq;

namespace FitnessCentar.App.Services.Implementation
{
    public class KorisnikService : IKorisnikService
    {
        private readonly IDataLayer _dataLayer;

        public KorisnikService(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<ServiceResult<IList<KorisnikDTO>>> GetAllAsync()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var korisniciEntiteti = await session.Query<Korisnik>().ToListAsync();
                var korisnici = korisniciEntiteti.Select(k => k.ToKorisnikDTO()).ToList();

                return ServiceResult<IList<KorisnikDTO>>.Success(korisnici);
            }
            catch (Exception ex)
            {
                return ServiceResult<IList<KorisnikDTO>>.Failure($"Greška pri dohvatanju korisnika: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddKorisnikAsync(KorisnikDTO korisnikDto)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Korisnik noviKorisnik = korisnikDto.CreateNewEntity();

                await session.SaveAsync(noviKorisnik);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju korisnika: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> UpdateKorisnikAsync(KorisnikDTO korisnikDto)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Korisnik? korisnik = await session.GetAsync<Korisnik>(korisnikDto.KorisnikId);
                if (korisnik == null)
                {
                    return ServiceResult<bool>.Failure("Korisnik sa datim ID-jem ne postoji.");
                }

                korisnik.Ime = korisnikDto.Ime;
                korisnik.Prezime = korisnikDto.Prezime;
                korisnik.Telefon = korisnikDto.Telefon;
                korisnik.Email = korisnikDto.Email;
                korisnik.StatusClanstva = korisnikDto.StatusClanstva;

                await session.SaveOrUpdateAsync(korisnik);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri izmeni korisnika: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> DeleteKorisnikAsync(int korisnikId)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Korisnik? korisnik = await session.GetAsync<Korisnik>(korisnikId);
                if (korisnik == null)
                {
                    return ServiceResult<bool>.Failure("Korisnik sa datim ID-jem ne postoji.");
                }

                await session.DeleteAsync(korisnik);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri brisanju korisnika: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }
    }
}
