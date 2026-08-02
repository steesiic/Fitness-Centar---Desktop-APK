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

        public ServiceResult<IList<KorisnikDTO>> GetAll()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var korisnici = session.Query<Korisnik>()
                    .ToList()
                    .Select(k => k.ToKorisnikDTO())
                    .ToList();

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

        public ServiceResult<bool> AddKorisnik(KorisnikDTO korisnikDto)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Korisnik noviKorisnik = korisnikDto.CreateNewEntity();

                session.Save(noviKorisnik);
                session.Flush();

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

        public ServiceResult<bool> UpdateKorisnik(KorisnikDTO korisnikDto)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Korisnik? korisnik = session.Get<Korisnik>(korisnikDto.KorisnikId);
                if (korisnik == null)
                {
                    return ServiceResult<bool>.Failure("Korisnik sa datim ID-jem ne postoji.");
                }

                korisnik.Ime = korisnikDto.Ime;
                korisnik.Prezime = korisnikDto.Prezime;
                korisnik.Telefon = korisnikDto.Telefon;
                korisnik.Email = korisnikDto.Email;
                korisnik.StatusClanstva = korisnikDto.StatusClanstva;

                session.SaveOrUpdate(korisnik);
                session.Flush();

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

        public ServiceResult<bool> DeleteKorisnik(int korisnikId)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Korisnik? korisnik = session.Get<Korisnik>(korisnikId);
                if (korisnik == null)
                {
                    return ServiceResult<bool>.Failure("Korisnik sa datim ID-jem ne postoji.");
                }

                session.Delete(korisnik);
                session.Flush();

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
