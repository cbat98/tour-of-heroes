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

        var parameters = new Dictionary<string, object> {
            { "searchTerm", $"%{name}%" }
        };

        using var connection = new SqliteConnection(_dataConfig.ConnectionString);
        connection.Open();

        var reader = HeroesDbHelper.CreateCommand(connection, query, parameters).ExecuteReader();

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
        var query =
            @"
                SELECT id, name
                FROM tbl_heroes
                WHERE id = $id
            ";

        var parameters = new Dictionary<string, object> {
            { "id", id }
        };

        using var connection = new SqliteConnection(_dataConfig.ConnectionString);
        connection.Open();

        var reader = HeroesDbHelper.CreateCommand(connection, query, parameters).ExecuteReader();

        if (reader.HasRows)
        {
            reader.Read();
            var heroId = reader.GetInt32(0);
            var heroName = reader.GetString(1);

            return new Hero(heroId, heroName);
        }

        return null;
    }

    public Hero? UpdateHero(Hero hero, string name)
    {
        var query =
            @"
                UPDATE tbl_heroes
                SET name = $name
                WHERE id = $id
            ";

        var parameters = new Dictionary<string, object> {
            { "id", hero.Id },
            { "name", name }
        };

        using var connection = new SqliteConnection(_dataConfig.ConnectionString);
        connection.Open();

        var rowsChanged = HeroesDbHelper.CreateCommand(connection, query, parameters).ExecuteNonQuery();

        return rowsChanged > 0 ? new Hero(hero.Id, name) : null;
    }

    public Hero AddHero(NewHeroDto newHero)
    {
        var query =
            @"
                INSERT INTO tbl_heroes (name)
                VALUES ($name)
            ";

        var parameters = new Dictionary<string, object> {
            { "name", newHero.Name }
        };

        using var connection = new SqliteConnection(_dataConfig.ConnectionString);
        connection.Open();

        HeroesDbHelper.CreateCommand(connection, query, parameters).ExecuteReader();

        query =
            @"
                SELECT id, name
                FROM tbl_heroes
                WHERE id = last_insert_rowid()
            ";

        var reader = HeroesDbHelper.CreateCommand(connection, query).ExecuteReader();

        reader.Read();

        var heroId = reader.GetInt32(0);
        var heroName = reader.GetString(1);

        return new Hero(heroId, heroName);
    }

    public bool DeleteHero(int id)
    {
        var query =
            @"
                DELETE FROM tbl_heroes
                WHERE id = $id
            ";

        var parameters = new Dictionary<string, object> {
            { "id", id }
        };

        using var connection = new SqliteConnection(_dataConfig.ConnectionString);
        connection.Open();

        var rowsChanged = HeroesDbHelper.CreateCommand(connection, query, parameters).ExecuteNonQuery();

        return rowsChanged > 0;
    }
}
