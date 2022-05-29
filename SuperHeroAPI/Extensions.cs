using SuperHeroAPI.DTO;
using SuperHeroAPI.Models;

namespace SuperHeroAPI
{
    public static class Extensions
    {
        public static SuperheroDto AsDto(this SuperHero hero)
        {
            return new SuperheroDto
            {
                Id = hero.Id,
                Name = hero.Name,
                FirstName = hero.FirstName,
                LastName = hero.LastName,
                Place = hero.Place
            };
        }
    }
}
