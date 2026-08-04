using FitnessCentar.App.DTOs;
using FitnessCentar.App.Extensions;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Data;
using FitnessCentar.Entities;
using NHibernate.Linq;

namespace FitnessCentar.App.Services.Implementation
{
    public class MerenjeEvaluacijaService : IMerenjeEvaluacijaService
    {
        private readonly IDataLayer _dataLayer;

        public MerenjeEvaluacijaService(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<ServiceResult<IList<MerenjeDTO>>> GetAllMerenjaAsync()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var merenjaEntiteti = await session.Query<Merenje>().ToListAsync();
                var merenja = merenjaEntiteti.Select(m => m.ToMerenjeDTO()).ToList();

                return ServiceResult<IList<MerenjeDTO>>.Success(merenja);
            }
            catch (Exception ex)
            {
                return ServiceResult<IList<MerenjeDTO>>.Failure($"Greška pri dohvatanju merenja: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddMerenjeAsync(int korisnikId, int osobljeId, string? tipMerenja, decimal? numerickaVrednost, string? jedinicaMere)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Korisnik korisnik = await session.LoadAsync<Korisnik>(korisnikId);
                Osoblje osoblje = await session.LoadAsync<Osoblje>(osobljeId);

                var novoMerenje = new Merenje
                {
                    Korisnik = korisnik,
                    Osoblje = osoblje,
                    Datum = DateTime.Now,
                    TipMerenja = tipMerenja,
                    NumerickaVrednost = numerickaVrednost,
                    JedinicaMere = jedinicaMere
                };

                await session.SaveAsync(novoMerenje);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju merenja: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<IList<EvaluacijaDTO>>> GetAllEvaluacijeAsync()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var evaluacijeEntiteti = await session.Query<Evaluacija>().ToListAsync();
                var evaluacije = evaluacijeEntiteti.Select(e => e.ToEvaluacijaDTO()).ToList();

                return ServiceResult<IList<EvaluacijaDTO>>.Success(evaluacije);
            }
            catch (Exception ex)
            {
                return ServiceResult<IList<EvaluacijaDTO>>.Failure($"Greška pri dohvatanju evaluacija: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddEvaluacijuAsync(int korisnikId, int osobljeId, string opis, string? preporuka)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Korisnik korisnik = await session.LoadAsync<Korisnik>(korisnikId);
                Osoblje osoblje = await session.LoadAsync<Osoblje>(osobljeId);

                var novaEvaluacija = new Evaluacija
                {
                    Korisnik = korisnik,
                    Osoblje = osoblje,
                    Datum = DateTime.Now,
                    Opis = opis,
                    Preporuka = preporuka
                };

                await session.SaveAsync(novaEvaluacija);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju evaluacije: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }
    }
}
