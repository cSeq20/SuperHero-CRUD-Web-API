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
        private readonly DataContext _dataContext;
        private readonly ISuperHeroRepository _repository;

        public SuperHeroController(DataContext dataContext, ISuperHeroRepository repository)
        {
            _dataContext = dataContext;
            _repository = repository;
        }

        // GET /superhero
        [HttpGet]
        public async Task<ActionResult<List<SuperheroDto>>> GetSuperHeros()
        {
            //return Ok(await _dataContext.SuperHeroes.ToListAsync());
            var superHeroes = _repository.GetSuperHeroes().Select(hero => hero.AsDto());
            return Ok(superHeroes);
        }

        // GET /superhero/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperheroDto>> GetSuperHero(int id)
        {
            //var hero = await _dataContext.SuperHeroes.FindAsync(id);
            //return (hero == null) ? BadRequest("Hero not found") : Ok(hero);
            var superHero = _repository.GetSuperHero(id);
            return superHero == null ? NotFound("Superhero not found") : Ok(superHero.AsDto());
        }

        // POST /superhero
        [HttpPost]
        public async Task<ActionResult<CreateSuperHeroDto>> AddHero(CreateSuperHeroDto heroDto)
        {
            //await _dataContext.SuperHeroes.AddAsync(hero);
            //await _dataContext.SaveChangesAsync();
            //return Ok(await _dataContext.SuperHeroes.ToListAsync());
            var heroList = _repository.GetSuperHeroes();
            SuperHero hero = new()
            {
                Id = heroList.Count() + 1,
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
        public async Task<ActionResult> UpdateHero(int id, UpdateSuperHeroDto superheroDto)
        {
            //  var dbHero = await _dataContext.SuperHeroes.FindAsync(request.Id);
            //  if (dbHero == null)
            //      return BadRequest("Hero not found");
            //
            //   dbHero.Name = request.Name;
            //   dbHero.FirstName = request.FirstName;
            //   dbHero.LastName = request.LastName;
            //   dbHero.Place = request.Place;
            //
            //   await _dataContext.SaveChangesAsync();
            //
            //   return Ok(await _dataContext.SuperHeroes.ToListAsync());

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
        public async Task<ActionResult> Delete(int id)
        {
            // var dbHero = await _dataContext.SuperHeroes.FindAsync(id);
            // if (dbHero == null)
            //    return BadRequest("Hero not found");
            //
            // _dataContext.SuperHeroes.Remove(dbHero);
            // await _dataContext.SaveChangesAsync();
            // return Ok(await _dataContext.SuperHeroes.ToListAsync());

            var existingHero = _repository.GetSuperHero(id);
            if (existingHero is null)
                return NotFound();

            _repository.DeleteSuperHero(id);
            return NoContent();

       }
    }
}
