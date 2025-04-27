namespace Metatalente.Core
{
    public readonly struct Landscape(string name, string? terrain = null)
    {
        public string Name { get; } = name ?? throw new ArgumentNullException(nameof(name));
        public string? Terrain { get; } = terrain;
    }
}