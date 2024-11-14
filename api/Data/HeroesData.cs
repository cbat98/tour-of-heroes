using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using TourOfHeroes.API.Models;

namespace TourOfHeroes.API.Data;

public class HeroesData : IHeroesData
{
    private readonly IList<Hero> _heroes = new List<Hero> { };

    private readonly DataConfig _dataConfig;

    public HeroesData(IOptions<DataConfig> dataConfig)
    {
        _dataConfig = dataConfig.Value;

        HeroesDbHelper.SeedDatabase(_dataConfig.ConnectionString);
    }

    public IList<Hero> GetHeroes(string name)
    {
        return _heroes.Where(h => h.Name.ToLower().Contains(name.ToLower())).ToList();
    }

    public Hero? GetHero(int id)
    {
        return _heroes.FirstOrDefault(h => h.Id == id);
    }

    public Hero? UpdateHero(Hero hero, string name)
    {
        var success = hero.SetName(name);

        return (success) ? hero : null;
    }

    public Hero AddHero(NewHeroDto newHero)
    {
        var id = _heroes.Max(h => h.Id) + 1;
        var hero = new Hero(id, newHero.Name);

        _heroes.Add(hero);

        return hero;
    }

    public bool DeleteHero(int id)
    {
        var hero = _heroes.FirstOrDefault(h => h.Id == id);

        return (hero is not null) ? _heroes.Remove(hero) : false;
    }
}
