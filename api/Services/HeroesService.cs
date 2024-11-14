using TourOfHeroes.API.Data;
using TourOfHeroes.API.Models;

namespace TourOfHeroes.API.Services;

public class HeroesService : IHeroesService
{
    private readonly IHeroesData _heroesData;

    public HeroesService(IHeroesData heroesData)
    {
        _heroesData = heroesData;
    }

    public Hero AddHero(NewHeroDto newHero)
    {
        return _heroesData.AddHero(newHero);
    }

    public bool DeleteHero(int id)
    {
        return _heroesData.DeleteHero(id);
    }

    public Hero? GetHero(int id)
    {
        return _heroesData.GetHero(id);
    }

    public IList<Hero> GetHeroes(string name)
    {
        return _heroesData.GetHeroes(name);
    }

    public Hero? UpdateHero(Hero hero, string name)
    {
        return _heroesData.UpdateHero(hero, name);
    }
}
