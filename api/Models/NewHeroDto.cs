namespace TourOfHeroes.API.Models;

public class NewHeroDto
{
    public string Name { get; init; } = string.Empty;

    public NewHeroDto(string name) {
        Name = name;

        if (string.IsNullOrWhiteSpace(Name))
        {
            throw new InvalidOperationException($"Unable to create new hero: {name}");
        }
    }
}
