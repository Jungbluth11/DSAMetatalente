using DSAUtils.HeldentoolInterop;
using DSAUtils.Settings.Aventurien;

namespace Metatalente.Core
{
    public class Core
    {
        internal Charakter? character;
        private readonly List<string> _knownTerrains = new();
        public int MU { get; set; } = 8;
        public int IN { get; set; } = 8;
        public int GE { get; set; } = 8;
        public int FF { get; set; } = 8;
        public int SkillWildnisleben { get; set; }
        public int SkillSinnenschaerfe { get; set; }
        public int SkillPflanzenkunde { get; set; }
        public int SkillTierkunde { get; set; }
        public int SkillFaehrtensuchen { get; set; }
        public int SkillSchleichen { get; set; }
        public int SkillSichVerstecken { get; set; }
        public int SkillWeapon { get; set; }
        public string[] Terrains => Sonderfertigkeiten.GetByGroup(Sonderfertigkeitengruppe.Gelaendekunde);
        public string[] KnownTerrains => _knownTerrains.ToArray();
        public string CurrentMonth { get; set; } = Months[0];
        public bool IsHeldenToolInstalled => HeldentoolInterop.IsInstalled();
        public bool IsKnownTerrain { get; set; }
        public Region CurrentRegion { get; set; } = Regions[0];
        public Landscape CurrentLandscape { get; set; } = Landscapes[0];

        #region hard coded data

