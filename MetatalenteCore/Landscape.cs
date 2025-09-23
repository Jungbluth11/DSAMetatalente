namespace Metatalente.Core;

public readonly record struct Landscape(string Name, string? Terrain = null)
{
    public string Name { get; } = Name ?? throw new ArgumentNullException(nameof(Name));
    public string? Terrain { get; } = Terrain;

}
