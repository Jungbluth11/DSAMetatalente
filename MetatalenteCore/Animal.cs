namespace Metatalente.Core
{
    public readonly struct Animal(int difficulty, string name, string[] loot)
    {
        public int Difficulty { get; } = difficulty;
        public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
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
        public string[] Loot { get; } = loot ?? throw new ArgumentNullException(nameof(loot));
    }
}