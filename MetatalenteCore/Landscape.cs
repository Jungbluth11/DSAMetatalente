namespace Metatalente.Core
{
    public readonly struct Landscape
    {
        public string Name { get; }
        public string? Terrain { get; }

        public Landscape(string name, string? terrain = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Terrain = terrain;
        }
    }
}