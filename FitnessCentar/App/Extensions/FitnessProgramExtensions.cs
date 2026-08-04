using FitnessCentar.App.DTOs;
using FitnessCentar.Entities;

namespace FitnessCentar.App.Extensions
{
    public static class FitnessProgramExtensions
    {
        public static FitnessProgramDTO ToFitnessProgramDTO(this FitnessProgram program)
        {
            return new FitnessProgramDTO
            {
                ProgramId = program.ProgramId,
                Naziv = program.Naziv,
                Tip = program.Tip,
                Cilj = program.Cilj,
                Trajanje = program.Trajanje,
                PreporuceniProfil = program.PreporuceniProfil,
                Ogranicenja = program.Ogranicenja,
                BrojVoditelja = program.Voditelji.Count
            };
        }

        public static FitnessProgram CreateNewEntity(this FitnessProgramDTO dto)
        {
            return new FitnessProgram
            {
                Naziv = dto.Naziv,
                Tip = dto.Tip,
                Cilj = dto.Cilj,
                Trajanje = dto.Trajanje,
                PreporuceniProfil = dto.PreporuceniProfil,
                Ogranicenja = dto.Ogranicenja
            };
        }
    }
}
