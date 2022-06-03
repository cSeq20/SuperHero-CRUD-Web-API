using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<List<SuperheroDto>>> GetSuperHeros()
        {
            var superHeroes = _repository.GetSuperHeroes().Select(hero => hero.AsDto());
            return Ok(superHeroes);
        }

        // GET /superhero/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperheroDto>> GetSuperHero(Guid id)
        {
            var superHero = _repository.GetSuperHero(id);
            return superHero == null ? NotFound("Superhero not found") : Ok(superHero.AsDto());
        }

        // POST /superhero
        [HttpPost]
        public async Task<ActionResult<CreateSuperHeroDto>> AddHero(CreateSuperHeroDto heroDto)
        {
            SuperHero hero = new()
            {
                Id = new Guid(),
                Name = heroDto.Name,
                FirstName = heroDto.FirstName,
                LastName = heroDto.LastName,
                Place = heroDto.Place,
            };

            _repository.AddSuperHero(hero);
            return CreatedAtAction(nameof (GetSuperHero), new { id = hero.Id}, hero.AsDto());
        }

        // PUT /superhero/id
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateHero(Guid id, UpdateSuperHeroDto superheroDto)
        {
            var existingHero = _repository.GetSuperHero(id);
            if (existingHero is null)
                return NotFound();

            SuperHero updatedHero = existingHero with
            {
                Name = superheroDto.Name,
                FirstName = superheroDto.FirstName,
                LastName = superheroDto.LastName,
                Place = superheroDto.Place
            };

            _repository.UpdateSuperHero(updatedHero);
            return NoContent();
        }
        
        // DELETE /superhero/id
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existingHero = _repository.GetSuperHero(id);
            if (existingHero is null)
                return NotFound();

            _repository.DeleteSuperHero(id);
            return NoContent();

       }
    }
}
