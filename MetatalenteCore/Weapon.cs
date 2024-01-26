namespace Metatalente.Core
{
    public struct Weapon
    {
        public int MaxRange { get; }
        public string Name { get; }
        public string UsedSkill { get; }

        public Weapon(string name, string usedSkill, int maxRange)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            UsedSkill = usedSkill ?? throw new ArgumentNullException(nameof(usedSkill));
            MaxRange = maxRange;
            if (maxRange <= 0)
            {
                throw new ArgumentException(nameof(maxRange) + "can't be 0 or less");
            }
        }
    }
}