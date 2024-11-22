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

    public async Task<Hero> AddHeroAsync(NewHeroDto newHeroDto)
    {
        return await _heroesData.AddHeroAsync(newHeroDto);
    }

    public async Task<bool> DeleteHeroAsync(int id)
    {
        return await _heroesData.DeleteHeroAsync(id);
    }

    public async Task<Hero?> GetHeroAsync(int id)
    {
        return await _heroesData.GetHeroAsync(id);
    }

    public async Task<IList<Hero>> GetHeroesAsync(string name)
    {
        return await _heroesData.GetHeroesAsync(name);
    }

    public async Task<Hero?> UpdateHeroAsync(Hero hero, string name)
    {
        return await _heroesData.UpdateHeroAsync(hero, name);
    }
}
