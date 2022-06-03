using SuperHeroAPI.Models;

namespace SuperHeroAPI.Repository
{
    public interface ISuperHeroRepository
    {
        public IEnumerable<SuperHero> GetSuperHeroes();
        public SuperHero GetSuperHero(Guid id);
        public void AddSuperHero(SuperHero hero);
        public void UpdateSuperHero(SuperHero hero);
        public void DeleteSuperHero(Guid id);
    }
}
