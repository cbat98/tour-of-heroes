using Microsoft.AspNetCore.Mvc;
using TourOfHeroes.API.Models;
using TourOfHeroes.API.Services;

namespace TourOfHeroes.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HeroesController : ControllerBase
{
    private readonly IHeroesService _heroesService;

    public HeroesController(IHeroesService heroesService)
    {
        _heroesService = heroesService;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IList<Hero>>> GetHeroes(string name = "")
    {
        return Ok(await _heroesService.GetHeroesAsync(name));
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Hero>> GetHero(int id)
    {
        var hero = await _heroesService.GetHeroAsync(id);

        return (hero is not null) ? Ok(hero) : NotFound();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Hero>> UpdateHero(int id, NewHeroDto newHeroDto)
    {
        var hero = await _heroesService.GetHeroAsync(id);

        if (hero is null)
        {
            return NotFound();
        }

        var newHero = await _heroesService.UpdateHeroAsync(hero, newHeroDto.Name);

        return (newHero is not null) ? Ok(newHero) : Conflict();
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Hero>> AddHero(NewHeroDto newHeroDto)
    {
        var hero = await _heroesService.AddHeroAsync(newHeroDto);

        if (hero is null) {
            return Conflict();
        }

        return Ok(hero);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Hero>> DeleteHero(int id)
    {
        var deleted = await _heroesService.DeleteHeroAsync(id);

        return (deleted) ? Ok() : NotFound();
    }
}
