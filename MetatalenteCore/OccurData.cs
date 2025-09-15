namespace DSAMetatalente.Core;

public readonly struct OccurData(Occur occur, Landscape landscape)
{
    public int? Mod { get; } = occur == 0 ? null : (int)occur;
    public string Occur { get; } = Core.OccurToString((int)occur);
    public string Landscape { get; } = landscape.Name;
}
