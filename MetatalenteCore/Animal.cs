namespace Metatalente.Core
{
    public readonly struct Animal
    {
        public int Difficulty { get; }
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

        public Animal(int difficulty, string name, string[] loot)
        {
            Difficulty = difficulty;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Loot = loot ?? throw new ArgumentNullException(nameof(loot));
        }
    }
}