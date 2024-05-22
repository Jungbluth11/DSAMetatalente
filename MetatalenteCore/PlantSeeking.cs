using System.Text.RegularExpressions;

namespace Metatalente.Core
{
    public class PlantSeeking : MetatalentBase
    {
        private readonly Random random = new();
        public bool Coincidence { get; set; } = false;
        public Plant CurrentPlant { get; set; } = Plants[0];

        #region hard coded data

        public static Plant[] Plants => new Plant[]
        {
            new(9, "Alraune", new string[]{ "1 Pflanze" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche", "Waldrand"}, new Occur[]{ Occur.Rare, Occur.Rare }),
            new(-5, "Alveranie", new string[]{ "12 Blätter, jeweils in der Farbe des Monats" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja" }, new string[] { "Eis", "Wüste und Wüstenrand", "Gebirge", "Hochland", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor", "Regenwald", "Wald", "Waldrand"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare }),
            new(4, "Arganstrauch", new string[]{ "1 Wurzel" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Sumpf und Moor", "Regenwald", "Wald"}, new Occur[]{ Occur.Rare, Occur.Modest, Occur.VeryRare }),
            new(6, "Atan-Kiefer", new string[]{ "W20 Stein Rinde, bei komplettem Abschälen Verdreifachung des Wertes" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Gebirge"}, new Occur[]{ Occur.Rare }),
            new(5, "Atmon", new string[]{ "W6 Büschel" }, new string[] { "Peraine" }, new string[] { "Hochland", "Steppe", "Flussauen", "Sumpf und Moor"}, new Occur[]{ Occur.Rare, Occur.Modest, Occur.VeryRare, Occur.VeryRare }),
            new(4, "Axorda-Baum", new string[]{ "1 Baum" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Gebirge", "Regenwald"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare }),
            new(15, "Basilamine", new string[]{ "W20+10 Schoten" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wald", "Waldrand"}, new Occur[]{ Occur.Modest, Occur.Rare }),
            new(6, "Belmart", new string[]{ "2W20 Blätter" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wald", "Waldrand"}, new Occur[]{ Occur.Modest, Occur.Rare }),
            new(4, "Blutblatt", new string[]{ "W20+2 Zweige pro 10 AsP der Quelle" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Eis", "Wüste und Wüstenrand", "Gebirge", "Hochland", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Küste, Strand", "Flussauen", "Sumpf und Moor", "Regenwald", "Wald", "Waldrand"}, new Occur[]{ Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare, Occur.Rare }),
            new(-2, "Boronie", new string[]{ "5 Blüten, die kurz vor dem Verblühen sind" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Grasland, Wiesen", "Regenwald"}, new Occur[]{ Occur.VeryRare, Occur.Rare }),
            new(15, "Boronsschlinge", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Regenwald", "Wald"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare }),
            new(5, "Carlog", new string[]{ "W6 Blüten", " mit je 1 Stempel" }, new string[] { "Efferd", "Peraine" }, new string[] { "Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor"}, new Occur[]{ Occur.Rare, Occur.VeryRare, Occur.Modest }),
            new(4, "Cheria-Kaktus", new string[]{ "W3 Stein Kaktusfleisch", " und pro Stein 3W6+8 Stacheln" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wüste und Wüstenrand"}, new Occur[]{ Occur.Rare }),
            new(6, "Chonchinis", new string[]{ "W20 Blätter" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Hochland", "Steppe", "Waldrand"}, new Occur[]{ Occur.VeryRare, Occur.Modest, Occur.Rare }),
            new(5, "Disdychonda", new string[]{ "4 Blätter" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Regenwald", "Wald"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare }),
            new(6, "Donf", new string[]{ "1 Stängel" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor"}, new Occur[]{ Occur.Rare, Occur.VeryRare, Occur.Modest }),
            new(3, "Dornrose", new string[]{ "Strauch mit W6 Blüten" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Grasland, Wiesen", "Wald"}, new Occur[]{ Occur.Common, Occur.Modest }),
            new(4, "Efeuer", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wald", "Höhle (feucht)", "Höhle (trocken)"}, new Occur[]{ Occur.Rare, Occur.Modest, Occur.Modest }),
            new(6, "Egelschreck", new string[]{ "2W20 Blätter" }, new string[] { "Rondra", "Efferd" }, new string[] { "Grasland, Wiesen", "Flussauen", "Sumpf und Moor", "Waldrand"}, new Occur[]{ Occur.Modest, Occur.Modest, Occur.Common, Occur.Rare }),
            new(2, "Eitriger Krötenschemel", new string[]{ "2W6 Pilzhäute" }, new string[] { "Efferd", "Travia", "Boron" }, new string[] { "Flussauen", "Sumpf und Moor", "Wald"}, new Occur[]{ Occur.Rare, Occur.Modest, Occur.VeryRare }),
            new(15, "Feuermoos und Efferdmoos", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Höhle (feucht)"}, new Occur[]{ Occur.Modest }),
            new(6, "Feuerschlick", new string[]{ "W6 Stein der Algen" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Küste, Strand", "Meer"}, new Occur[]{ Occur.Common, Occur.VeryRare }),
            new(5, "Finage", new string[]{ "Baum mit W20 Trieben", " und Bast" }, new string[] { "Boron", "Hesinde", "Firun", "Peraine" }, new string[] { "Grasland, Wiesen", "Regenwald", "Wald"}, new Occur[]{ Occur.VeryRare, Occur.Rare, Occur.Rare }),
            new(4, "Grüne Schleimschlange", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Flussauen", "Sumpf und Moor"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare }),
            new(6, "Gulmond", new string[]{ "2W6 Blätter" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Hochland", "Steppe", "Wald"}, new Occur[]{ Occur.Modest, Occur.Common, Occur.Modest }),
            new(8, "Hiradwurz", new string[]{ "1 Wurzel" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Steppe"}, new Occur[]{ Occur.Rare }),
            new(4, "Hollbeere", new string[]{ "2W6 Sträucher", " mit jeweils 2W6+5 Beeren", " und 2W6+3 Blätter der untersten Zweige" }, new string[] { "Rondra", "Efferd" }, new string[] { "Wald", "Waldrand"}, new Occur[]{ Occur.Modest, Occur.Common }),
            new(8, "Höllenkraut", new string[]{ "W10 Stein der Ranken" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Regenwald", "Wald", "Waldrand"}, new Occur[]{ Occur.Common, Occur.Rare, Occur.VeryRare }),
            new(7, "Horusche", new string[]{ "W6 erntereife Schoten", " mit je W3 Kernen" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wald"}, new Occur[]{ Occur.Rare }),
            new(2, "Ilmenblatt", new string[]{ "W20 Blätter", " und W20 Blüten" }, new string[] { "Travia", "Ingerimm" }, new string[] { "Gebirge", "Grasland, Wiesen", "Wald"}, new Occur[]{ Occur.VeryRare, Occur.Modest, Occur.Modest }),
            new(12, "Iribaarslilie", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Sumpf und Moor"}, new Occur[]{ Occur.VeryRare }),
            new(15, "Jagdgras", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Gebirge", "Hochland", "Steppe", "Wald"}, new Occur[]{ Occur.Modest, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare }),
            new(7, "Joruga", new string[]{ "1 Wurzel" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Rahja", "Namenlose Tage" }, new string[] { "Gebirge", "Grasland, Wiesen", "Wald"}, new Occur[]{ Occur.VeryRare, Occur.Rare, Occur.Modest }),
            new(6, "Kairan", new string[]{ "1 Halm" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche"}, new Occur[]{ Occur.Rare }),
            new(4, "Kajubo", new string[]{ "2W6 Knospen (Nur die Hälfte um den Strauch zu schonen)" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Küste, Strand", "Waldrand"}, new Occur[]{ Occur.Rare, Occur.Rare }),
            new(12, "Khôm- oder Mhanadiknolle", new string[]{ "1 Wurzel", " mit W6 Maß klarem Wasser" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wüste und Wüstenrand", "Steppe"}, new Occur[]{ Occur.VeryRare, Occur.Rare }),
            new(8, "Klippenzahn", new string[]{ "2W6 Stängel" }, new string[] { "Praios", "Rondra", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Gebirge", "Hochland"}, new Occur[]{ Occur.Modest, Occur.Modest }),
            new(10, "Kukuka", new string[]{ "1W3 x 20 Blätter" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Regenwald"}, new Occur[]{ Occur.Rare }),
            new(9, "Färberlotus (Gelber, Blauer, Roter und Rosa Lotus)", new string[]{ "2W6+1 Blüten" }, new string[] { "Praios", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche"}, new Occur[]{ Occur.Modest }),
            new(7, "Purpurner Lotus", new string[]{ "W6+1 Blüten" }, new string[] { "Praios", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche"}, new Occur[]{ Occur.Rare }),
            new(6, "Schwarzer Lotus", new string[]{ "W6 Blüten" }, new string[] { "Praios", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche"}, new Occur[]{ Occur.VeryRare }),
            new(8, "Grauer Lotus", new string[]{ "W6+1 Blüten" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche", "Sumpf und Moor"}, new Occur[]{ Occur.Rare, Occur.VeryRare }),
            new(10, "Weißer Lotus", new string[]{ "W6+1 Blüten" }, new string[] { "Praios", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche", "Sumpf und Moor"}, new Occur[]{ Occur.Rare, Occur.VeryRare }),
            new(10, "Weißgelber Lotus", new string[]{ "W3 Blüten" }, new string[] { "Praios", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche"}, new Occur[]{ Occur.Modest }),
            new(5, "Lulanie", new string[]{ "1 Blüte" }, new string[] { "Praios", "Rondra", "Rahja", "Namenlose Tage" }, new string[] { "Wald", "Waldrand"}, new Occur[]{ Occur.Rare, Occur.VeryRare }),
            new(15, "Madablüte", new string[]{ "1 Blüte" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Gebirge", "Steppe"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare }),
            new(4, "Menchal-Kaktus", new string[]{ "1 Kaktus", " mit W3 Maß Menchalsaft; bei 1 auf W20 außerdem mit W6 Blüten" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wüste und Wüstenrand", "Hochland"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare }),
            new(2, "Merach-Strauch", new string[]{ "2W20 reife Früchte" }, new string[] { "Efferd", "Travia" }, new string[] { "Regenwald", "Wald"}, new Occur[]{ Occur.Rare, Occur.VeryRare }),
            new(6, "Messergras", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wüste und Wüstenrand", "Hochland", "Steppe"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare, Occur.Rare }),
            new(10, "Mibelrohr", new string[]{ "2W6 Kolben" }, new string[] { "Praios", "Rondra", "Efferd", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche", "Flussauen", "Sumpf und Moor"}, new Occur[]{ Occur.Modest, Occur.Rare, Occur.VeryRare }),
            new(8, "Mirbelstein", new string[]{ "1 Wurzelknolle" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Hochland", "Grasland, Wiesen", "Wald"}, new Occur[]{ Occur.Rare, Occur.Modest, Occur.Common }),
            new(4, "Mirhamer Seidenliane", new string[]{ "1 Ranke", " mit W2+1 Knoten" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Gebirge", "Fluss- und Seeufer, Teiche", "Regenwald"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare, Occur.Rare }),
            new(5, "Bleichmohn (Weißer Mohn)", new string[]{ "W6 geschlossene Samenkapseln" }, new string[] { "Rondra" }, new string[] { "Gebirge"}, new Occur[]{ Occur.Rare }),
            new(-5, "Bunter Mohn", new string[]{ "1 geschlossene Samenkapsel" }, new string[] { "Travia" }, new string[] { "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Waldrand"}, new Occur[]{ Occur.Common, Occur.Modest, Occur.Rare }),
            new(1, "Grauer Mohn", new string[]{ "1 geschlossene Samenkapsel und 1 Blüte" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Gebirge"}, new Occur[]{ Occur.VeryRare }),
            new(3, "Purpurmohn", new string[]{ "1 geschlossene Samenkapsel" }, new string[] { "Rahja" }, new string[] { "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Wald"}, new Occur[]{ Occur.Rare, Occur.VeryRare, Occur.VeryRare }),
            new(5, "Schwarzer Mohn", new string[]{ "2 Blätter", " und 1 geschlossene Samenkapsel" }, new string[] { "Efferd", "Travia", "Boron" }, new string[] { "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Flussauen", "Wald", "Waldrand"}, new Occur[]{ Occur.VeryCommon, Occur.VeryCommon, Occur.VeryCommon, Occur.VeryCommon, Occur.VeryCommon, Occur.VeryCommon }),
            new(10, "Tigermohn", new string[]{ "1 geschlossene Samenkapsel" }, new string[] { "Travia" }, new string[] { "Grasland, Wiesen", "Flussauen", "Waldrand"}, new Occur[]{ Occur.Rare, Occur.Rare, Occur.Rare }),
            new(13, "Morgendornstrauch", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Sumpf und Moor"}, new Occur[]{ Occur.Rare }),
            new(1, "Naftanstaude", new string[]{ "1 Staude" }, new string[] { "Praios", "Rondra", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Steppe", "Grasland, Wiesen", "Waldrand"}, new Occur[]{ Occur.VeryRare, Occur.Rare, Occur.VeryRare }),
            new(4, "Neckerkraut", new string[]{ "W20+5 Blätter" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Grasland, Wiesen", "Küste, Strand", "Sumpf und Moor"}, new Occur[]{ Occur.Rare, Occur.Modest, Occur.VeryRare }),
            new(6, "Nothilf", new string[]{ "W20+2 Blüten", " und 2W20+10 Blätter" }, new string[] { "Praios", "Peraine" }, new string[] { "Gebirge", "Wald"}, new Occur[]{ Occur.Rare, Occur.Rare }),
            new(10, "Olginwurz", new string[]{ "W3 Moosballen" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Gebirge", "Hochland", "Wald"}, new Occur[]{ Occur.Rare, Occur.Rare, Occur.Rare }),
            new(4, "Orazal", new string[]{ "W6 verholzte Stängel" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Regenwald", "Wald"}, new Occur[]{ Occur.Rare, Occur.VeryRare }),
            new(4, "Orklandbovist", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Hochland", "Steppe", "Wald"}, new Occur[]{ Occur.Rare, Occur.VeryRare, Occur.VeryRare }),
            new(6, "Pestsporenpilz", new string[]{ "1 Pilzhaut" }, new string[] { "Efferd", "Travia", "Boron", "Peraine", "Ingerimm", "Rahja" }, new string[] { "Grasland, Wiesen", "Sumpf und Moor", "Wald"}, new Occur[]{ Occur.Rare, Occur.Rare, Occur.VeryRare }),
            new(10, "Phosphorpilz", new string[]{ "W6 Stein Geflechtstücke" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Höhle (feucht)", "Höhle (trocken)"}, new Occur[]{ Occur.Modest, Occur.Rare }),
            new(12, "Quasselwurz", new string[]{ "1 Wurzel" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wald"}, new Occur[]{ Occur.Rare }),
            new(6, "Quinja", new string[]{ "W3+2 Beeren" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Regenwald", "Wald", "Waldrand"}, new Occur[]{ Occur.Common, Occur.Modest, Occur.Rare }),
            new(5, "Rahjalieb", new string[]{ "2W6 Blätter" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Grasland, Wiesen", "Sumpf und Moor", "Regenwald", "Wald"}, new Occur[]{ Occur.Modest, Occur.Modest, Occur.Common, Occur.Modest }),
            new(7, "Rattenpilz", new string[]{ "1 Pilz" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Gebirge", "Hochland", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Küste, Strand", "Flussauen", "Sumpf und Moor", "Regenwald", "Wald", "Waldrand"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare }),
            new(3, "Rauschgurke", new string[]{ "3W6 reife Rauschgurken" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wald", "Waldrand"}, new Occur[]{ Occur.Modest, Occur.Rare }),
            new(7, "Rote Pfeilblüte", new string[]{ "W6 Blüten" }, new string[] { "Peraine", "Ingerimm", "Rahja" }, new string[] { "Sumpf und Moor", "Regenwald", "Wald"}, new Occur[]{ Occur.Modest, Occur.Modest, Occur.VeryRare }),
            new(10, "Roter Drachenschlund", new string[]{ "W6 Blätter" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche", "Waldrand"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare }),
            new(12, "Sansaro", new string[]{ "1 Pflanze" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Küste, Strand", "Meer"}, new Occur[]{ Occur.Common, Occur.Rare }),
            new(-2, "Satuariensbusch", new string[]{ "4W20 Blätter,", " W20 Blüten,", " W20 Früchte,", " W3 Flux Saft" }, new string[] { "Praios", "Efferd", "Travia", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wald", "Waldrand"}, new Occur[]{ Occur.Modest, Occur.Modest }),
            new(3, "Schlangenzünglein", new string[]{ "Saft einer Pflanze" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche", "Sumpf und Moor", "Regenwald"}, new Occur[]{ Occur.Rare, Occur.Rare, Occur.VeryRare }),
            new(6, "Schleichender Tod", new string[]{ "W6 Blüten" }, new string[] { "Ingerimm", "Rahja" }, new string[] { "Fluss- und Seeufer, Teiche", "Waldrand"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare }),
            new(3, "Schleimiger Sumpfknöterich", new string[]{ "2W6 Pilze" }, new string[] { "Praios", "Rondra", "Efferd", "Travia" }, new string[] { "Wald", "Waldrand"}, new Occur[]{ Occur.Rare, Occur.VeryRare }),
            new(12, "Schlinggras", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Sumpf und Moor"}, new Occur[]{ Occur.Rare }),
            new(3, "Schwarmschwamm", new string[]{ "1 Schwamm", " und W2 Samenkörper" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche", "Flussauen"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare }),
            new(2, "Schwarzer Wein", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Hochland", "Steppe", "Wald"}, new Occur[]{ Occur.Modest, Occur.Modest, Occur.Common }),
            new(2, "Shurinstrauch", new string[]{ "W20 Knollen" }, new string[] { "Praios", "Rondra", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Steppe", "Grasland, Wiesen", "Wald"}, new Occur[]{ Occur.Rare, Occur.Rare, Occur.VeryRare }),
            new(5, "Talaschin", new string[]{ "W6 Flechten" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Gebirge", "Eis", "Wüste und Wüstenrand"}, new Occur[]{ Occur.Modest, Occur.Rare, Occur.VeryRare }),
            new(8, "Tarnblatt", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Regenwald", "Wald"}, new Occur[]{ Occur.Rare, Occur.Rare }),
            new(4, "Tarnele", new string[]{ "1 Pflanze" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Hochland", "Grasland, Wiesen", "Flussauen", "Sumpf und Moor", "Wald"}, new Occur[]{ Occur.VeryRare, Occur.Common, Occur.Common, Occur.Modest, Occur.Modest }),
            new(12, "Thonnys", new string[]{ "W6+4 Blätter" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Steppe", "Wald"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare }),
            new(6, "Traschbart", new string[]{ "W6 Flechten" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Sumpf und Moor", "Wald"}, new Occur[]{ Occur.Modest, Occur.Common }),
            new(11, "Trichterwurzel", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wald", "Waldrand"}, new Occur[]{ Occur.Modest, Occur.VeryRare }),
            new(1, "Tuur-Amash-Kelch", new string[]{ "W6+3 Kelche" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wald", "Waldrand"}, new Occur[]{ Occur.Rare, Occur.VeryRare }),
            new(2, "Ulmenwürger", new string[]{ "W20 Blüten" }, new string[] { "Efferd", "Travia" }, new string[] { "Wald", "Waldrand"}, new Occur[]{ Occur.Modest, Occur.Rare }),
            new(5, "Vierblättrige Einbeere", new string[]{ "W6 Beeren" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Eis", "Gebirge", "Hochland", "Steppe", "Grasland, Wiesen", "Fluss- und Seeufer, Teiche", "Küste, Strand", "Flussauen", "Sumpf und Moor", "Wald", "Waldrand"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.Modest, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.VeryRare, Occur.Common, Occur.Common }),
            new(6, "Vragieswurzel", new string[]{ "1 Wurzel" }, new string[] { "Efferd", "Travia" }, new string[] { "Gebirge", "Hochland", "Regenwald", "Wald"}, new Occur[]{ Occur.Rare, Occur.VeryRare, Occur.Modest, Occur.Rare }),
            new(9, "Waldwebe", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Wald"}, new Occur[]{ Occur.Modest }),
            new(1, "Wasserrausch", new string[]{ "2W20 Blüten" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche"}, new Occur[]{ Occur.Rare }),
            new(12, "Winselgras", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Steppe", "Grasland, Wiesen"}, new Occur[]{ Occur.Modest, Occur.Rare }),
            new(5, "Wirselkraut", new string[]{ "W6+4 Blätter" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Steppe", "Grasland, Wiesen"}, new Occur[]{ Occur.Common, Occur.Modest }),
            new(5, "Würgedattel", new string[]{ "Keine Angabe im ZBA" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Regenwald", "Waldrand"}, new Occur[]{ Occur.VeryRare, Occur.VeryRare }),
            new(6, "Yaganstrauch", new string[]{ "W6 Nüsse" }, new string[] { "Boron" }, new string[] { "Hochland", "Wald"}, new Occur[]{ Occur.Rare, Occur.Modest }),
            new(5, "Zithabar", new string[]{ "3W20 Blätter" }, new string[] { "Praios", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Fluss- und Seeufer, Teiche", "Sumpf und Moor", "Wald", "Waldrand"}, new Occur[]{ Occur.Common, Occur.Modest, Occur.VeryRare, Occur.Rare }),
            new(4, "Zunderschwamm", new string[]{ "W6 Pilze" }, new string[] { "Praios", "Rondra", "Efferd", "Travia", "Boron", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Regenwald", "Wald"}, new Occur[]{ Occur.Modest, Occur.Common }),
            new(5, "Zwölfblatt", new string[]{ "12 Stängel" }, new string[] { "Praios", "Hesinde", "Firun", "Tsa", "Phex", "Peraine", "Ingerimm", "Rahja", "Namenlose Tage" }, new string[] { "Hochland", "Steppe", "Grasland, Wiesen", "Sumpf und Moor", "Wald"}, new Occur[]{ Occur.VeryRare, Occur.Modest, Occur.Rare, Occur.VeryRare, Occur.Modest })
        };

        #endregion hard coded data

        public PlantSeeking(Core core)
        {
            this.core = core;
        }

        public override void SetSkill()
        {
            SetSkill(new string[] { "Wildnisleben", "Sinnenschärfe", "Pflanzenkunde" });
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
                skillpoints = (int)(SkillPoints * 1.5);
            }

            if (Coincidence)
            {
                rolldata = Roll(core.MU, core.IN, core.GE, mod, skillpoints);
                pointsLeft = rolldata.pointsResult;
                pointsLeft = 20;
                bool canFind = true;
                List<Plant> PlantsLootTable = new();
                List<ResultData> Results = new();
                List<(string name, int difficulty)> plantDifficulties = new();
                List<Plant> plantsToRemove = new();
                foreach (Plant plant in core.CurrentRegion.GetPossiblePlants(core.CurrentLandscape.Name, core.CurrentMonth))
                {
                    int foundingDifficulty = GetFoundingDifficulty(plant);
                    if (foundingDifficulty <= pointsLeft / 2 && plant.Loot[0] != "Keine Angabe im ZBA")
                    {
                        PlantsLootTable.AddRange(GenerateLootTableEntry(plant));
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
                        PlantsLootTable.RemoveAll(p => p.Name == plant.Name);
                    }
                    plantsToRemove.Clear();
                }
                if (PlantsLootTable.Count == 0)
                {
                    canFind = false;
                }
                while (canFind)
                {
                    if (plantDifficulties.Count > 0 && pointsLeft / 2 >= (from Tuple in plantDifficulties select Tuple.difficulty).ToArray().Min())
                    {
                        int index = PlantsLootTable.Count == 1 ? 0 : random.Next(PlantsLootTable.Count);
                        (int[] quantityData, string[] quantityStrings) result = GenerateLootQuantity(PlantsLootTable[index].Loot);
                        try
                        {
                            Results.Single(r => r.Name == PlantsLootTable[index].Name).IncreaseData(result.quantityData);
                        }
                        catch
                        {
                            ResultData resultData = new()
                            {
                                Name = PlantsLootTable[index].Name,
                                quantity = result
                            };
                            Results.Add(resultData);
                        }
                        pointsLeft -= plantDifficulties.Single(p => p.name == PlantsLootTable[index].Name).difficulty;
                        foreach (Plant plant in PlantsLootTable)
                        {
                            if (GetFoundingDifficulty(plant) > pointsLeft / 2 && !plantsToRemove.Contains(plant))
                            {
                                plantsToRemove.Add(plant);
                            }
                        }
                        foreach (Plant plant in plantsToRemove)
                        {
                            PlantsLootTable.RemoveAll(p => p.Name == plant.Name);
                            plantDifficulties.Remove(plantDifficulties.SingleOrDefault(t => t.name == plant.Name));
                        }
                        plantsToRemove.Clear();
                    }
                    else
                    {
                        canFind = false;
                        for (int i = 0; i < Results.Count; i++)
                        {
                            textResult += Results[i].Name + ":\n";
                            for (int n = 0; n < Results[i].quantity.quantityData.Length; n++)
                            {
                                textResult += Results[i].quantity.quantityData[n] + Results[i].quantity.quantityStrings[n].TrimEnd(',') + "\n";
                            }
                        }
                    }
                }
            }
            else
            {
                int[] quantityTotal = Array.Empty<int>();
                string[] quantityStrings = Array.Empty<string>();
                mod = GetFoundingDifficulty(CurrentPlant);
                rolldata = Roll(core.MU, core.IN, core.GE, mod, skillpoints);
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
            LastResult = new Result(rolldata.pointsResult.ToString(), rolldata.stringResult, textResult);
        }

        public Plant GetPlantByName(string plantName)
        {
            return Plants.Single(p => p.Name == plantName);
        }

        public int GetOccurrenceMod(Plant plant)
        {
            return (int)plant.OccurData.SingleOrDefault(o => o.Landscape == core.CurrentLandscape.Name).Mod;
        }

        public int GetFoundingDifficulty(Plant plant)
        {
            return plant.IdentificationMod + GetOccurrenceMod(plant);
        }

        private (int[] quantityData, string[] quantityStrings) GenerateLootQuantity(string[] loot)
        {
            Regex variableQuantityRegex = new("((\\s|[0-9]+)?W[0-9]+(\\+[0-9]+)?)");
            Regex fixedQuantityRegex = new("\\s?[0-9][0-9]?");
            Regex unitRegex = new("[0-9]+(\\s\\w{3,})(?!\\s[0-9])");
            List<int> quantityData = new();
            List<string> quantityStrings = new();
            foreach (string l in loot)
            {
                if (variableQuantityRegex.IsMatch(l))
                {
                    int[] quantityDatas = new int[3];
                    Match match = variableQuantityRegex.Match(l);
                    string unit = unitRegex.Match(l).Groups[1].Value;
                    string[] s = match.Value.Split(new char[] { 'W', '+', });
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
}