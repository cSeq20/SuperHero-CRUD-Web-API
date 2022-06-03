using SuperHeroAPI.Models;

namespace SuperHeroAPI.Repository
{
    public class InMemSuperHeroRepository : ISuperHeroRepository
    {
        private readonly List<SuperHero> _superHeroes = new()
        {
            new SuperHero {Id = Guid.NewGuid(), FirstName = "Bruce", LastName = "Banner", Name = "Hulk", Place = string.Empty },
            new SuperHero {Id = Guid.NewGuid(), FirstName = "Peter", LastName = "Parker", Name = "Spiderman", Place = "New York" },
            new SuperHero {Id = Guid.NewGuid(), FirstName = "Tony", LastName = "Stark", Name = "Ironman", Place = "Long Island" }
        };

        public async Task<IEnumerable<SuperHero>> GetSuperHeroesAsync()
        {
            return await Task.FromResult(_superHeroes);
        }

        public async Task<SuperHero> GetSuperHeroAsync(Guid id)
        {
            var hero = _superHeroes.SingleOrDefault(hero => hero.Id == id);
            return await Task.FromResult(hero);
        }

        public async Task AddSuperHeroAsync(SuperHero hero)
        {
            _superHeroes.Add(hero);
            await Task.CompletedTask;
        }

        public async Task UpdateSuperHeroAsync(SuperHero hero)
        {
            var index = _superHeroes.FindIndex(existing => existing.Id == hero.Id);
            _superHeroes[index] = hero;
            await Task.CompletedTask;
        }

        public async Task DeleteSuperHeroAsync(Guid id)
        {
            var index = _superHeroes.FindIndex(existing => existing.Id == id);
            _superHeroes.RemoveAt(index);
            await Task.CompletedTask;
        }
    }
}