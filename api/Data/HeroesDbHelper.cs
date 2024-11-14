using Microsoft.Data.Sqlite;
using TourOfHeroes.API.Models;

namespace TourOfHeroes.API.Data;

public static class HeroesDbHelper
{
    private static IList<Hero> _heroes = new List<Hero> {
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

    public static void SeedDatabase(string connectionString)
    {
        using (var connection = new SqliteConnection(connectionString))
        {
            connection.Open();

            var createTableQuery =
                @"
                    CREATE TABLE IF NOT EXISTS tbl_heroes (
                        id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        name TEXT NOT NULL
                    );
                ";

            ExecuteQuery(connection, createTableQuery);

            var seedHeroQuery =
                @"
                    INSERT OR IGNORE INTO tbl_heroes
                    VALUES ($id, $name)
                ";

            foreach (var hero in _heroes)
            {
                var parameters = new Dictionary<string, object> {
                    {"id", hero.Id},
                    {"name", hero.Name},
                };

                ExecuteQueryWithParameter(connection, seedHeroQuery, parameters);
            }
        }
    }

    public static SqliteDataReader ExecuteQuery(SqliteConnection connection, string query)
    {
        var command = connection.CreateCommand();

        command.CommandText = query;

        return command.ExecuteReader();
    }

    public static SqliteDataReader ExecuteQueryWithParameter(SqliteConnection connection, string query, IDictionary<string, object> parameters)
    {
        var command = connection.CreateCommand();

        command.CommandText = query;

        Console.WriteLine(query);

        foreach (var parameter in parameters)
        {
            Console.WriteLine($"Added param: [{parameter.Key}] {parameter.Value}");
            command.Parameters.AddWithValue($"${parameter.Key}", parameter.Value);
        }


        return command.ExecuteReader();
    }
}
