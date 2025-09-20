namespace Metatalente.Core;

public readonly record struct Region(Occur Foraging, Occur Wildlife, string Name, string[] Landscapes, string[] Animals, string[] Plants)
{
    public int? ForagingMod { get; } = Foraging == 0 ? null : (int)Foraging;
    public int? WildlifeMod { get; } = Wildlife == 0 ? null : (int)Wildlife;
    public string ForagingString { get; } = Core.OccurToString((int)Foraging);
    public string WildlifeString { get; } = Core.OccurToString((int)Wildlife);
    public string Name { get; } = Name ?? throw new ArgumentNullException(nameof(Name));
    public string[] Landscapes { get; } = Landscapes ?? throw new ArgumentNullException(nameof(Landscapes));
    public string[] Animals { get; } = Animals ?? throw new ArgumentNullException(nameof(Animals));
    public string[] Plants { get; } = Plants ?? throw new ArgumentNullException(nameof(Plants));

    public Plant[] GetPossiblePlants(string landscape, string month)
    {
        List<Plant> plantsLandscape = [];

        foreach (string plant in Plants)
        {
            Plant plantEntry = PlantSeeking.Plants.SingleOrDefault(p => p.Name == plant && p.OccurData.SingleOrDefault(o => o.Landscape == landscape).Landscape == landscape);

            if (plantEntry.Name != null)
            {
                plantsLandscape.Add(plantEntry);
            }
        }

        List<Plant> plantsData = [];
        plantsData.AddRange(plantsLandscape.Where(plant => plant.Months.Contains(month)));

        return [.. plantsData];
    }
}
