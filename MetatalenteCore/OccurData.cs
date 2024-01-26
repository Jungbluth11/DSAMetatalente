namespace Metatalente.Core
{
    public readonly struct OccurData
    {
        public int? Mod { get; }
        public string Occur { get; }
        public string Landscape { get; }

        public OccurData(Occur occur, Landscape landscape)
        {
            Mod = occur == 0 ? null : (int)occur;
            Occur = Core.OccurToString((int)occur);
            Landscape = landscape.Name;
        }
    }
}