        public static string[] Months => new string[]
        {
            "Praios",
            "Rondra",
            "Efferd",
            "Travia",
            "Boron",
            "Hesinde",
            "Firun",
            "Tsa",
            "Phex",
            "Peraine",
            "Ingerimm",
            "Rahja",
            "Namenlose Tage"
        };
        public static Region[] Regions => new Region[]
        {
            new(Occur.None, Occur.VeryRare, "Ewiges Eis", new string[]{ "Eis"}, new string[]{ "Felsrobbe", "Firnyak", "Firnluchs", "Firunsbär", "Mammut", "Mastodon", "Meerkalb", "Schneedachs", "Seetiger"}, new string[]{ "Alveranie", "Blutblatt", "Rattenpilz"}),
            new(Occur.Rare, Occur.Modest, "Nördliche Tundra", new string[]{ "Eis", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche"}, new string[]{ "Felsrobbe", "Karen", "Pfeifhase", "Elch", "Firnluchs", "Firnyak", "Schneedachs", "Blaufuchs", "Firunsbär", "Mammut", "Mastodon", "Meerkalb", "Seetiger", "Steppenhund", "Steppentiger"}, new string[]{ "Alveranie", "Blutblatt", "Kairan", "Bunter Mohn", "Rattenpilz", "Talaschin", "Vierblättrige Einbeere", "Wirselkraut"}),
            new(Occur.Modest, Occur.Modest, "Nördliche Grasländer und Steppen", new string[]{ "Hochland", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new string[]{ "Karen", "Karnickel", "Steppenhund", "Gelbfuchs", "Pfeifhase", "Rebhuhn", "Rotpüschel", "Elch", "Firunshirsch", "Klippechse", "Rotluchs", "Schneedachs", "Steppenrind", "Trappe", "Blaufuchs", "Borkenbär", "Firnluchs", "Halmar-Antilope", "Mammut", "Orklandbär", "Steppentiger", "Vielfraß"}, new string[]{ "Alraune", "Alveranie", "Blutblatt", "Donf", "Egelschreck", "Eitriger Krötenschemel", "Grüne Schleimschlange", "Gulmond", "Joruga", "Kairan", "Klippenzahn", "Madablüte", "Messergras", "Bunter Mohn", "Naftanstaude", "Orklandbovist", "Rattenpilz", "Roter Drachenschlund", "Shurinstrauch", "Tarnele", "Thonnys", "Tigermohn", "Traschbart", "Vierblättrige Einbeere", "Wirselkraut", "Zwölfblatt"}),
            new(Occur.Modest, Occur.Modest, "Nördliches Hochland", new string[]{ "Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new string[]{ "Orklandkarnickel", "Rebhuhn", "Gelbfuchs", "Halmar-Antilope", "Rotpüschel", "Trappe", "Klippechse", "Orklandbär", "Rotfuchs", "Rotluchs", "Schreckkatze", "Sonnenluchs", "Borkenbär", "Mammut", "Riesenaffe", "Steppentiger"}, new string[]{ "Alraune", "Alveranie", "Basilamine", "Blutblatt", "Eitriger Krötenschemel", "Grüne Schleimschlange", "Gulmond", "Kairan", "Klippenzahn", "Messergras", "Bunter Mohn", "Naftanstaude", "Orklandbovist", "Rattenpilz", "Shurinstrauch", "Tarnele", "Traschbart", "Vierblättrige Einbeere", "Wirselkraut", "Zwölfblatt"}),
            new(Occur.Rare, Occur.Rare, "Kalkgebirge", new string[]{ "Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Wald", "Waldrand", "Höhle (feucht)", "Höhle (trocken)"}, new string[]{ "Gebirgsbock", "Karnickel", "Berglöwe", "Höhlenbär", "Rotluchs", "Sonnenluchs"}, new string[]{ "Alveranie", "Atan-Kiefer", "Blutblatt", "Eitriger Krötenschemel", "Feuermoos und Efferdmoos", "Gulmond", "Joruga", "Kairan", "Madablüte", "Bleichmohn (Weißer Mohn)", "Grauer Mohn", "Nothilf", "Phosphorpilz", "Rattenpilz", "Talaschin", "Tarnele", "Thonnys", "Traschbart", "Vierblättrige Einbeere", "Vragieswurzel", "Wirselkraut", "Zwölfblatt"}),
            new(Occur.Rare, Occur.Rare, "Andere Mittelländische Gebirge", new string[]{ "Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Wald", "Waldrand", "Höhle (feucht)", "Höhle (trocken)"}, new string[]{ "Gebirgsbock", "Karnickel", "Pfeifhase", "Riesenlöffler", "Berglöwe", "Sonnenluchs", "Höhlenbär", "Rotluchs"}, new string[]{ "Alveranie", "Blutblatt", "Eitriger Krötenschemel", "Feuermoos und Efferdmoos", "Gulmond", "Joruga", "Kairan", "Madablüte", "Bleichmohn (Weißer Mohn)", "Grauer Mohn", "Nothilf", "Phosphorpilz", "Rattenpilz", "Schleimiger Sumpfknöterich", "Talaschin", "Tarnele", "Thonnys", "Traschbart", "Vierblättrige Einbeere", "Vragieswurzel", "Wirselkraut", "Zwölfblatt"}),
            new(Occur.Common, Occur.Common, "Nördliche Wälder (Westküste)", new string[]{ "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new string[]{ "Karnickel", "Rebhuhn", "Wildschwein", "Auerhahn", "Rehwild", "Streifendachs", "Fasan", "Kronenhirsch", "Pfeifhase", "Rotfuchs", "Rotluchs", "Rotpüschel", "Auerochse", "Baumbär", "Blaufuchs", "Borkenbär", "Vielfraß", "Waldlöwe", "Wildkatze", "Höhlenbär", "Riesenaffe", "Silberlöwe"}, new string[]{ "Alraune", "Alveranie", "Basilamine", "Belmart", "Blutblatt", "Carlog", "Efeuer", "Eitriger Krötenschemel", "Gulmond", "Hollbeere", "Joruga", "Kairan", "Klippenzahn", "Mibelrohr", "Orklandbovist", "Pestsporenpilz", "Rattenpilz", "Roter Drachenschlund", "Shurinstrauch", "Tarnele", "Thonnys", "Traschbart", "Ulmenwürger", "Vierblättrige Einbeere", "Waldwebe", "Wirselkraut", "Zunderschwamm", "Zwölfblatt"}),
            new(Occur.Common, Occur.VeryCommon, "Nördliche Wälder (Taiga)", new string[]{ "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new string[]{ "Karnickel", "Wildschwein", "Fasan", "Pfeifhase", "Rebhuhn", "Rehwild", "Elch", "Rotluchs", "Schneedachs", "Wildkatze", "Blaufuchs", "Firunshirsch", "Karen", "Kronenhirsch", "Schwarzbär", "Sonnenluchs", "Streifendachs", "Vielfraß", "Waldlöwe", "Auerochse", "Borkenbär", "Rotfuchs", "Silberlöwe"}, new string[]{ "Alraune", "Alveranie", "Belmart", "Blutblatt", "Efeuer", "Eitriger Krötenschemel", "Gulmond", "Joruga", "Kairan", "Nothilf", "Pestsporenpilz", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Schleimiger Sumpfknöterich", "Shurinstrauch", "Tarnele", "Thonnys", "Traschbart", "Ulmenwürger", "Vierblättrige Einbeere", "Waldwebe", "Wirselkraut", "Zunderschwamm", "Zwölfblatt"}),
            new(Occur.Common, Occur.VeryCommon, "Nördliche Wälder (Bornland)", new string[]{ "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new string[]{ "Pfeifhase", "Rehwild", "Silberbock", "Wildschwein", "Blaufuchs", "Elch", "Kronenhirsch", "Rebhuhn", "Sonnenluchs", "Streifendachs", "Sumpfranze", "Auerochse", "Firunshirsch", "Höhlenbär", "Riesenaffe", "Rotluchs", "Schneedachs", "Schwarzbär", "Vielfraß", "Waldlöwe", "Wildkatze"}, new string[]{ "Alraune", "Alveranie", "Belmart", "Blutblatt", "Efeuer", "Eitriger Krötenschemel", "Gulmond", "Joruga", "Kairan", "Nothilf", "Pestsporenpilz", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Schleimiger Sumpfknöterich", "Tarnele", "Thonnys", "Ulmenwürger", "Vierblättrige Einbeere", "Waldwebe", "Wasserrausch", "Wirselkraut", "Zunderschwamm", "Zwölfblatt"}),
            new(Occur.Modest, Occur.Rare, "Nördliche Sümpfe, Marschen und Moore", new string[]{ "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor", "Waldrand"}, new string[]{ "Karnickel", "Klippechse", "Sumpfranze"}, new string[]{ "Alraune", "Alveranie", "Blutblatt", "Carlog", "Donf", "Egelschreck", "Eitriger Krötenschemel", "Grüne Schleimschlange", "Iribaarslilie", "Kairan", "Mibelrohr", "Morgendornstrauch", "Neckerkraut", "Pestsporenpilz", "Rahjalieb", "Rattenpilz", "Schleimiger Sumpfknöterich", "Schlinggras", "Tarnele", "Traschbart", "Vierblättrige Einbeere", "Wirselkraut", "Zwölfblatt"})
            ,new(Occur.Common, Occur.Common, "Mittelländische Grasländer, Heide und Steppe", new string[]{ "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new string[]{ "Pfeifhase", "Rotpüschel", "Gänseluchs", "Rebhuhn", "Trappe", "Fasan", "Gelbfuchs", "Gabelantilope", "Sonnenluchs", "Wildkatze", "Steppentiger"}, new string[]{ "Alraune", "Alveranie", "Blutblatt", "Chonchinis", "Donf", "Egelschreck", "Eitriger Krötenschemel", "Gulmond", "Hiradwurz", "Ilmenblatt", "Joruga", "Kairan", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Lulanie", "Messergras", "Mibelrohr", "Mirbelstein", "Bunter Mohn", "Purpurmohn", "Schwarzer Mohn", "Tigermohn", "Naftanstaude", "Neckerkraut", "Rahjalieb", "Rattenpilz", "Roter Drachenschlund", "Schlangenzünglein", "Schwarmschwamm", "Shurinstrauch", "Tarnele", "Vierblättrige Einbeere", "Winselgras", "Wirselkraut", "Zwölfblatt"}),
            new(Occur.Common, Occur.Common, "Mittelländische Wälder (Gemäßigtes Klima)", new string[]{ "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new string[]{ "Karnickel", "Rehwild", "Riesenlöffler", "Wildschwein", "Gänseluchs", "Pfeifhase", "Rebhuhn", "Rotpüschel", "Auerhahn", "Fasan", "Kronenhirsch", "Rotfuchs", "Streifendachs", "Auerochse", "Baumbär", "Höhlenbär", "Rotluchs", "Schwarzbär", "Silberbock", "Wildkatze", "Silberlöwe"}, new string[]{ "Alraune", "Alveranie", "Belmart", "Blutblatt", "Carlog", "Chonchinis", "Efeuer", "Egelschreck", "Eitriger Krötenschemel", "Gulmond", "Ilmenblatt", "Joruga", "Kairan", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Lulanie", "Mibelrohr", "Mirbelstein", "Neckerkraut", "Quasselwurz", "Rahjalieb", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Schleimiger Sumpfknöterich", "Shurinstrauch", "Tarnele", "Traschbart", "Ulmenwürger", "Vierblättrige Einbeere", "Vragieswurzel", "Waldwebe", "Wirselkraut", "Zunderschwamm", "Zwölfblatt"}),
            new(Occur.Common, Occur.Common, "Mittelländische Wälder (Yaquirisches Klima)", new string[]{ "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new string[]{ "Karnickel", "Rehwild", "Riesenlöffler", "Rotfuchs", "Rotpüschel", "Wildschwein", "Gänseluchs", "Rebhuhn", "Regenbogenfasan", "Rotluchs", "Baumbär", "Raschtulsluchs", "Streifendachs", "Wildkatze"}, new string[]{ "Alraune", "Alveranie", "Arganstrauch", "Belmart", "Blutblatt", "Boronsschlinge", "Carlog", "Chonchinis", "Efeuer", "Egelschreck", "Eitriger Krötenschemel", "Ilmenblatt", "Joruga", "Kairan", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Lulanie", "Mibelrohr", "Purpurmohn", "Schwarzer Mohn", "Neckerkraut", "Quasselwurz", "Rahjalieb", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Shurinstrauch", "Tarnele", "Traschbart", "Ulmenwürger", "Vierblättrige Einbeere", "Vragieswurzel", "Waldwebe", "Zunderschwamm", "Zwölfblatt"}),
            new(Occur.Common, Occur.Modest, "Mittelländische Wälder (Tobrisches Klima)", new string[]{ "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new string[]{ "Rehwild", "Riesenlöffler", "Rotpüschel", "Auerhahn", "Fasan", "Rebhuhn", "Rotfuchs", "Wildkatze", "Wildschwein", "Auerochse", "Baumbär", "Riesenaffe"}, new string[]{ "Alraune", "Alveranie", "Belmart", "Blutblatt", "Chonchinis", "Efeuer", "Egelschreck", "Eitriger Krötenschemel", "Gulmond", "Ilmenblatt", "Joruga", "Kairan", "Mirbelstein", "Purpurmohn", "Quasselwurz", "Rahjalieb", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Schwarmschwamm", "Tarnele", "Traschbart", "Tuur-Amash-Kelch", "Ulmenwürger", "Vierblättrige Einbeere", "Waldwebe", "Wasserrausch", "Zunderschwamm", "Zwölfblatt"}),
            new(Occur.Common, Occur.Common, "Immergrüne Wälder (Südosten)", new string[]{ "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new string[]{ "Rehwild", "Riesenlöffler", "Rotpüschel", "Auerhahn", "Ongalobulle", "Raschtulsluchs", "Rebhuhn", "Regenbogenfasan", "Rotfuchs", "Warzenschwein", "Al'Kebir-Antilope", "Baumbär", "Springbock", "Wildkatze", "Moosaffe", "Riesenaffe"}, new string[]{ "Alraune", "Alveranie", "Blutblatt", "Chonchinis", "Dornrose", "Efeuer", "Egelschreck", "Eitriger Krötenschemel", "Ilmenblatt", "Joruga", "Kairan", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Purpurmohn", "Quasselwurz", "Rahjalieb", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Schwarzer Wein", "Shurinstrauch", "Tarnele", "Traschbart", "Waldwebe", "Zunderschwamm", "Zwölfblatt"}),
            new(Occur.Common, Occur.Modest, "Südländische Grasländer und Steppen", new string[]{ "Wüste und Wüstenrand", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new string[]{ "Al'Kebir-Antilope", "Gabelantilope", "Springbock", "Khômgepard", "Ongalobulle", "Raschtulsluchs", "Strauß", "Warzenschwein", "Fasan", "Gelbfuchs", "Regenbogenfasan", "Rehwild", "Rotpüschel", "Sandlöwe"}, new string[]{ "Alraune", "Alveranie", "Atmon", "Blutblatt", "Chonchinis", "Dornrose", "Eitriger Krötenschemel", "Finage", "Hiradwurz", "Jagdgras", "Kairan", "Khôm- oder Mhanadiknolle", "Menchal-Kaktus", "Merach-Strauch", "Messergras", "Mirbelstein", "Bunter Mohn", "Purpurmohn", "Naftanstaude", "Olginwurz", "Rattenpilz", "Schlangenzünglein", "Schwarzer Wein", "Shurinstrauch", "Talaschin", "Tarnele", "Winselgras", "Wirselkraut", "Yaganstrauch", "Zithabar", "Zwölfblatt"}),
            new(Occur.Rare, Occur.Rare, "Wüstenrandgebiete", new string[]{ "Wüste und Wüstenrand", "Steppe", "Grasland, Wiesen", "Wald", "Waldrand"}, new string[]{ "Gabelantilope", "Springbock", "Al'Kebir-Antilope", "Rotpüschel", "Khômgepard", "Sandlöwe", "Strauß", "Raschtulsluchs"}, new string[]{ "Alveranie", "Atmon", "Blutblatt", "Cheria-Kaktus", "Chonchinis", "Eitriger Krötenschemel", "Finage", "Hiradwurz", "Khôm- oder Mhanadiknolle", "Menchal-Kaktus", "Merach-Strauch", "Messergras", "Purpurmohn", "Naftanstaude", "Olginwurz", "Rattenpilz", "Shurinstrauch", "Talaschin", "Tarnele", "Winselgras", "Wirselkraut", "Zwölfblatt"}),
            new(Occur.VeryRare, Occur.VeryRare, "Wüste", new string[]{ "Wüste und Wüstenrand"}, new string[]{ "Sandlöwe"}, new string[]{ "Alveranie", "Blutblatt", "Cheria-Kaktus", "Khôm- oder Mhanadiknolle", "Menchal-Kaktus", "Rattenpilz", "Talaschin"}),
            new(Occur.Rare, Occur.Rare, "Südländische Gebirge", new string[]{ "Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Wald", "Waldrand"}, new string[]{ "Raschtulsluchs"}, new string[]{ "Alveranie", "Atmon", "Blutblatt", "Eitriger Krötenschemel", "Kairan", "Bleichmohn (Weißer Mohn)", "Olginwurz", "Rattenpilz", "Talaschin", "Tarnele", "Vierblättrige Einbeere", "Wirselkraut"}),
            new(Occur.Modest, Occur.Common, "Maraskan", new string[]{ "Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor", "Regenwald", "Wald", "Waldrand"}, new string[]{ "Riesenlöffler", "Otan-Otan", "Rotpüschel", "Vy'Tagga-Antilope", "Wildschwein", "Maraskanisches Stachelschwein", "Baumschleimer", "Baumwürger", "Rehwild", "Riesenaffe"}, new string[]{ "Alraune", "Alveranie", "Axorda-Baum", "Blutblatt", "Disdychonda", "Eitriger Krötenschemel", "Horusche", "Jagdgras", "Kairan", "Rattenpilz", "Rauschgurke", "Schlangenzünglein", "Shurinstrauch", "Tarnblatt", "Tarnele", "Trichterwurzel", "Wirselkraut", "Yaganstrauch"}),
            new(Occur.Rare, Occur.Rare, "Südliche Sümpfe", new string[]{ "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Küste, Strand", "Flussauen", "Sumpf und Moor", "Wald", "Waldrand"}, new string[]{ "Sumpfranze"}, new string[]{ "Alveranie", "Arganstrauch", "Atmon", "Blutblatt", "Carlog", "Chonchinis", "Disdychonda", "Donf", "Egelschreck", "Eitriger Krötenschemel", "Iribaarslilie", "Kairan", "Kajubo", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Mirhamer Seidenliane", "Rahjalieb", "Rattenpilz", "Rote Pfeilblüte", "Sansaro", "Schleichender Tod", "Wirselkraut", "Zwölfblatt"}),
            new(Occur.VeryCommon, Occur.VeryCommon, "Regenwald", new string[]{ "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Küste, Strand", "Flussauen", "Regenwald", "Wald", "Waldrand"}, new string[]{ "Moosaffe", "Otan-Otan", "Löwenaffe", "Purzelaffe", "Fleckenpanther", "Zwergelefant", "Brabaker Waldelefant", "Dschungeltiger", "Lioma", "Riesenaffe"}, new string[]{ "Alveranie", "Arganstrauch", "Blutblatt", "Boronie", "Boronsschlinge", "Carlog", "Cheria-Kaktus", "Disdychonda", "Eitriger Krötenschemel", "Finage", "Höllenkraut", "Ilmenblatt", "Kairan", "Kajubo", "Kukuka", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Merach-Strauch", "Mirhamer Seidenliane", "Orazal", "Quinja", "Rahjalieb", "Rattenpilz", "Rote Pfeilblüte", "Schleichender Tod", "Shurinstrauch", "Vragieswurzel", "Waldwebe", "Wirselkraut", "Würgedattel", "Zunderschwamm"}),
            new(Occur.Modest, Occur.Modest, "Südliche Regengebirge", new string[]{ "Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Regenwald", "Wald", "Waldrand", "Höhle (feucht)"}, new string[]{ "Karnickel", "Löwenaffe", "Purzelaffe", "Moosaffe", "Otan-Otan", "Fleckenpanther", "Dschungeltiger", "Riesenaffe"}, new string[]{ "Alveranie", "Atmon", "Blutblatt", "Eitriger Krötenschemel", "Feuermoos und Efferdmoos", "Finage", "Höllenkraut", "Ilmenblatt", "Kukuka", "Merach-Strauch", "Mirhamer Seidenliane", "Bleichmohn (Weißer Mohn)", "Grauer Mohn", "Orazal", "Rattenpilz", "Schleichender Tod", "Vierblättrige Einbeere", "Vragieswurzel"}),
            new(Occur.None, Occur.Modest, "Ifirns Ozean", new string[]{ "Meer"}, new string[]{}, new string[]{}),
            new(Occur.None, Occur.Common, "Meer der Sieben Winde", new string[]{ "Meer"}, new string[]{}, new string[]{}),
            new(Occur.None, Occur.Modest, "Südmeer (Feuermeer)", new string[]{ "Meer"}, new string[]{}, new string[]{}),
            new(Occur.None, Occur.Common, "Perlenmeer", new string[]{ "Meer"}, new string[]{}, new string[]{ "Feuerschlick", "Sansaro"})
        };

        public static Landscape[] Landscapes => new Landscape[]
        {
            new("Eis", "Eiskundig"),
            new("Wüste und Wüstenrand", "Wüstenkundig"),
            new("Gebirge", "Gebirgskundig"),
            new("Hochland"),
            new("Steppe", "Steppenkundig"),
            new("Flussauen"),
            new("Fluss- und Seeufer, Teiche"),
            new("Grasland, Wiesen"),
            new("Höhle (feucht)", "Höhlenkundig"),
            new("Höhle (trocken)", "Höhlenkundig"),
            new("Küste, Strand"),
            new("Meer", "Meereskundig"),
            new("Regenwald", "Dschungelkundig"),
            new("Sumpf und Moor", "Sumpfkundig"),
            new("Wald", "Waldkundig"),
            new("Waldrand", "Waldkundig")
        };

        #endregion hard coded data

        public Charakter[] GetCharactersFromTool()
        {
            if (IsHeldenToolInstalled)
            {
                return HeldentoolInterop.Load();
            }
            throw new Exception("\"Heldentool\" is not installed.");
        }

        public void LoadCharacterFromFile(string filename)
        {
            LoadCharacter(HeldentoolInterop.LoadFromXML(filename));
        }

        public void LoadCharacter(Charakter character)
        {
            this.character = character;
            _knownTerrains.Clear();
            IsKnownTerrain = false;
            foreach (Ability ability in character.Eigenschaften)
            {
                switch (ability.Name)
                {
                    case "Mut":
                        MU = ability.Wert;
                        break;
                    case "Intuition":
                        IN = ability.Wert;
                        break;
                    case "Fingerfertigkeit":
                        FF = ability.Wert;
                        break;
                    case "Gewandtheit":
                        GE = ability.Wert;
                        break;
                    default:
                        break;
                }
            }
            foreach (Ability ability in character.Talente)
            {
                switch (ability.Name)
                {
                    case "Wildnisleben":
                        SkillWildnisleben = ability.Wert;
                        break;
                    case "Sinnenschärfe":
                        SkillSinnenschaerfe = ability.Wert;
                        break;
                    case "Pflanzenkunde":
                        SkillPflanzenkunde = ability.Wert;
                        break;
                    case "Tierkunde":
                        SkillTierkunde = ability.Wert;
                        break;
                    case "Fährtensuchen":
                        SkillFaehrtensuchen = ability.Wert;
                        break;
                    case "Schleichen":
                        SkillSchleichen = ability.Wert;
                        break;
                    case "Sich Verstecken":
                        SkillSichVerstecken = ability.Wert;
                        break;
                    default:
                        break;
                }
            }
            foreach (string sonderfertigkeit in character.Sonderfertigkeiten)
            {
                if (Terrains.Contains(sonderfertigkeit))
                {
                    _knownTerrains.Add(sonderfertigkeit);
                }
            }
            if (CurrentLandscape.Terrain != null)
            {
                IsKnownTerrain = _knownTerrains.Contains(CurrentLandscape.Terrain);
            }
        }

        public void AddKnownTerrain(string terrain)
        {
            if (Terrains.Contains(terrain))
            {
                _knownTerrains.Add(terrain);
            }
            else
            {
                throw new ArgumentException(nameof(terrain) + " is invalid");
            }
        }

        public static Region GetRegion(string regionName)
        {
            foreach (Region region in Regions)
            {
                if (region.Name == regionName)
                {
                    return region;
                }
            }
            throw new ArgumentException(nameof(regionName) + " is invalid");
        }

        public static Landscape GetLandscape(string landscapeName)
        {
            foreach (Landscape landscape in Landscapes)
            {
                if (landscape.Name == landscapeName)
                {
                    return landscape;
                }
            }
            throw new ArgumentException(nameof(landscapeName) + " is invalid");
        }

        public static string OccurToString(int occur)
        {
            return occur switch
            {
                1 => "sehr häufig",
                2 => "häufig",
                4 => "gelegentlich",
                8 => "selten",
                16 => "sehr selten",
                _ => "kein Vorkommen",
            };
        }
    }
}