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
        var query =
            @"
                SELECT id, name
                FROM tbl_heroes
                WHERE name LIKE $searchTerm;
            ";

        using var connection = new SqliteConnection(_dataConfig.ConnectionString);
        connection.Open();

        var parameters = new Dictionary<string, object> {
            { "searchTerm", $"%{name}%" }
        };

        var reader = HeroesDbHelper.ExecuteQueryWithParameter(connection, query, parameters);

        var heroes = new List<Hero>();

        while (reader.Read())
        {
            var heroId = reader.GetInt32(0);
            var heroName = reader.GetString(1);

            heroes.Add(new Hero(heroId, heroName));
        }

        return heroes;
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
