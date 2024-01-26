namespace Metatalente.Core
{
    public readonly struct Plant
    {
        public int IdentificationMod { get; }
        public string Name { get; }
        public string LootDisplayText
        {
            get
            {
                string text = string.Empty;
                foreach (string item in Loot)
                {
                    text += item + ", ";
                }
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
            if (landscapes is null)
            {
                throw new ArgumentNullException(nameof(landscapes));
            }

            if (occur is null)
            {
                throw new ArgumentNullException(nameof(occur));
            }
            List<OccurData> occurData = new();
            for (int i = 0; i < landscapes.Length; i++)
            {
                occurData.Add(new OccurData(occur[i], Core.GetLandscape(landscapes[i])));
            }
            OccurData = occurData.ToArray();
        }
    }
}