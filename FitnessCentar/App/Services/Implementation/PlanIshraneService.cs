using FitnessCentar.App.DTOs;
using FitnessCentar.App.Extensions;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Data;
using FitnessCentar.Entities;
using NHibernate.Linq;

namespace FitnessCentar.App.Services.Implementation
{
    public class PlanIshraneService : IPlanIshraneService
    {
        private readonly IDataLayer _dataLayer;

        public PlanIshraneService(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<ServiceResult<IList<PlanIshraneDTO>>> GetAllAsync()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var planoviEntiteti = await session.Query<PlanIshrane>().ToListAsync();
                var planovi = planoviEntiteti.Select(p => p.ToPlanIshraneDTO()).ToList();

                return ServiceResult<IList<PlanIshraneDTO>>.Success(planovi);
            }
            catch (Exception ex)
            {
                return ServiceResult<IList<PlanIshraneDTO>>.Failure($"Greška pri dohvatanju planova ishrane: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddPlanAsync(int korisnikId, int nutricionistaId, string naziv)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Korisnik korisnik = await session.LoadAsync<Korisnik>(korisnikId);

                // Nutricionista deli PK sa Osoblje (OsobljeId) - isti obrazac ucitavanja kao svuda.
                Nutricionista nutricionista = await session.LoadAsync<Nutricionista>(nutricionistaId);

                var noviPlan = new PlanIshrane
                {
                    Korisnik = korisnik,
                    Nutricionista = nutricionista,
                    Naziv = naziv,
                    DatumKreiranja = DateTime.Now
                };

                await session.SaveAsync(noviPlan);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju plana ishrane: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddObrokAsync(int planIshraneId, string naziv, decimal? kalorije, decimal? proteini)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                PlanIshrane plan = await session.LoadAsync<PlanIshrane>(planIshraneId);

                var noviObrok = new Obrok
                {
                    PlanIshrane = plan,
                    Naziv = naziv,
                    Kalorije = kalorije,
                    Proteini = proteini
                };

                await session.SaveAsync(noviObrok);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju obroka: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddOgranicenjeAsync(int planIshraneId, string ogranicenje)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                PlanIshrane plan = await session.GetAsync<PlanIshrane>(planIshraneId);
                if (plan == null)
                {
                    return ServiceResult<bool>.Failure("Plan ishrane sa datim ID-jem ne postoji.");
                }

                plan.Ogranicenja.Add(ogranicenje);

                await session.SaveOrUpdateAsync(plan);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju ograničenja: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }
    }
}
