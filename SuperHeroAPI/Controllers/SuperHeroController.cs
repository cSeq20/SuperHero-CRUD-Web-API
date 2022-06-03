using Microsoft.AspNetCore.Mvc;
using SuperHeroAPI.DTO;
using SuperHeroAPI.Models;
using SuperHeroAPI.Repository;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroRepository _repository;

        public SuperHeroController(ISuperHeroRepository repository)
        {
            _repository = repository;
        }

        // GET /superhero
        [HttpGet]
        public async Task<ActionResult<List<SuperheroDto>>> GetSuperHerosAsync()
        {
            var superHeroes = (await _repository.GetSuperHeroesAsync())
                              .Select(hero => hero.AsDto());
            return Ok(superHeroes);
        }

        // GET /superhero/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperheroDto>> GetSuperHeroAsync(Guid id)
        {
            var superHero = await _repository.GetSuperHeroAsync(id);
            return superHero == null ? NotFound("Superhero not found") : Ok(superHero.AsDto());
        }

        // POST /superhero
        [HttpPost]
        public async Task<ActionResult<CreateSuperHeroDto>> AddHeroAsync(CreateSuperHeroDto heroDto)
        {
            SuperHero hero = new()
            {
                Id = new Guid(),
                Name = heroDto.Name,
                FirstName = heroDto.FirstName,
                LastName = heroDto.LastName,
                Place = heroDto.Place,
            };

            await _repository.AddSuperHeroAsync(hero);
            return CreatedAtAction(nameof (GetSuperHeroAsync), new { id = hero.Id}, hero.AsDto());
        }

        // PUT /superhero/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHeroAsync(Guid id, UpdateSuperHeroDto superheroDto)
        {
            var existingHero = await _repository.GetSuperHeroAsync(id);
            if (existingHero is null)
                return NotFound();

            SuperHero updatedHero = existingHero with
            {
                Name = superheroDto.Name,
                FirstName = superheroDto.FirstName,
                LastName = superheroDto.LastName,
                Place = superheroDto.Place
            };

            await _repository.UpdateSuperHeroAsync(updatedHero);
            return NoContent();
        }
        
        // DELETE /superhero/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var existingHero = await _repository.GetSuperHeroAsync(id);
            if (existingHero is null)
                return NotFound();

            await _repository.DeleteSuperHeroAsync(id);
            return NoContent();

       }
    }
}
