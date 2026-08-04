using FitnessCentar.App.DTOs;
using FitnessCentar.App.Extensions;
using FitnessCentar.App.Services.Interfaces;
using FitnessCentar.Data;
using FitnessCentar.Entities;
using NHibernate.Linq;

namespace FitnessCentar.App.Services.Implementation
{
    public class TreningService : ITreningService
    {
        private readonly IDataLayer _dataLayer;

        public TreningService(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public async Task<ServiceResult<IList<TreningDTO>>> GetAllAsync()
        {
            var session = _dataLayer.OpenSession();

            try
            {
                var treninziEntiteti = await session.Query<Trening>().ToListAsync();
                var treninzi = treninziEntiteti.Select(t => t.ToTreningDTO()).ToList();

                return ServiceResult<IList<TreningDTO>>.Success(treninzi);
            }
            catch (Exception ex)
            {
                return ServiceResult<IList<TreningDTO>>.Failure($"Greška pri dohvatanju treninga: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }

        public async Task<ServiceResult<bool>> AddTreningAsync(TreningDTO dto)
        {
            // Poslovno pravilo koje ni baza ni NHibernate ne cuvaju sami -
            // TACNO JEDNO od svake nezavisne specijalizacije mora biti izabrano.
            if (dto.JeIndividualni == dto.JeGrupni)
            {
                return ServiceResult<bool>.Failure(
                    "Trening mora biti TAČNO jedno od: individualni ILI grupni (ne oba, ne nijedan).");
            }

            if (dto.JeTradicionalni == dto.JeOnline)
            {
                return ServiceResult<bool>.Failure(
                    "Trening mora biti TAČNO jedno od: tradicionalni ILI online (ne oba, ne nijedan).");
            }

            if (dto.JeIndividualni && dto.KorisnikId == null)
            {
                return ServiceResult<bool>.Failure("Individualni trening zahteva ID korisnika.");
            }

            if (dto.JeTradicionalni && dto.ProstorId == null)
            {
                return ServiceResult<bool>.Failure("Tradicionalni trening zahteva ID prostora.");
            }

            if (string.IsNullOrWhiteSpace(dto.Tip))
            {
                return ServiceResult<bool>.Failure("Tip treninga je obavezno polje (NOT NULL u bazi).");
            }

            var session = _dataLayer.OpenSession();

            try
            {
                var noviTrening = new Trening
                {
                    Naziv = dto.Naziv,
                    Tip = dto.Tip,
                    DatumVreme = dto.DatumVreme,
                    Status = dto.Status
                };

                if (dto.JeIndividualni)
                {
                    Korisnik korisnik = await session.LoadAsync<Korisnik>(dto.KorisnikId!.Value);
                    noviTrening.IndividualniTrening = new IndividualniTrening
                    {
                        Trening = noviTrening,
                        Korisnik = korisnik,
                        CiljeviTreninga = dto.CiljeviTreninga
                    };
                }
                else
                {
                    noviTrening.GrupniTrening = new GrupniTrening
                    {
                        Trening = noviTrening,
                        MaksUcesnika = dto.MaksUcesnika
                    };
                }

                if (dto.JeTradicionalni)
                {
                    Prostor prostor = await session.LoadAsync<Prostor>(dto.ProstorId!.Value);
                    noviTrening.TradicionalniTrening = new TradicionalniTrening
                    {
                        Trening = noviTrening,
                        Prostor = prostor
                    };
                }
                else
                {
                    noviTrening.OnlineTreningSesija = new OnlineTreningSesija
                    {
                        Trening = noviTrening,
                        Platforma = dto.Platforma,
                        Uzivo = 1,
                        BrojPregleda = 0
                    };
                }

                await session.SaveAsync(noviTrening);
                await session.FlushAsync();

                return ServiceResult<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.Failure($"Greška pri dodavanju treninga: {ex.Message}");
            }
            finally
            {
                session?.Close();
            }
        }
    }
}
