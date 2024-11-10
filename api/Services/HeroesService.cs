using TourOfHeroes.API.Models;

namespace TourOfHeroes.API.Services;

public class HeroesService : IHeroesService
{
    private readonly IList<Hero> _heroes = new List<Hero> {
      new Hero(12, "Dr. Nice"),
      new Hero(13, "Bombasto"),
      new Hero(14, "Celeritas"),
      new Hero(15, "Magneta"),
      new Hero(16, "RubberMan"),
      new Hero(17, "Dynama"),
      new Hero(18, "Dr. IQ"),
      new Hero(19, "Magma"),
      new Hero(20, "Tornado")
    };

    public IList<Hero> GetHeroes(string searchTerm)
    {
        return _heroes.Where(h => h.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
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
