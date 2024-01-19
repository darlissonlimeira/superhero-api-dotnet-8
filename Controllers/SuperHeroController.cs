using Microsoft.AspNetCore.Mvc;
using SuperHeroApi.Dto;
using SuperHeroApi.Entities;
using SuperHeroApi.Services;

namespace SuperHeroApi.Controllers
{
    [ApiController]
    [Route("/api/superheroes")]
    public class SuperHeroController : ControllerBase
    {
        private readonly SuperHeroesService _superHeroService;

        public SuperHeroController(SuperHeroesService service)
        {
            _superHeroService = service;
        }

        [HttpGet]
        [ProducesResponseType<List<SuperHero>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var superHeroes = await _superHeroService.FindAll();
            return Ok(new { superheroes = superHeroes });
        }

        [HttpGet("{id}")]
        [ProducesResponseType<SuperHero>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var superHero = await _superHeroService.FindById(id);

            if (superHero is null) return NotFound(new { error = "Superhero not found." });

            return Ok(new { superhero = superHero });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(SuperHeroDTO request)
        {
            await _superHeroService.Create(request);
            return Created();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _superHeroService.Remove(id);

            if (result is null) return BadRequest();

            return Ok();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, SuperHeroDTO request)
        {
            var result = await _superHeroService.Update(id, request);

            if (result is null) return BadRequest();

            return Ok();
        }

    }
}