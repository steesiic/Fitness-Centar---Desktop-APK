namespace FitnessCentar.Entities
{
    // Slozen kljuc (korisnik_id + program_id + datum_upisa) - isti obrazac kao KorisnikClanarina.
    public class KorisnikProgram
    {
        public virtual required Korisnik Korisnik { get; set; }

        public virtual required FitnessProgram Program { get; set; }

        public virtual DateTime DatumUpisa { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is not KorisnikProgram other) return false;

            return Korisnik.KorisnikId == other.Korisnik.KorisnikId
                && Program.ProgramId == other.Program.ProgramId
                && DatumUpisa == other.DatumUpisa;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Korisnik.KorisnikId, Program.ProgramId, DatumUpisa);
        }
    }
}
