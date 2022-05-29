using SuperHeroAPI.Models;

namespace SuperHeroAPI.Repository
{
    public interface ISuperHeroRepository
    {
        public IEnumerable<SuperHero> GetSuperHeroes();
        public SuperHero GetSuperHero(int id);
        public void AddSuperHero(SuperHero hero);
        public void UpdateSuperHero(SuperHero hero);
        public void DeleteSuperHero(int id);
    }
}
