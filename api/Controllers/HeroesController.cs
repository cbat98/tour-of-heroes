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
    public IList<Hero> GetHeroes(string name = "")
    {
        return _heroesService.GetHeroes(name);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Hero> GetHero(int id)
    {
        var hero = _heroesService.GetHero(id);

        return (hero is not null) ? Ok(hero) : NotFound();
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<Hero> UpdateHero(Hero newHero)
    {
        var hero = _heroesService.GetHero(newHero.Id);

        if (hero is null)
        {
            return NotFound();
        }

        hero = _heroesService.UpdateHero(hero, newHero.Name);

        return (hero is not null) ? Ok() : NotFound();
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Hero> AddHero(NewHeroDto newHero)
    {
        var hero = _heroesService.AddHero(newHero);

        return Ok(hero);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Hero> DeleteHero(int id)
    {
        var deleted = _heroesService.DeleteHero(id);

        return (deleted) ? Ok() : NotFound();
    }
}
