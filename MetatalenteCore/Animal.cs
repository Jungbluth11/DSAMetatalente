namespace Metatalente.Core;

public readonly record struct Animal(int Difficulty, string Name, string[] Loot)
{
    public string Name { get; } = Name ?? throw new ArgumentNullException(nameof(Name));

    public string LootDisplayText
    {
        get
        {
            string text = Loot.Aggregate(string.Empty, (current, item) => current + (item + ", "));
            return text.TrimEnd(',');
        }
    }
    public string[] Loot { get; } = Loot ?? throw new ArgumentNullException(nameof(Loot));
}
