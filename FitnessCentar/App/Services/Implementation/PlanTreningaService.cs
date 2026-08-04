using FitnessCentar.App.DTOs;
using FitnessCentar.App.Extensions;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Data;
using FitnessCentar.Entities;
using NHibernate.Linq;

namespace FitnessCentar.App.Services.Implementation
{
    public class PlanTreningaService : IPlanTreningaService
    {
        private readonly IDataLayer _dataLayer;

        public PlanTreningaService(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<ServiceResult<IList<PlanTreningaDTO>>> GetAllAsync()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var planoviEntiteti = await session.Query<PlanTreninga>().ToListAsync();
                var planovi = planoviEntiteti.Select(p => p.ToPlanTreningaDTO()).ToList();

                return ServiceResult<IList<PlanTreningaDTO>>.Success(planovi);
            }
            catch (Exception ex)
            {
                return ServiceResult<IList<PlanTreningaDTO>>.Failure($"Greška pri dohvatanju planova: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddPlanAsync(int korisnikId, int osobljeId, string naziv)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                Korisnik korisnik = await session.LoadAsync<Korisnik>(korisnikId);
                Osoblje osoblje = await session.LoadAsync<Osoblje>(osobljeId);

                var noviPlan = new PlanTreninga
                {
                    Korisnik = korisnik,
                    Osoblje = osoblje,
                    Naziv = naziv,
                    DatumKreiranja = DateTime.Now
                };

                await session.SaveAsync(noviPlan);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju plana: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddFazuAsync(int planId, string naziv, int redosled, string? opis)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                PlanTreninga plan = await session.LoadAsync<PlanTreninga>(planId);

                var novaFaza = new FazaPlana
                {
                    PlanTreninga = plan,
                    Naziv = naziv,
                    Redosled = redosled,
                    Opis = opis
                };

                await session.SaveAsync(novaFaza);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju faze: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddNedeljuAsync(int fazaId, int brojNedelje, string? opis)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                FazaPlana faza = await session.LoadAsync<FazaPlana>(fazaId);

                var novaNedelja = new NedeljaPlana
                {
                    FazaPlana = faza,
                    BrojNedelje = brojNedelje,
                    Opis = opis
                };

                await session.SaveAsync(novaNedelja);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju nedelje: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddVezbuAsync(int nedeljaId, string naziv, string? tip, int? brojSerija, int? brojPonavljanja)
        {
            var session = _dataLayer.OpenSession();

            try
            {
                NedeljaPlana nedelja = await session.LoadAsync<NedeljaPlana>(nedeljaId);

                var novaVezba = new Vezba
                {
                    NedeljaPlana = nedelja,
                    Naziv = naziv,
                    Tip = tip,
                    BrojSerija = brojSerija,
                    BrojPonavljanja = brojPonavljanja
                };

                await session.SaveAsync(novaVezba);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju vežbe: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }
    }
}
