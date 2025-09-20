namespace Metatalente.Core;

public readonly record struct Plant
{
    public int IdentificationMod { get; }
    public string Name { get; }
    public string LootDisplayText
    {
        get
        {
            string text = Loot.Aggregate(string.Empty, (current, item) => current + (item + ", "));
            return text.TrimEnd(',');
        }
    }
    public string[] Loot { get; }
    public string[] Months { get; }
    public OccurData[] OccurData { get; }

    public Plant(int identificationMod, string name, string[] loot, string[] months, string[] landscapes, Occur[] occur)
    {
        IdentificationMod = identificationMod;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Loot = loot ?? throw new ArgumentNullException(nameof(loot));
        Months = months ?? throw new ArgumentNullException(nameof(months));
        ArgumentNullException.ThrowIfNull(landscapes);
        ArgumentNullException.ThrowIfNull(occur);
        OccurData = [.. landscapes.Select((s, i) => new OccurData(occur[i], Core.GetLandscape(s)))];
    }
}
