namespace Metatalente.Core
{
    public readonly struct Region
    {
        public int? ForagingMod { get; }
        public int? WildlifeMod { get; }
        public string ForagingString { get; }
        public string WildlifeString { get; }
        public string Name { get; }
        public string[] Landscapes { get; }
        public string[] Animals { get; }
        public string[] Plants { get; }

        public Region(Occur foraging, Occur wildlife, string name, string[] landscapes, string[] animals, string[] plants)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Landscapes = landscapes ?? throw new ArgumentNullException(nameof(landscapes));
            Animals = animals ?? throw new ArgumentNullException(nameof(animals));
            Plants = plants ?? throw new ArgumentNullException(nameof(plants));
            ForagingMod = foraging == 0 ? null : (int)foraging;
            WildlifeMod = wildlife == 0 ? null : (int)wildlife;
            ForagingString = Core.OccurToString((int)foraging);
            WildlifeString = Core.OccurToString((int)wildlife);
        }

        public Plant[] GetPossiblePlants(string landscape, string month)
        {
            List<Plant> plantsLandscape = new();
            foreach (string plant in Plants)
            {
                Plant plantEntry = PlantSeeking.Plants.SingleOrDefault(p => p.Name == plant && p.OccurData.SingleOrDefault(o => o.Landscape == landscape).Landscape == landscape);
                if (plantEntry.Name != null)
                {
                    plantsLandscape.Add(plantEntry);
                }
            }
            List<Plant> plantsData = new();
            foreach (Plant plant in plantsLandscape)
            {
                if (plant.Months.Contains(month))
                {
                    plantsData.Add(plant);
                }
            }
            return plantsData.ToArray();
        }
    }
}