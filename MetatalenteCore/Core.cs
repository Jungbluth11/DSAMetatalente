using DSAUtils.HeldentoolInterop;
using DSAUtils.Settings.Aventurien;

namespace DSAMetatalente.Core;

public class Core
{
    internal Charakter? character;
    private readonly List<string> _knownTerrains = [];
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
    public string[] KnownTerrains => [.. _knownTerrains];
    public string CurrentMonth { get; set; } = Months[0];
    public bool IsHeldenToolInstalled => HeldentoolInterop.IsInstalled();
    public bool IsKnownTerrain { get; set; }
    public Region CurrentRegion { get; set; } = Regions[0];
    public Landscape CurrentLandscape { get; set; } = Landscapes[0];

    #region hard coded data

    public static string[] Months =>
    [
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
    ];
    public static Region[] Regions =>
    [
        new(Occur.None, Occur.VeryRare, "Ewiges Eis", ["Eis"], ["Felsrobbe", "Firnyak", "Firnluchs", "Firunsbär", "Mammut", "Mastodon", "Meerkalb", "Schneedachs", "Seetiger"], ["Alveranie", "Blutblatt", "Rattenpilz"]),
        new(Occur.Rare, Occur.Modest, "Nördliche Tundra", ["Eis", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche"], ["Felsrobbe", "Karen", "Pfeifhase", "Elch", "Firnluchs", "Firnyak", "Schneedachs", "Blaufuchs", "Firunsbär", "Mammut", "Mastodon", "Meerkalb", "Seetiger", "Steppenhund", "Steppentiger"], ["Alveranie", "Blutblatt", "Kairan", "Bunter Mohn", "Rattenpilz", "Talaschin", "Vierblättrige Einbeere", "Wirselkraut"]),
        new(Occur.Modest, Occur.Modest, "Nördliche Grasländer und Steppen", ["Hochland", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], ["Karen", "Karnickel", "Steppenhund", "Gelbfuchs", "Pfeifhase", "Rebhuhn", "Rotpüschel", "Elch", "Firunshirsch", "Klippechse", "Rotluchs", "Schneedachs", "Steppenrind", "Trappe", "Blaufuchs", "Borkenbär", "Firnluchs", "Halmar-Antilope", "Mammut", "Orklandbär", "Steppentiger", "Vielfraß"], ["Alraune", "Alveranie", "Blutblatt", "Donf", "Egelschreck", "Eitriger Krötenschemel", "Grüne Schleimschlange", "Gulmond", "Joruga", "Kairan", "Klippenzahn", "Madablüte", "Messergras", "Bunter Mohn", "Naftanstaude", "Orklandbovist", "Rattenpilz", "Roter Drachenschlund", "Shurinstrauch", "Tarnele", "Thonnys", "Tigermohn", "Traschbart", "Vierblättrige Einbeere", "Wirselkraut", "Zwölfblatt"]),
        new(Occur.Modest, Occur.Modest, "Nördliches Hochland", ["Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], ["Orklandkarnickel", "Rebhuhn", "Gelbfuchs", "Halmar-Antilope", "Rotpüschel", "Trappe", "Klippechse", "Orklandbär", "Rotfuchs", "Rotluchs", "Schreckkatze", "Sonnenluchs", "Borkenbär", "Mammut", "Riesenaffe", "Steppentiger"], ["Alraune", "Alveranie", "Basilamine", "Blutblatt", "Eitriger Krötenschemel", "Grüne Schleimschlange", "Gulmond", "Kairan", "Klippenzahn", "Messergras", "Bunter Mohn", "Naftanstaude", "Orklandbovist", "Rattenpilz", "Shurinstrauch", "Tarnele", "Traschbart", "Vierblättrige Einbeere", "Wirselkraut", "Zwölfblatt"]),
        new(Occur.Rare, Occur.Rare, "Kalkgebirge", ["Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Wald", "Waldrand", "Höhle (feucht)", "Höhle (trocken)"], ["Gebirgsbock", "Karnickel", "Berglöwe", "Höhlenbär", "Rotluchs", "Sonnenluchs"], ["Alveranie", "Atan-Kiefer", "Blutblatt", "Eitriger Krötenschemel", "Feuermoos und Efferdmoos", "Gulmond", "Joruga", "Kairan", "Madablüte", "Bleichmohn (Weißer Mohn)", "Grauer Mohn", "Nothilf", "Phosphorpilz", "Rattenpilz", "Talaschin", "Tarnele", "Thonnys", "Traschbart", "Vierblättrige Einbeere", "Vragieswurzel", "Wirselkraut", "Zwölfblatt"]),
        new(Occur.Rare, Occur.Rare, "Andere Mittelländische Gebirge", ["Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Wald", "Waldrand", "Höhle (feucht)", "Höhle (trocken)"], ["Gebirgsbock", "Karnickel", "Pfeifhase", "Riesenlöffler", "Berglöwe", "Sonnenluchs", "Höhlenbär", "Rotluchs"], ["Alveranie", "Blutblatt", "Eitriger Krötenschemel", "Feuermoos und Efferdmoos", "Gulmond", "Joruga", "Kairan", "Madablüte", "Bleichmohn (Weißer Mohn)", "Grauer Mohn", "Nothilf", "Phosphorpilz", "Rattenpilz", "Schleimiger Sumpfknöterich", "Talaschin", "Tarnele", "Thonnys", "Traschbart", "Vierblättrige Einbeere", "Vragieswurzel", "Wirselkraut", "Zwölfblatt"]),
        new(Occur.Common, Occur.Common, "Nördliche Wälder (Westküste)", ["Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], ["Karnickel", "Rebhuhn", "Wildschwein", "Auerhahn", "Rehwild", "Streifendachs", "Fasan", "Kronenhirsch", "Pfeifhase", "Rotfuchs", "Rotluchs", "Rotpüschel", "Auerochse", "Baumbär", "Blaufuchs", "Borkenbär", "Vielfraß", "Waldlöwe", "Wildkatze", "Höhlenbär", "Riesenaffe", "Silberlöwe"], ["Alraune", "Alveranie", "Basilamine", "Belmart", "Blutblatt", "Carlog", "Efeuer", "Eitriger Krötenschemel", "Gulmond", "Hollbeere", "Joruga", "Kairan", "Klippenzahn", "Mibelrohr", "Orklandbovist", "Pestsporenpilz", "Rattenpilz", "Roter Drachenschlund", "Shurinstrauch", "Tarnele", "Thonnys", "Traschbart", "Ulmenwürger", "Vierblättrige Einbeere", "Waldwebe", "Wirselkraut", "Zunderschwamm", "Zwölfblatt"]),
        new(Occur.Common, Occur.VeryCommon, "Nördliche Wälder (Taiga)", ["Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], ["Karnickel", "Wildschwein", "Fasan", "Pfeifhase", "Rebhuhn", "Rehwild", "Elch", "Rotluchs", "Schneedachs", "Wildkatze", "Blaufuchs", "Firunshirsch", "Karen", "Kronenhirsch", "Schwarzbär", "Sonnenluchs", "Streifendachs", "Vielfraß", "Waldlöwe", "Auerochse", "Borkenbär", "Rotfuchs", "Silberlöwe"], ["Alraune", "Alveranie", "Belmart", "Blutblatt", "Efeuer", "Eitriger Krötenschemel", "Gulmond", "Joruga", "Kairan", "Nothilf", "Pestsporenpilz", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Schleimiger Sumpfknöterich", "Shurinstrauch", "Tarnele", "Thonnys", "Traschbart", "Ulmenwürger", "Vierblättrige Einbeere", "Waldwebe", "Wirselkraut", "Zunderschwamm", "Zwölfblatt"]),
        new(Occur.Common, Occur.VeryCommon, "Nördliche Wälder (Bornland)", ["Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], ["Pfeifhase", "Rehwild", "Silberbock", "Wildschwein", "Blaufuchs", "Elch", "Kronenhirsch", "Rebhuhn", "Sonnenluchs", "Streifendachs", "Sumpfranze", "Auerochse", "Firunshirsch", "Höhlenbär", "Riesenaffe", "Rotluchs", "Schneedachs", "Schwarzbär", "Vielfraß", "Waldlöwe", "Wildkatze"], ["Alraune", "Alveranie", "Belmart", "Blutblatt", "Efeuer", "Eitriger Krötenschemel", "Gulmond", "Joruga", "Kairan", "Nothilf", "Pestsporenpilz", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Schleimiger Sumpfknöterich", "Tarnele", "Thonnys", "Ulmenwürger", "Vierblättrige Einbeere", "Waldwebe", "Wasserrausch", "Wirselkraut", "Zunderschwamm", "Zwölfblatt"]),
        new(Occur.Modest, Occur.Rare, "Nördliche Sümpfe, Marschen und Moore", ["Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor", "Waldrand"], ["Karnickel", "Klippechse", "Sumpfranze"], ["Alraune", "Alveranie", "Blutblatt", "Carlog", "Donf", "Egelschreck", "Eitriger Krötenschemel", "Grüne Schleimschlange", "Iribaarslilie", "Kairan", "Mibelrohr", "Morgendornstrauch", "Neckerkraut", "Pestsporenpilz", "Rahjalieb", "Rattenpilz", "Schleimiger Sumpfknöterich", "Schlinggras", "Tarnele", "Traschbart", "Vierblättrige Einbeere", "Wirselkraut", "Zwölfblatt"])
        ,new(Occur.Common, Occur.Common, "Mittelländische Grasländer, Heide und Steppe", ["Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], ["Pfeifhase", "Rotpüschel", "Gänseluchs", "Rebhuhn", "Trappe", "Fasan", "Gelbfuchs", "Gabelantilope", "Sonnenluchs", "Wildkatze", "Steppentiger"], ["Alraune", "Alveranie", "Blutblatt", "Chonchinis", "Donf", "Egelschreck", "Eitriger Krötenschemel", "Gulmond", "Hiradwurz", "Ilmenblatt", "Joruga", "Kairan", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Lulanie", "Messergras", "Mibelrohr", "Mirbelstein", "Bunter Mohn", "Purpurmohn", "Schwarzer Mohn", "Tigermohn", "Naftanstaude", "Neckerkraut", "Rahjalieb", "Rattenpilz", "Roter Drachenschlund", "Schlangenzünglein", "Schwarmschwamm", "Shurinstrauch", "Tarnele", "Vierblättrige Einbeere", "Winselgras", "Wirselkraut", "Zwölfblatt"]),
        new(Occur.Common, Occur.Common, "Mittelländische Wälder (Gemäßigtes Klima)", ["Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], ["Karnickel", "Rehwild", "Riesenlöffler", "Wildschwein", "Gänseluchs", "Pfeifhase", "Rebhuhn", "Rotpüschel", "Auerhahn", "Fasan", "Kronenhirsch", "Rotfuchs", "Streifendachs", "Auerochse", "Baumbär", "Höhlenbär", "Rotluchs", "Schwarzbär", "Silberbock", "Wildkatze", "Silberlöwe"], ["Alraune", "Alveranie", "Belmart", "Blutblatt", "Carlog", "Chonchinis", "Efeuer", "Egelschreck", "Eitriger Krötenschemel", "Gulmond", "Ilmenblatt", "Joruga", "Kairan", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Lulanie", "Mibelrohr", "Mirbelstein", "Neckerkraut", "Quasselwurz", "Rahjalieb", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Schleimiger Sumpfknöterich", "Shurinstrauch", "Tarnele", "Traschbart", "Ulmenwürger", "Vierblättrige Einbeere", "Vragieswurzel", "Waldwebe", "Wirselkraut", "Zunderschwamm", "Zwölfblatt"]),
        new(Occur.Common, Occur.Common, "Mittelländische Wälder (Yaquirisches Klima)", ["Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], ["Karnickel", "Rehwild", "Riesenlöffler", "Rotfuchs", "Rotpüschel", "Wildschwein", "Gänseluchs", "Rebhuhn", "Regenbogenfasan", "Rotluchs", "Baumbär", "Raschtulsluchs", "Streifendachs", "Wildkatze"], ["Alraune", "Alveranie", "Arganstrauch", "Belmart", "Blutblatt", "Boronsschlinge", "Carlog", "Chonchinis", "Efeuer", "Egelschreck", "Eitriger Krötenschemel", "Ilmenblatt", "Joruga", "Kairan", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Lulanie", "Mibelrohr", "Purpurmohn", "Schwarzer Mohn", "Neckerkraut", "Quasselwurz", "Rahjalieb", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Shurinstrauch", "Tarnele", "Traschbart", "Ulmenwürger", "Vierblättrige Einbeere", "Vragieswurzel", "Waldwebe", "Zunderschwamm", "Zwölfblatt"]),
        new(Occur.Common, Occur.Modest, "Mittelländische Wälder (Tobrisches Klima)", ["Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], ["Rehwild", "Riesenlöffler", "Rotpüschel", "Auerhahn", "Fasan", "Rebhuhn", "Rotfuchs", "Wildkatze", "Wildschwein", "Auerochse", "Baumbär", "Riesenaffe"], ["Alraune", "Alveranie", "Belmart", "Blutblatt", "Chonchinis", "Efeuer", "Egelschreck", "Eitriger Krötenschemel", "Gulmond", "Ilmenblatt", "Joruga", "Kairan", "Mirbelstein", "Purpurmohn", "Quasselwurz", "Rahjalieb", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Schwarmschwamm", "Tarnele", "Traschbart", "Tuur-Amash-Kelch", "Ulmenwürger", "Vierblättrige Einbeere", "Waldwebe", "Wasserrausch", "Zunderschwamm", "Zwölfblatt"]),
        new(Occur.Common, Occur.Common, "Immergrüne Wälder (Südosten)", ["Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], ["Rehwild", "Riesenlöffler", "Rotpüschel", "Auerhahn", "Ongalobulle", "Raschtulsluchs", "Rebhuhn", "Regenbogenfasan", "Rotfuchs", "Warzenschwein", "Al'Kebir-Antilope", "Baumbär", "Springbock", "Wildkatze", "Moosaffe", "Riesenaffe"], ["Alraune", "Alveranie", "Blutblatt", "Chonchinis", "Dornrose", "Efeuer", "Egelschreck", "Eitriger Krötenschemel", "Ilmenblatt", "Joruga", "Kairan", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Purpurmohn", "Quasselwurz", "Rahjalieb", "Rattenpilz", "Roter Drachenschlund", "Satuariensbusch", "Schwarzer Wein", "Shurinstrauch", "Tarnele", "Traschbart", "Waldwebe", "Zunderschwamm", "Zwölfblatt"]),
        new(Occur.Common, Occur.Modest, "Südländische Grasländer und Steppen", ["Wüste und Wüstenrand", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], ["Al'Kebir-Antilope", "Gabelantilope", "Springbock", "Khômgepard", "Ongalobulle", "Raschtulsluchs", "Strauß", "Warzenschwein", "Fasan", "Gelbfuchs", "Regenbogenfasan", "Rehwild", "Rotpüschel", "Sandlöwe"], ["Alraune", "Alveranie", "Atmon", "Blutblatt", "Chonchinis", "Dornrose", "Eitriger Krötenschemel", "Finage", "Hiradwurz", "Jagdgras", "Kairan", "Khôm- oder Mhanadiknolle", "Menchal-Kaktus", "Merach-Strauch", "Messergras", "Mirbelstein", "Bunter Mohn", "Purpurmohn", "Naftanstaude", "Olginwurz", "Rattenpilz", "Schlangenzünglein", "Schwarzer Wein", "Shurinstrauch", "Talaschin", "Tarnele", "Winselgras", "Wirselkraut", "Yaganstrauch", "Zithabar", "Zwölfblatt"]),
        new(Occur.Rare, Occur.Rare, "Wüstenrandgebiete", ["Wüste und Wüstenrand", "Steppe", "Grasland, Wiesen", "Wald", "Waldrand"], ["Gabelantilope", "Springbock", "Al'Kebir-Antilope", "Rotpüschel", "Khômgepard", "Sandlöwe", "Strauß", "Raschtulsluchs"], ["Alveranie", "Atmon", "Blutblatt", "Cheria-Kaktus", "Chonchinis", "Eitriger Krötenschemel", "Finage", "Hiradwurz", "Khôm- oder Mhanadiknolle", "Menchal-Kaktus", "Merach-Strauch", "Messergras", "Purpurmohn", "Naftanstaude", "Olginwurz", "Rattenpilz", "Shurinstrauch", "Talaschin", "Tarnele", "Winselgras", "Wirselkraut", "Zwölfblatt"]),
        new(Occur.VeryRare, Occur.VeryRare, "Wüste", ["Wüste und Wüstenrand"], ["Sandlöwe"], ["Alveranie", "Blutblatt", "Cheria-Kaktus", "Khôm- oder Mhanadiknolle", "Menchal-Kaktus", "Rattenpilz", "Talaschin"]),
        new(Occur.Rare, Occur.Rare, "Südländische Gebirge", ["Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Wald", "Waldrand"], ["Raschtulsluchs"], ["Alveranie", "Atmon", "Blutblatt", "Eitriger Krötenschemel", "Kairan", "Bleichmohn (Weißer Mohn)", "Olginwurz", "Rattenpilz", "Talaschin", "Tarnele", "Vierblättrige Einbeere", "Wirselkraut"]),
        new(Occur.Modest, Occur.Common, "Maraskan", ["Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor", "Regenwald", "Wald", "Waldrand"], ["Riesenlöffler", "Otan-Otan", "Rotpüschel", "Vy'Tagga-Antilope", "Wildschwein", "Maraskanisches Stachelschwein", "Baumschleimer", "Baumwürger", "Rehwild", "Riesenaffe"], ["Alraune", "Alveranie", "Axorda-Baum", "Blutblatt", "Disdychonda", "Eitriger Krötenschemel", "Horusche", "Jagdgras", "Kairan", "Rattenpilz", "Rauschgurke", "Schlangenzünglein", "Shurinstrauch", "Tarnblatt", "Tarnele", "Trichterwurzel", "Wirselkraut", "Yaganstrauch"]),
        new(Occur.Rare, Occur.Rare, "Südliche Sümpfe", ["Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Küste, Strand", "Flussauen", "Sumpf und Moor", "Wald", "Waldrand"], ["Sumpfranze"], ["Alveranie", "Arganstrauch", "Atmon", "Blutblatt", "Carlog", "Chonchinis", "Disdychonda", "Donf", "Egelschreck", "Eitriger Krötenschemel", "Iribaarslilie", "Kairan", "Kajubo", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Mirhamer Seidenliane", "Rahjalieb", "Rattenpilz", "Rote Pfeilblüte", "Sansaro", "Schleichender Tod", "Wirselkraut", "Zwölfblatt"]),
        new(Occur.VeryCommon, Occur.VeryCommon, "Regenwald", ["Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Küste, Strand", "Flussauen", "Regenwald", "Wald", "Waldrand"], ["Moosaffe", "Otan-Otan", "Löwenaffe", "Purzelaffe", "Fleckenpanther", "Zwergelefant", "Brabaker Waldelefant", "Dschungeltiger", "Lioma", "Riesenaffe"], ["Alveranie", "Arganstrauch", "Blutblatt", "Boronie", "Boronsschlinge", "Carlog", "Cheria-Kaktus", "Disdychonda", "Eitriger Krötenschemel", "Finage", "Höllenkraut", "Ilmenblatt", "Kairan", "Kajubo", "Kukuka", "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", "Purpurner Lotus", "Schwarzer Lotus", "Grauer Lotus", "Weißer Lotus", "Weißgelber Lotus", "Merach-Strauch", "Mirhamer Seidenliane", "Orazal", "Quinja", "Rahjalieb", "Rattenpilz", "Rote Pfeilblüte", "Schleichender Tod", "Shurinstrauch", "Vragieswurzel", "Waldwebe", "Wirselkraut", "Würgedattel", "Zunderschwamm"]),
        new(Occur.Modest, Occur.Modest, "Südliche Regengebirge", ["Gebirge", "Hochland", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Regenwald", "Wald", "Waldrand", "Höhle (feucht)"], ["Karnickel", "Löwenaffe", "Purzelaffe", "Moosaffe", "Otan-Otan", "Fleckenpanther", "Dschungeltiger", "Riesenaffe"], ["Alveranie", "Atmon", "Blutblatt", "Eitriger Krötenschemel", "Feuermoos und Efferdmoos", "Finage", "Höllenkraut", "Ilmenblatt", "Kukuka", "Merach-Strauch", "Mirhamer Seidenliane", "Bleichmohn (Weißer Mohn)", "Grauer Mohn", "Orazal", "Rattenpilz", "Schleichender Tod", "Vierblättrige Einbeere", "Vragieswurzel"]),
        new(Occur.None, Occur.Modest, "Ifirns Ozean", ["Meer"], [], []),
        new(Occur.None, Occur.Common, "Meer der Sieben Winde", ["Meer"], [], []),
        new(Occur.None, Occur.Modest, "Südmeer (Feuermeer)", ["Meer"], [], []),
        new(Occur.None, Occur.Common, "Perlenmeer", ["Meer"], [], ["Feuerschlick", "Sansaro"])
    ];

    public static Landscape[] Landscapes =>
    [
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
    ];

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
