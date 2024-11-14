using TourOfHeroes.API.Models;

namespace TourOfHeroes.API.Services;

public interface IHeroesService
{
    public IList<Hero> GetHeroes(string name);
    public Hero? GetHero(int id);
    public Hero? UpdateHero(Hero hero, string name);
    public Hero AddHero(NewHeroDto newHeroDto);
    public bool DeleteHero(int id);
}
