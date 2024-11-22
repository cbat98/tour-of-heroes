using TourOfHeroes.API.Models;

namespace TourOfHeroes.API.Data;

public interface IHeroesData
{
    public Task<IList<Hero>> GetHeroesAsync(string name);
    public Task<Hero?> GetHeroAsync(int id);
    public Task<Hero?> UpdateHeroAsync(Hero hero, string name);
    public Task<Hero> AddHeroAsync(NewHeroDto newHeroDto);
    public Task<bool> DeleteHeroAsync(int id);
}
