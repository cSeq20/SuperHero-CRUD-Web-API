using SuperHeroAPI.Models;

namespace SuperHeroAPI.Repository
{
    public interface ISuperHeroRepository
    {
        public Task<IEnumerable<SuperHero>> GetSuperHeroesAsync();
        public Task<SuperHero> GetSuperHeroAsync(Guid id);
        public Task AddSuperHeroAsync(SuperHero hero);
        public Task UpdateSuperHeroAsync(SuperHero hero);
        public Task DeleteSuperHeroAsync(Guid id);
    }
}
