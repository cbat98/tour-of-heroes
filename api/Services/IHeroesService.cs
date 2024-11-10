using TourOfHeroes.API.Models;

namespace TourOfHeroes.API.Services;

public interface IHeroesService
{
    public IList<Hero> GetHeroes(string searchTerm);
    public Hero? GetHero(int id);
    public Hero? UpdateHero(Hero hero, string name);
    public Hero AddHero(NewHeroDto newHero);
    public bool DeleteHero(int id);
}
