namespace TourOfHeroes.API.Models;

public class Hero
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;

    public Hero(int id, string name)
    {
        SetId(id);
        SetName(name);

        if (!ValidateHero())
        {
            throw new InvalidOperationException($"Unable to create hero: {id}:{name}");
        }
    }

    public bool SetId(int id)
    {
        Id = id;

        return ValidateHero();
    }

    public bool SetName(string name)
    {
        Name = name;

        return ValidateHero();
    }

    private bool ValidateHero()
    {
        if (Id <= 0)
        {
            return false;
        }

        if (string.IsNullOrWhiteSpace(Name))
        {
            return false;
        }

        return true;
    }
}
