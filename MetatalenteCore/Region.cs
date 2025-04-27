namespace Metatalente.Core
{
    public readonly struct Region(Occur foraging, Occur wildlife, string name, string[] landscapes, string[] animals, string[] plants)
    {
        public int? ForagingMod { get; } = foraging == 0 ? null : (int)foraging;
        public int? WildlifeMod { get; } = wildlife == 0 ? null : (int)wildlife;
        public string ForagingString { get; } = Core.OccurToString((int)foraging);
        public string WildlifeString { get; } = Core.OccurToString((int)wildlife);
        public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
        public string[] Landscapes { get; } = landscapes ?? throw new ArgumentNullException(nameof(landscapes));
        public string[] Animals { get; } = animals ?? throw new ArgumentNullException(nameof(animals));
        public string[] Plants { get; } = plants ?? throw new ArgumentNullException(nameof(plants));

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
            foreach (Plant plant in plantsLandscape)
            {
                if (plant.Months.Contains(month))
                {
                    plantsData.Add(plant);
                }
            }
            return [.. plantsData];
        }
    }
}