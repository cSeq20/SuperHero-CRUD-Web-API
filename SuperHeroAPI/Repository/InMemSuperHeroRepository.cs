using SuperHeroAPI.Models;

namespace SuperHeroAPI.Repository
{
    public class InMemSuperHeroRepository : ISuperHeroRepository
    {
        private readonly List<SuperHero> _superHeroes = new()
        {
            new SuperHero {Id = 1, FirstName = "Bruce", LastName = "Banner", Name = "Hulk", Place = string.Empty },
            new SuperHero {Id = 2, FirstName = "Peter", LastName = "Parker", Name = "Spiderman", Place = "New York" },
            new SuperHero {Id = 3, FirstName = "Tony", LastName = "Stark", Name = "Ironman", Place = "Long Island" }
        };

        public IEnumerable<SuperHero> GetSuperHeroes()
        {
            return _superHeroes;
        }

        public SuperHero GetSuperHero(int id)
        {
            return _superHeroes.SingleOrDefault(hero => hero.Id == id);
        }

        public void AddSuperHero(SuperHero hero)
        {
            _superHeroes.Add(hero);
        }

        public void UpdateSuperHero(SuperHero hero)
        {
            var index = _superHeroes.FindIndex(existing => existing.Id == hero.Id);
            _superHeroes[index] = hero;
        }

        public void DeleteSuperHero(int id)
        {
            var index = _superHeroes.FindIndex(existing => existing.Id == id);
            _superHeroes.RemoveAt(index);
        }
    }
}
