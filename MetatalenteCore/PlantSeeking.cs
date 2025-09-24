using System.Text.RegularExpressions;

namespace Metatalente.Core;

public class PlantSeeking : MetatalentBase
{
    private readonly Random _random = new();
    public bool Coincidence { get; set; } = false;
    public Plant CurrentPlant { get; set; } = Plants[0];

    #region hard coded data

    public static Plant[] Plants =>
    [
        new(9, "Alraune", ["1 Pflanze"], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche", "Waldrand"], [Occur.Rare, Occur.Rare ]),
        new(-5, "Alveranie", ["12 Blätter, jeweils in der Farbe des Monats" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja" ], ["Eis", "Wüste und Wüstenrand", "Gebirge", "Hochland", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor", "Regenwald", "Wald", "Waldrand"], [Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare ]),
        new(4, "Arganstrauch", ["1 Wurzel" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Sumpf und Moor", "Regenwald", "Wald"], [Occur.Rare, Occur.Modest, Occur.VeryRare ]),
        new(6, "Atan-Kiefer", ["W20 Stein Rinde, bei komplettem Abschälen Verdreifachung des Wertes" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Gebirge"], [Occur.Rare ]),
        new(5, "Atmon", ["W6 Büschel" ], ["Peraine" ], ["Hochland", "Steppe", "Flussauen", "Sumpf und Moor"], [Occur.Rare, Occur.Modest, Occur.VeryRare, Occur.VeryRare ]),
        new(4, "Axorda-Baum", ["1 Baum" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Gebirge", "Regenwald"], [Occur.VeryRare, Occur.VeryRare ]),
        new(15, "Basilamine", ["W20+10 Schoten" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wald", "Waldrand"], [Occur.Modest, Occur.Rare ]),
        new(6, "Belmart", ["2W20 Blätter" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wald", "Waldrand"], [Occur.Modest, Occur.Rare ]),
        new(4, "Blutblatt", ["W20+2 Zweige pro 10 AsP der Quelle" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Eis", "Wüste und Wüstenrand", "Gebirge", "Hochland", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Küste, Strand", "Flussauen", "Sumpf und Moor", "Regenwald", "Wald", "Waldrand"], [Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare ]),
        new(-2, "Boronie", ["5 Blüten, die kurz vor dem Verblühen sind" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Grasland, Wiesen", "Regenwald"], [Occur.VeryRare, Occur.Rare ]),
        new(15, "Boronsschlinge", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Regenwald", "Wald"], [Occur.VeryRare, Occur.VeryRare ]),
        new(5, "Carlog", ["W6 Blüten", " mit je 1 Stempel" ], ["Efferd", "Peraine" ], ["Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor"], [Occur.Rare, Occur.VeryRare, Occur.Modest ]),
        new(4, "Cheria-Kaktus", ["W3 Stein Kaktusfleisch", " und pro Stein 3W6+8 Stacheln" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wüste und Wüstenrand"], [Occur.Rare ]),
        new(6, "Chonchinis", ["W20 Blätter" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Hochland", "Steppe", "Waldrand"], [Occur.VeryRare, Occur.Modest, Occur.Rare ]),
        new(5, "Disdychonda", ["4 Blätter" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Regenwald", "Wald"], [Occur.VeryRare, Occur.VeryRare ]),
        new(6, "Donf", ["1 Stängel" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor"], [Occur.Rare, Occur.VeryRare, Occur.Modest ]),
        new(3, "Dornrose", ["Strauch mit W6 Blüten" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Grasland, Wiesen", "Wald"], [Occur.Common, Occur.Modest ]),
        new(4, "Efeuer", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wald", "Höhle (feucht)", "Höhle (trocken)"], [Occur.Rare, Occur.Modest, Occur.Modest ]),
        new(6, "Egelschreck", ["2W20 Blätter" ], ["Rondra", "Efferd" ], ["Grasland, Wiesen", "Flussauen", "Sumpf und Moor", "Waldrand"], [Occur.Modest, Occur.Modest, Occur.Common, Occur.Rare ]),
        new(2, "Eitriger Krötenschemel", ["2W6 Pilzhäute" ], ["Efferd", "Travia", "Boron" ], ["Flussauen", "Sumpf und Moor", "Wald"], [Occur.Rare, Occur.Modest, Occur.VeryRare ]),
        new(15, "Feuermoos und Efferdmoos", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Höhle (feucht)"], [Occur.Modest ]),
        new(6, "Feuerschlick", ["W6 Stein der Algen" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Küste, Strand", "Meer"], [Occur.Common, Occur.VeryRare ]),
        new(5, "Finage", ["Baum mit W20 Trieben", " und Bast" ], ["Boron", "Hesinde", "Firun", "Peraine" ], ["Grasland, Wiesen", "Regenwald", "Wald"], [Occur.VeryRare, Occur.Rare, Occur.Rare ]),
        new(4, "Grüne Schleimschlange", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Flussauen", "Sumpf und Moor"], [Occur.VeryRare, Occur.VeryRare ]),
        new(6, "Gulmond", ["2W6 Blätter" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Hochland", "Steppe", "Wald"], [Occur.Modest, Occur.Common, Occur.Modest ]),
        new(8, "Hiradwurz", ["1 Wurzel" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Steppe"], [Occur.Rare ]),
        new(4, "Hollbeere", ["2W6 Sträucher", " mit jeweils 2W6+5 Beeren", " und 2W6+3 Blätter der untersten Zweige" ], ["Rondra", "Efferd" ], ["Wald", "Waldrand"], [Occur.Modest, Occur.Common ]),
        new(8, "Höllenkraut", ["W10 Stein der Ranken" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Regenwald", "Wald", "Waldrand"], [Occur.Common, Occur.Rare, Occur.VeryRare ]),
        new(7, "Horusche", ["W6 erntereife Schoten", " mit je W3 Kernen" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wald"], [Occur.Rare ]),
        new(2, "Ilmenblatt", ["W20 Blätter", " und W20 Blüten" ], ["Travia", "Ingerimm" ], ["Gebirge", "Grasland, Wiesen", "Wald"], [Occur.VeryRare, Occur.Modest, Occur.Modest ]),
        new(12, "Iribaarslilie", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Sumpf und Moor"], [Occur.VeryRare ]),
        new(15, "Jagdgras", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Gebirge", "Hochland", "Steppe", "Wald"], [Occur.Modest, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare ]),
        new(7, "Joruga", ["1 Wurzel" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Rahja", "Namenlose Tage" ], ["Gebirge", "Grasland, Wiesen", "Wald"], [Occur.VeryRare, Occur.Rare, Occur.Modest ]),
        new(6, "Kairan", ["1 Halm" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche"], [Occur.Rare ]),
        new(4, "Kajubo", ["2W6 Knospen (Nur die Hälfte um den Strauch zu schonen)" ], ["Praios", "Rondra", "Efferd", "Travia", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Küste, Strand", "Waldrand"], [Occur.Rare, Occur.Rare ]),
        new(12, "Khôm- oder Mhanadiknolle", ["1 Wurzel", " mit W6 Maß klarem Wasser" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wüste und Wüstenrand", "Steppe"], [Occur.VeryRare, Occur.Rare ]),
        new(8, "Klippenzahn", ["2W6 Stängel" ], ["Praios", "Rondra", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Gebirge", "Hochland"], [Occur.Modest, Occur.Modest ]),
        new(10, "Kukuka", ["1W3 x 20 Blätter" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Regenwald"], [Occur.Rare ]),
        new(9, "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", ["2W6+1 Blüten" ], ["Praios", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche"], [Occur.Modest ]),
        new(7, "Purpurner Lotus", ["W6+1 Blüten" ], ["Praios", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche"], [Occur.Rare ]),
        new(6, "Schwarzer Lotus", ["W6 Blüten" ], ["Praios", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche"], [Occur.VeryRare ]),
        new(8, "Grauer Lotus", ["W6+1 Blüten" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche", "Sumpf und Moor"], [Occur.Rare, Occur.VeryRare ]),
        new(10, "Weißer Lotus", ["W6+1 Blüten" ], ["Praios", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche", "Sumpf und Moor"], [Occur.Rare, Occur.VeryRare ]),
        new(10, "Weißgelber Lotus", ["W3 Blüten" ], ["Praios", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche"], [Occur.Modest ]),
        new(5, "Lulanie", ["1 Blüte" ], ["Praios", "Rondra", "Rahja", "Namenlose Tage" ], ["Wald", "Waldrand"], [Occur.Rare, Occur.VeryRare ]),
        new(15, "Madablüte", ["1 Blüte" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Gebirge", "Steppe"], [Occur.VeryRare, Occur.VeryRare ]),
        new(4, "Menchal-Kaktus", ["1 Kaktus", " mit W3 Maß Menchalsaft; bei 1 auf W20 außerdem mit W6 Blüten" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wüste und Wüstenrand", "Hochland"], [Occur.VeryRare, Occur.VeryRare ]),
        new(2, "Merach-Strauch", ["2W20 reife Früchte" ], ["Efferd", "Travia" ], ["Regenwald", "Wald"], [Occur.Rare, Occur.VeryRare ]),
        new(6, "Messergras", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wüste und Wüstenrand", "Hochland", "Steppe"], [Occur.VeryRare, Occur.VeryRare, Occur.Rare ]),
        new(10, "Mibelrohr", ["2W6 Kolben" ], ["Praios", "Rondra", "Efferd", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor"], [Occur.Modest, Occur.Rare, Occur.VeryRare ]),
        new(8, "Mirbelstein", ["1 Wurzelknolle" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Hochland", "Grasland, Wiesen", "Wald"], [Occur.Rare, Occur.Modest, Occur.Common ]),
        new(4, "Mirhamer Seidenliane", ["1 Ranke", " mit W2+1 Knoten" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Gebirge", "Fluss- und Seeufer, Teiche", "Regenwald"], [Occur.VeryRare, Occur.VeryRare, Occur.Rare ]),
        new(5, "Bleichmohn (Weißer Mohn)", ["W6 geschlossene Samenkapseln" ], ["Rondra" ], ["Gebirge"], [Occur.Rare ]),
        new(-5, "Bunter Mohn", ["1 geschlossene Samenkapsel" ], ["Travia" ], ["Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Waldrand"], [Occur.Common, Occur.Modest, Occur.Rare ]),
        new(1, "Grauer Mohn", ["1 geschlossene Samenkapsel und 1 Blüte" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Gebirge"], [Occur.VeryRare ]),
        new(3, "Purpurmohn", ["1 geschlossene Samenkapsel" ], ["Rahja" ], ["Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Wald"], [Occur.Rare, Occur.VeryRare, Occur.VeryRare ]),
        new(5, "Schwarzer Mohn", ["2 Blätter", " und 1 geschlossene Samenkapsel" ], ["Efferd", "Travia", "Boron" ], ["Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"], [Occur.VeryCommon, Occur.VeryCommon, Occur.VeryCommon, Occur.VeryCommon, Occur.VeryCommon, Occur.VeryCommon ]),
        new(10, "Tigermohn", ["1 geschlossene Samenkapsel" ], ["Travia" ], ["Grasland, Wiesen", "Flussauen", "Waldrand"], [Occur.Rare, Occur.Rare, Occur.Rare ]),
        new(13, "Morgendornstrauch", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Sumpf und Moor"], [Occur.Rare ]),
        new(1, "Naftanstaude", ["1 Staude" ], ["Praios", "Rondra", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Steppe", "Grasland, Wiesen", "Waldrand"], [Occur.VeryRare, Occur.Rare, Occur.VeryRare ]),
        new(4, "Neckerkraut", ["W20+5 Blätter" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Grasland, Wiesen", "Küste, Strand", "Sumpf und Moor"], [Occur.Rare, Occur.Modest, Occur.VeryRare ]),
        new(6, "Nothilf", ["W20+2 Blüten", " und 2W20+10 Blätter" ], ["Praios", "Peraine" ], ["Gebirge", "Wald"], [Occur.Rare, Occur.Rare ]),
        new(10, "Olginwurz", ["W3 Moosballen" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Gebirge", "Hochland", "Wald"], [Occur.Rare, Occur.Rare, Occur.Rare ]),
        new(4, "Orazal", ["W6 verholzte Stängel" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Regenwald", "Wald"], [Occur.Rare, Occur.VeryRare ]),
        new(4, "Orklandbovist", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Hochland", "Steppe", "Wald"], [Occur.Rare, Occur.VeryRare, Occur.VeryRare ]),
        new(6, "Pestsporenpilz", ["1 Pilzhaut" ], ["Efferd", "Travia", "Boron", "Peraine", "Ingerimm", "Rahja" ], ["Grasland, Wiesen", "Sumpf und Moor", "Wald"], [Occur.Rare, Occur.Rare, Occur.VeryRare ]),
        new(10, "Phosphorpilz", ["W6 Stein Geflechtstücke" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Höhle (feucht)", "Höhle (trocken)"], [Occur.Modest, Occur.Rare ]),
        new(12, "Quasselwurz", ["1 Wurzel" ], ["Praios", "Rondra", "Efferd", "Travia", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wald"], [Occur.Rare ]),
        new(6, "Quinja", ["W3+2 Beeren" ], ["Praios", "Rondra", "Efferd", "Travia", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Regenwald", "Wald", "Waldrand"], [Occur.Common, Occur.Modest, Occur.Rare ]),
        new(5, "Rahjalieb", ["2W6 Blätter" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Grasland, Wiesen", "Sumpf und Moor", "Regenwald", "Wald"], [Occur.Modest, Occur.Modest, Occur.Common, Occur.Modest ]),
        new(7, "Rattenpilz", ["1 Pilz" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Gebirge", "Hochland", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Küste, Strand", "Flussauen", "Sumpf und Moor", "Regenwald", "Wald", "Waldrand"], [Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare ]),
        new(3, "Rauschgurke", ["3W6 reife Rauschgurken" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wald", "Waldrand"], [Occur.Modest, Occur.Rare ]),
        new(7, "Rote Pfeilblüte", ["W6 Blüten" ], ["Peraine", "Ingerimm", "Rahja" ], ["Sumpf und Moor", "Regenwald", "Wald"], [Occur.Modest, Occur.Modest, Occur.VeryRare ]),
        new(10, "Roter Drachenschlund", ["W6 Blätter" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche", "Waldrand"], [Occur.VeryRare, Occur.VeryRare ]),
        new(12, "Sansaro", ["1 Pflanze" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Küste, Strand", "Meer"], [Occur.Common, Occur.Rare ]),
        new(-2, "Satuariensbusch", ["4W20 Blätter,", " W20 Blüten,", " W20 Früchte,", " W3 Flux Saft" ], ["Praios", "Efferd", "Travia", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wald", "Waldrand"], [Occur.Modest, Occur.Modest ]),
        new(3, "Schlangenzünglein", ["Saft einer Pflanze" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche", "Sumpf und Moor", "Regenwald"], [Occur.Rare, Occur.Rare, Occur.VeryRare ]),
        new(6, "Schleichender Tod", ["W6 Blüten" ], ["Ingerimm", "Rahja" ], ["Fluss- und Seeufer, Teiche", "Waldrand"], [Occur.VeryRare, Occur.VeryRare ]),
        new(3, "Schleimiger Sumpfknöterich", ["2W6 Pilze" ], ["Praios", "Rondra", "Efferd", "Travia" ], ["Wald", "Waldrand"], [Occur.Rare, Occur.VeryRare ]),
        new(12, "Schlinggras", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Sumpf und Moor"], [Occur.Rare ]),
        new(3, "Schwarmschwamm", ["1 Schwamm", " und W2 Samenkörper" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche", "Flussauen"], [Occur.VeryRare, Occur.VeryRare ]),
        new(2, "Schwarzer Wein", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Hochland", "Steppe", "Wald"], [Occur.Modest, Occur.Modest, Occur.Common ]),
        new(2, "Shurinstrauch", ["W20 Knollen" ], ["Praios", "Rondra", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Steppe", "Grasland, Wiesen", "Wald"], [Occur.Rare, Occur.Rare, Occur.VeryRare ]),
        new(5, "Talaschin", ["W6 Flechten" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Gebirge", "Eis", "Wüste und Wüstenrand"], [Occur.Modest, Occur.Rare, Occur.VeryRare ]),
        new(8, "Tarnblatt", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Regenwald", "Wald"], [Occur.Rare, Occur.Rare ]),
        new(4, "Tarnele", ["1 Pflanze" ], ["Praios", "Rondra", "Efferd", "Travia", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Hochland", "Grasland, Wiesen", "Flussauen", "Sumpf und Moor", "Wald"], [Occur.VeryRare, Occur.Common, Occur.Common, Occur.Modest, Occur.Modest ]),
        new(12, "Thonnys", ["W6+4 Blätter" ], ["Praios", "Rondra", "Efferd", "Travia", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Steppe", "Wald"], [Occur.VeryRare, Occur.VeryRare ]),
        new(6, "Traschbart", ["W6 Flechten" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Sumpf und Moor", "Wald"], [Occur.Modest, Occur.Common ]),
        new(11, "Trichterwurzel", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wald", "Waldrand"], [Occur.Modest, Occur.VeryRare ]),
        new(1, "Tuur-Amash-Kelch", ["W6+3 Kelche" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wald", "Waldrand"], [Occur.Rare, Occur.VeryRare ]),
        new(2, "Ulmenwürger", ["W20 Blüten" ], ["Efferd", "Travia" ], ["Wald", "Waldrand"], [Occur.Modest, Occur.Rare ]),
        new(5, "Vierblättrige Einbeere", ["W6 Beeren" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Eis", "Gebirge", "Hochland", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Küste, Strand", "Flussauen", "Sumpf und Moor", "Wald", "Waldrand"], [Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.Modest, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.Common, Occur.Common ]),
        new(6, "Vragieswurzel", ["1 Wurzel" ], ["Efferd", "Travia" ], ["Gebirge", "Hochland", "Regenwald", "Wald"], [Occur.Rare, Occur.VeryRare, Occur.Modest, Occur.Rare ]),
        new(9, "Waldwebe", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Wald"], [Occur.Modest ]),
        new(1, "Wasserrausch", ["2W20 Blüten" ], ["Praios", "Rondra", "Efferd", "Travia", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche"], [Occur.Rare ]),
        new(12, "Winselgras", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Steppe", "Grasland, Wiesen"], [Occur.Modest, Occur.Rare ]),
        new(5, "Wirselkraut", ["W6+4 Blätter" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Steppe", "Grasland, Wiesen"], [Occur.Common, Occur.Modest ]),
        new(5, "Würgedattel", ["Keine Angabe im ZBA" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Regenwald", "Waldrand"], [Occur.VeryRare, Occur.VeryRare ]),
        new(6, "Yaganstrauch", ["W6 Nüsse" ], ["Boron" ], ["Hochland", "Wald"], [Occur.Rare, Occur.Modest ]),
        new(5, "Zithabar", ["3W20 Blätter" ], ["Praios", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Fluss- und Seeufer, Teiche", "Sumpf und Moor", "Wald", "Waldrand"], [Occur.Common, Occur.Modest, Occur.VeryRare, Occur.Rare ]),
        new(4, "Zunderschwamm", ["W6 Pilze" ], ["Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Regenwald", "Wald"], [Occur.Modest, Occur.Common ]),
        new(5, "Zwölfblatt", ["12 Stängel" ], ["Praios", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" ], ["Hochland", "Steppe", "Grasland, Wiesen", "Sumpf und Moor", "Wald"], [Occur.VeryRare, Occur.Modest, Occur.Rare, Occur.VeryRare, Occur.Modest])
    ];

    #endregion hard coded data

    public PlantSeeking()
    {
        SetSkill();
    }

    public sealed override void SetSkill()
    {
        SetSkill(["Wildnisleben", "Sinnenschärfe", "Pflanzenkunde"]);
    }

    public override void Roll()
    {
        int pointsLeft;
        int amount = 0;
        int mod = 0;
        int skillpoints = SkillPoints;
        string textResult = string.Empty;
        (int pointsResult, string stringResult) rolldata;

        if (Duration >= MinDuration * 2)
        {
            skillpoints = (int) (SkillPoints * 1.5);
        }

        if (Coincidence)
        {
            rolldata = Roll(_core.Mu, _core.In, _core.Ge, mod, skillpoints);
            pointsLeft = rolldata.pointsResult;
            bool canFind = true;
            List<Plant> plantsLootTable = [];
            List<ResultData> results = [];
            List<(string name, int difficulty)> plantDifficulties = [];
            List<Plant> plantsToRemove = [];

            foreach (Plant plant in _core.CurrentRegion.GetPossiblePlants(_core.CurrentLandscape.Name, _core.CurrentMonth))
            {
                int foundingDifficulty = GetFoundingDifficulty(plant);
                if (foundingDifficulty <= pointsLeft / 2 && plant.Loot[0] != "Keine Angabe im ZBA")
                {
                    plantsLootTable.AddRange(GenerateLootTableEntry(plant));
                    plantDifficulties.Add((plant.Name, foundingDifficulty));
                }
                else
                {
                    plantsToRemove.Add(plant);
                }
            }

            if (plantsToRemove.Count > 0)
            {
                foreach (Plant plant in plantsToRemove)
                {
                    plantsLootTable.RemoveAll(p => p.Name == plant.Name);
                }
                plantsToRemove.Clear();
            }

            if (plantsLootTable.Count == 0)
            {
                canFind = false;
            }

            while (canFind)
            {
                if (plantDifficulties.Count > 0 && pointsLeft / 2 >= (from Tuple in plantDifficulties select Tuple.difficulty).ToArray().Min())
                {
                    int index = plantsLootTable.Count == 1 ? 0 : _random.Next(plantsLootTable.Count);
                    (int[] quantityData, string[] quantityStrings) result = GenerateLootQuantity(plantsLootTable[index].Loot);

                    try
                    {
                        results.Single(r => r.Name == plantsLootTable[index].Name).IncreaseData(result.quantityData);
                    }
                    catch
                    {
                        ResultData resultData = new()
                        {
                            Name = plantsLootTable[index].Name,
                            Quantity = result
                        };
                        results.Add(resultData);
                    }

                    pointsLeft -= plantDifficulties.Single(p => p.name == plantsLootTable[index].Name).difficulty;

                    foreach (Plant plant in plantsLootTable.Where(plant => GetFoundingDifficulty(plant) > pointsLeft / 2 && !plantsToRemove.Contains(plant)))
                    {
                        plantsToRemove.Add(plant);
                    }

                    foreach (Plant plant in plantsToRemove)
                    {
                        plantsLootTable.RemoveAll(p => p.Name == plant.Name);
                        plantDifficulties.Remove(plantDifficulties.SingleOrDefault(t => t.name == plant.Name));
                    }
                    plantsToRemove.Clear();
                }
                else
                {
                    canFind = false;

                    for (int i = 0; i < results.Count; i++)
                    {
                        textResult += results[i].Name + ":\n";

                        for (int n = 0; n < results[i].Quantity.quantityData.Length; n++)
                        {
                            textResult += results[i].Quantity.quantityData[n] + results[i].Quantity.quantityStrings[n].TrimEnd(',') + "\n";
                        }
                    }
                }
            }
        }
        else
        {
            int[] quantityTotal = [];
            string[] quantityStrings = [];
            mod = GetFoundingDifficulty(CurrentPlant);
            rolldata = Roll(_core.Mu, _core.In, _core.Ge, mod, skillpoints);
            pointsLeft = rolldata.pointsResult;

            if (pointsLeft >= 0)
            {
                do
                {
                    amount++;
                    pointsLeft -= mod / 2;
                }
                while (pointsLeft >= 0);

                for (int i = 0; i < amount; i++)
                {
                    (int[] quantityData, string[] quantityStrings) result = GenerateLootQuantity(CurrentPlant.Loot);

                    if (quantityTotal.Length == 0)
                    {
                        quantityTotal = new int[result.quantityData.Length];
                        quantityStrings = result.quantityStrings;
                    }

                    for (int j = 0; j < quantityTotal.Length; j++)
                    {
                        quantityTotal[j] += result.quantityData[j];
                    }
                }

                for (int i = 0; i < quantityTotal.Length; i++)
                {
                    textResult += quantityTotal[i] + quantityStrings[i].TrimEnd(',') + "\n";
                }
            }
        }
        LastResult = new(rolldata.pointsResult.ToString(), rolldata.stringResult, textResult);
    }

    public Plant GetPlantByName(string plantName)
    {
        return Plants.Single(p => p.Name == plantName);
    }

    public int GetOccurrenceMod(Plant plant)
    {
        return (int) plant.OccurData.SingleOrDefault(o => o.Landscape == _core.CurrentLandscape.Name).Mod!;
    }

    public int GetFoundingDifficulty(Plant plant)
    {
        return plant.IdentificationMod + GetOccurrenceMod(plant);
    }

    private (int[] quantityData, string[] quantityStrings) GenerateLootQuantity(string[] loot)
    {
        Regex variableQuantityRegex = new(@"((\s|[0-9]+)?W[0-9]+(\+[0-9]+)?)");
        Regex fixedQuantityRegex = new("\\s?[0-9][0-9]?");
        Regex unitRegex = new(@"[0-9]+(\s\w{3,})(?!\s[0-9])");
        List<int> quantityData = [];
        List<string> quantityStrings = [];

        foreach (string l in loot)
        {
            if (variableQuantityRegex.IsMatch(l))
            {
                int[] quantityDatas = new int[3];
                Match match = variableQuantityRegex.Match(l);
                string unit = unitRegex.Match(l).Groups[1].Value;
                string[] s = match.Value.Split(['W', '+',]);

                for (int i = 0; i < s.Length; i++)
                {
                    quantityDatas[i] = string.IsNullOrWhiteSpace(s[i]) ? 1 : int.Parse(s[i]);
                }

                int rollData = DSA.Roll(quantityDatas[0], quantityDatas[1]).Sum();
                quantityData.Add(rollData + quantityDatas[2]);
                quantityStrings.Add(unit);
            }
            else
            {
                Match match = fixedQuantityRegex.Match(l);
                string unit = unitRegex.Match(l).Groups[1].Value;
                quantityData.Add(int.Parse(match.Value));
                quantityStrings.Add(unit);
            }
        }

        return (quantityData.ToArray(), quantityStrings.ToArray());
    }

    private Plant[] GenerateLootTableEntry(Plant plant)
    {
        int amount = GetOccurrenceMod(plant) switch
        {
            1 => 5,
            2 => 4,
            4 => 3,
            8 => 2,
            16 => 1,
            _ => 0,
        };

        Plant[] entry = new Plant[amount];

        for (int i = 0; i < amount; i++)
        {
            entry[i] = plant;
        }

        return entry;
    }
}
