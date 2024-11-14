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

            CreateCommand(connection, createTableQuery).ExecuteNonQuery();

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

                CreateCommand(connection, seedHeroQuery, parameters).ExecuteNonQuery();
            }
        }
    }

    public static SqliteCommand CreateCommand(SqliteConnection connection, string query, IDictionary<string, object>? parameters = null)
    {
        var command = connection.CreateCommand();

        command.CommandText = query;

        if (parameters is not null)
        {
            foreach (var parameter in parameters)
            {
                command.Parameters.AddWithValue($"${parameter.Key}", parameter.Value);
            }
        }

        return command;
    }
}
