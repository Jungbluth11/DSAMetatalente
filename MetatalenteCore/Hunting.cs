// Ignore Spelling: Scharfschuetze Meisterschuetze

using System.Text.RegularExpressions;
using System.Xml;
using DSAUtils.HeldentoolInterop;

namespace Metatalente.Core;

public class Hunting : MetatalentBase
{
    private Variants _variant = Variants.Pirschjagd;
    public Variants Variant
    {
        get => _variant;
        set
        {
            _variant = value;
            MinDuration = _variant == Variants.Pirschjagd ? 60 : 90;
            SetSkill();
        }
    }
    public bool IsScharfschuetze { get; set; }
    public bool IsMeisterschuetze { get; set; }
    public bool Coincidence { get; set; } = false;
    public enum Variants
    {
        Pirschjagd,
        Ansitzjagd
    };
    public Animal CurrentAnimal { get; set; } = Animals[0];
    public Weapon UsedWeapon { get; set; } = Weapons[0];

    #region hard coded data

    public static Weapon[] Weapons =>
    [
        new("Wurfscheibe","Wurfmesser",20 ),
        new("Wurfring","Wurfmesser",20 ),
        new("Dschadra","Wurfspeer",40 ),
        new("Efferdbart","Wurfspeer",25 ),
        new("Holzspeer","Wurfspeer",40 ),
        new("Speer","Wurfspeer",40 ),
        new("Stabschleuder","Wurfspeer",60 ),
        new("Wurfspeer","Wurfspeer",40 ),
        new("Schneidzahn","Wurfbeil",30 ),
        new("Wurfbeil","Wurfbeil",25 ),
        new("Wurfkeule","Wurfbeil",40 ),
        new("Arbalette","Armbrust",100 ),
        new("Arbalone","Armbrust",250 ),
        new("Balestra","Armbrust",60 ),
        new("Balestrina","Armbrust",25 ),
        new("Balläster","Armbrust",100 ),
        new("Eisenwalder","Armbrust",40 ),
        new("Leichte Armbrust","Armbrust",60 ),
        new("Windenarmbrust","Armbrust",180 ),
        new("Elfenbogen","Bogen",200 ),
        new("Kompositbogen","Bogen",80 ),
        new("Kriegsbogen","Bogen",150 ),
        new("Kurzbogen","Bogen",60 ),
        new("Langbogen","Bogen",200 ),
        new("Ork. Reiterbogen","Bogen",100 ),
        new("Fledermaus","Schleuder",25 ),
        new("Schleuder","Schleuder",40 ),
        new("Diskus","Diskus",60 ),
        new("Jagddiskus","Diskus",60 ),
        new("Kampfdiskus","Diskus",60 )
    ];

    public static Animal[] Animals =>
    [
        new(15, "Löwenaffe", ["0.5 bis 2 Rationen Fleisch", "Fell (besser)"]),
        new(15, "Moosaffe", ["0.5 bis 2 Rationen Fleisch", "Fell (besser)"]),
        new(2, "Otan-Otan", ["bis 40 Rationen Fleisch (zäh)", "Fell (besser)"]),
        new(15, "Purzelaffe", ["0.5 bis 2 Rationen Fleisch", "Fell (besser)"]),
        new(5, "Riesenaffe", ["190 Rationen Fleisch (zäh)", "Fell (besser)"]),
        new(2, "Sumpfranze", ["25 Rationen Fleisch (ungenießbar)", "Fell (wertlos)" ]),
        new(12, "Al'Kebir-Antilope", ["140 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" ]),
        new(12, "Gabelantilope", ["9 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" ]),
        new(12, "Halmar-Antilope", ["15 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" ]),
        new(10, "Karen", ["40 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (besser)" ]),
        new(12, "Springbock", ["19 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" ]),
        new(10, "Vy'Tagga-Antilope", ["15 Rationen Fleisch (ungenießbar)", "Geweih (Trophäe)", "Fell (besser) oder Leder (besser)" ]),
        new(6, "Baumbär", ["15 bis 20 Rationen Fleisch (zäh)", "Fell (teuer)" ]),
        new(12, "Baumwürger", ["30 Rationen Fleisch (zäh)", "Fell (teuer)" ]),
        new(5, "Borkenbär", ["110 Rationen Fleisch", "Fell (einfach)" ]),
        new(12, "Firunsbär", ["600 Rationen Fleisch", "Fell (Luxusartikel)" ]),
        new(6, "Höhlenbär", ["350 Rationen Fleisch", "Fell (teuer)" ]),
        new(7, "Orklandbär", ["50 Rationen Fleisch", "Fell (billig)" ]),
        new(4, "Schwarzbär", ["380 Rationen Fleisch", "Fell (Luxusartikel)" ]),
        new(9, "Schneedachs", ["5 Rationen Fleisch", "Fell (einfach)" ]),
        new(12, "Streifendachs", ["4 Rationen Fleisch", "Fell (einfach)" ]),
        new(7, "Brabaker Waldelefant", ["1500 bis 2000 Rationen Fleisch", "Haut (Leder, teuer)", "Stoßzähne (5 D je Stein / pro Zahn bis zu 15 Stein)" ]),
        new(10, "Mammut", ["2800 bis 3400 Rationen Fleisch", "Haut (Fell oder Leder, teuer)", "Stoßzähne (4 D je Stein / pro Zahn bis zu 40 Stein)" ]),
        new(8, "Mastodon", ["1600 bis 2400 Rationen Fleisch", "Haut (Fell oder Leder, teuer)", "Stoßzähne (3 D je Stein / pro Zahn bis zu 20 Stein)" ]),
        new(4, "Zwergelefant", ["500 Rationen Fleisch", "Haut (Fell oder Leder, teuer)", "Stoßzähne (3 D je Stein / pro Zahn bis zu 30 Stein)" ]),
        new(13, "Gelbfuchs", ["3 Rationen Fleisch (sehr zäh)", "Pelz (billig)" ]),
        new(9, "Rotfuchs", ["3 Rationen Fleisch (sehr zäh)", "Pelz (teuer)" ]),
        new(15, "Blaufuchs", ["3 Rationen Fleisch (sehr zäh)", "Pelz (Luxusartikel)" ]),
        new(6, "Auerhahn", ["3 Rationen Fleisch" ]),
        new(5, "Fasan", ["1 bis 2 Rationen Fleisch", "Fell (besser)" ]),
        new(5, "Regenbogenfasan", ["1 Ration Fleisch", "Fell (besser)" ]),
        new(5, "Rebhuhn", ["1 bis 2 Rationen Fleisch" ]),
        new(5, "Trappe", ["5 Rationen Fleisch" ]),
        new(7, "Karnickel", ["1 bis 2 Rationen Fleisch", "Fell (besser)" ]),
        new(7, "Orklandkarnickel", ["1 bis 2 Rationen Fleisch", "Fell (besser)" ]),
        new(7, "Pfeifhase", ["1 bis 2 Rationen Fleisch", "Fell (besser)" ]),
        new(4, "Riesenlöffler", ["2 Rationen Fleisch", "Fell (besser)" ]),
        new(12, "Rotpüschel", ["1 Ration Fleisch", "Fell (besser)" ]),
        new(10, "Silberbock", ["1 bis 2 Rationen Fleisch", "Fell (besser)" ]),
        new(4, "Elch", ["450 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" ]),
        new(8, "Firunshirsch", ["60 Rationen Fleisch", "Geweih (Trophäe)", "Fell (teuer, in reinem Weiß Luxusware) oder Leder (teuer)" ]),
        new(6, "Kronenhirsch", ["110 Rationen Fleisch", "Geweih (Trophäe)", "Fell (teuer) oder Leder (teuer)" ]),
        new(5, "Rehwild", ["11 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" ]),
        new(10, "Steppenhund", ["12 Rationen Fleisch (zäh)", "Fell (billig)" ]),
        new(20, "Wildkatze", ["3 Rationen Fleisch", "Fell (einfach)" ]),
        new(12, "Berglöwe", ["65 Rationen Fleisch", "Fell (Luxusartikel)" ]),
        new(6, "Sandlöwe", ["100 Rationen Fleisch", "Fell (Luxusartikel)" ]),
        new(12, "Lioma", ["90 Rationen Fleisch", "Fell (Luxusartikel)" ]),
        new(9, "Waldlöwe", ["60 Rationen Fleisch", "Fell (Luxusartikel)" ]),
        new(20, "Firnluchs", ["12 Rationen Fleisch", "Fell (teuer)" ]),
        new(15, "Gänseluchs", ["3 Rationen Fleisch", "Fell (einfach)" ]),
        new(15, "Raschtulsluchs", ["10 Rationen Fleisch", "Fell (besser)" ]),
        new(15, "Rotluchs", ["10 Rationen Fleisch", "Fell (besser)" ]),
        new(15, "Sonnenluchs", ["10 Rationen Fleisch", "Fell (besser)" ]),
        new(15, "Fleckenpanther", ["40 Rationen Fleisch", "Fell (Luxusartikel)" ]),
        new(15, "Khômgepard", ["15 Rationen Fleisch", "Fell (Luxusartikel)" ]),
        new(8, "Auerochse", ["550 Rationen Fleisch", "Hörner (Trophäe)", "Haut (einfach)", "Leder (teuer)" ]),
        new(7, "Firnyak", ["140 Rationen Fleisch", "Hörner (Trophäe)", "Fell (teuer, in reinem weiß Luxusware) oder Leder (teuer)" ]),
        new(4, "Ongalobulle", ["300 Rationen Fleisch", "Hörner (Trophäe)", "Haut (einfach)", "Leder (besser)" ]),
        new(12, "Steppenrind", ["600 Rationen Fleisch", "Hörner (Trophäe)", "Haut (einfach)", "Leder (teuer)" ]),
        new(8, "Meerkalb", ["bis 250 Rationen Fleisch", "Tran (5-7 D)", "Haut (Leder, besser)" ]),
        new(13, "Seetiger", ["250 bis 300 Rationen Fleisch", "Tran (12-15 D)", "Haut (Leder, besser)", "Bein (teuer)", "Ambra (3 D)" ]),
        new(10, "Dschungeltiger", ["100 Rationen Fleisch", "Fell (Luxusartikel)", "Zähne (Trophäe)" ]),
        new(10, "Silberlöwe", ["100 Rationen Fleisch", "Fell (Luxusartikel)", "Zähne (Trophäe)" ]),
        new(10, "Steppentiger", ["100 Rationen Fleisch", "Fell (Luxusartikel)", "Zähne (Trophäe)" ]),
        new(2, "Wildschwein", ["bis 130 Rationen Fleisch", "Hauer (Trophäe)", "Fell (billig) oder Leder (einfach)" ]),
        new(2, "Warzenschwein", ["bis 130 Rationen Fleisch", "Hauer (Trophäe)", "Fell (billig) oder Leder (einfach)" ]),
        new(4, "Strauß", ["50 Rationen Fleisch", "15 bis 60 Eier im Nest (jedes Ei eine Ration)", "Gefieder (teuer)" ]),
        new(6, "Vielfraß", ["8 Rationen Fleisch (ungenießbar)", "Fell (besser)" ]),
        new(5, "Baumschleimer", ["6 Rationen Fleisch (ungenießbar)", "Fell (wertlos)" ]),
        new(20, "Gebirgsbock", ["60 Rationen Fleisch (zäh)", "Fell (einfach)", "Horn (Trophäe)" ]),
        new(2, "Felsrobbe", ["bis 45 Rationen Fleisch", "Talg (Fett", "Öl", "5 S)", "Tran (1 D)", "Haut (Fell, besser, von Jungtieren teuer)" ]),
        new(7, "Maraskanisches Stachelschwein", ["1 Ration Fleisch", "2W20+30 Stacheln (FF-Probe, um sie inklusive Giftdrüse aus dem Tier zu ziehen)" ]),
        new(7, "Klippechse", ["10 Rationen Fleisch", "Haut (Leder, teuer)", "Eier (diverse alchemistische Anwendungen)" ]),
        new(5, "Schreckkatze",  ["12 Rationen Fleisch (nur für Orks und Goblins genießbar)", "Fell wertlos"])
    ];

    #endregion hard coded data

    public Hunting()
    {
        SetSkill();
    }

    public sealed override void SetSkill()
    {
        string skill = Variant == Variants.Pirschjagd ? "Schleichen" : "Sich Verstecken";

        SetSkill(["Wildnisleben", "Tierkunde", "Fährtensuchen", skill, UsedWeapon.UsedSkill]);

        if (_core.Character == null)
        {
            return;
        }

        if (_core.Character.Sonderfertigkeiten.Contains("Scharfschütze"))
        {
            IsScharfschuetze = true;
        }

        if (_core.Character.Sonderfertigkeiten.Contains("Meisterschütze"))
        {
            IsMeisterschuetze = true;
        }
    }

    public override void Roll()
    {
        int mod = 0;
        int lowRangeMod = 0;
        string pointsLeft = string.Empty;
        string diceResult = string.Empty;
        string textResult = string.Empty;

        if (_core.CurrentRegion.WildlifeMod is null)
        {
            return;
        }

        if (Coincidence)
        {
            mod += 7 + (int) _core.CurrentRegion.WildlifeMod;
        }
        else
        {
            mod += CurrentAnimal.Difficulty + (int) _core.CurrentRegion.WildlifeMod;
        }

        lowRangeMod = UsedWeapon.MaxRange switch
        {
            20 => 7,
            < 51 => 3,
            _ => lowRangeMod
        };

        mod += lowRangeMod;

        if (IsMeisterschuetze)
        {
            mod -= 7;
        }
        else if (IsScharfschuetze)
        {
            mod -= 3;
        }

        if (mod < lowRangeMod)
        {
            mod = lowRangeMod;
        }

        int intervall = Duration / MinDuration;

        if (Coincidence)
        {
            int amount = 0;

            for (int i = 0; i < intervall; i++)
            {
                (int pointsResult, string[] resultStrings) = GetRollData(mod, i, intervall > 1);
                amount += pointsResult;
                pointsLeft += resultStrings[0];
                diceResult += resultStrings[1];
            }

            textResult += amount + " Rationen";
        }
        else
        {
            int huntedAnimals = 0;
            int[] quantityTotal = [];
            string[] quantityStrings = [];
            for (int i = 0; i < intervall; i++)
            {
                (int pointsResult, string[] resultStrings) = GetRollData(mod, i, intervall > 1);
                pointsLeft += resultStrings[0];
                diceResult += resultStrings[1];

                if (pointsResult <= 0)
                {
                    continue;
                }

                huntedAnimals++;
                (int[] quantityData, string[] quantityStrings) result = GenerateLootQuantity(CurrentAnimal.Loot);

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

            textResult = huntedAnimals + (huntedAnimals > 1 ? " Tiere" : " Tier") + "\n\n";

            for (int i = 0; i < quantityTotal.Length; i++)
            {
                textResult += quantityStrings[i].Replace("<value>", quantityTotal[i].ToString()) + "\n";
            }
        }

        LastResult = new(pointsLeft, diceResult, textResult);
    }

    public Animal GetAnimalByName(string animalName)
    {
        return Animals.Single(a => a.Name == animalName);
    }

    public Weapon GetWeaponByName(string weaponName)
    {
        return Weapons.Single(w => w.Name == weaponName);
    }

    public void LoadWeaponFromCharacter()
    {
        if (_core.Character == null)
        {
            return;
        }

        try
        {
            XmlDocument xml = new();
            xml.LoadXml(_core.Character.XML);
            string number = xml.SelectSingleNode("//heldenausruestung[@name='jagtwaffe']")!.Attributes!["nummer"]!.Value;
            XmlNode weapon = xml.SelectSingleNode("//heldenausruestung[@name='fkwaffe" + number + "']")!;
            Ability weaponAbility = _core.Character.Talente.Single(a => a.Name == weapon.Attributes!["talent"]!.Value);
            UsedWeapon = GetWeaponByName(weapon.Attributes!["waffenname"]!.Value);
            _core.SkillWeapon = weaponAbility.Wert;
            IsScharfschuetze = _core.Character.Sonderfertigkeiten.Contains("Scharfschütze (" + weaponAbility.Name + ")");
            IsMeisterschuetze = _core.Character.Sonderfertigkeiten.Contains("Meisterschütze (" + weaponAbility.Name + ")");
        }
        catch
        {
            throw new("No used Weapon defined");
        }
    }

    private (int pointsResult, string[] resultStrings) GetRollData(int mod, int intervall, bool multiple)
    {
        string[] resultStrings = new string[2];
        string count = string.Empty;
        (int pointsResult, string stringResult) = Roll(_core.Mu, _core.In, _core.Ge, mod);

        if (multiple)
        {
            count += "Wurf " + (intervall + 1) + ": ";
        }

        resultStrings[0] = count + pointsResult + "\n";
        resultStrings[1] = count + stringResult + "\n";
        return (pointsResult, resultStrings);
    }

    private (int[] quantityData, string[] quantityStrings) GenerateLootQuantity(string[] loot)
    {
        const string replacement = "<value>";
        Regex variableQuantityRegex = new(@"([0-9]+)?\s?bis\s?(?:zu\s)?([0-9]+)");
        Regex fixedQuantityRegex = new("[0-9]+");
        Regex removeBracketsRegex = new(@"\(((?!\)).)*\)\s?");
        Regex unitRegex = new(@"[0-9]+(\s\w{3,})(?!\s[0-9])");
        Random random = new();
        List<int> quantityData = [];
        List<string> quantityStrings = [];

        foreach (string l in loot)
        {
            if (l.Contains("bis"))
            {
                Match match = variableQuantityRegex.Match(l);
                int min = match.Groups[1].Value == string.Empty ? 1 : int.Parse(match.Groups[1].Value);
                int amount = random.Next(min, int.Parse(match.Groups[2].Value));
                quantityData.Add(amount);

                if (removeBracketsRegex.IsMatch(l))
                {
                    string unit = unitRegex.Match(l).Groups[1].Value;
                    string quantityString = variableQuantityRegex.Replace(l, replacement);
                    string removedBracketsString = removeBracketsRegex.Replace(quantityString, string.Empty);
                    removedBracketsString += removedBracketsString.Contains(unit) ? string.Empty : replacement + unit;
                    quantityStrings.Add(removedBracketsString);
                }
                else
                {
                    quantityStrings.Add(variableQuantityRegex.Replace(l, replacement));
                }
            }
            else if (fixedQuantityRegex.IsMatch(l))
            {
                Match match = fixedQuantityRegex.Match(l);
                quantityData.Add(int.Parse(match.Value));

                if (removeBracketsRegex.IsMatch(l))
                {
                    string removedBracketsString = removeBracketsRegex.Replace(l, string.Empty);
                    quantityStrings.Add(fixedQuantityRegex.Replace(removedBracketsString, replacement));
                }
                else
                {
                    quantityStrings.Add(fixedQuantityRegex.Replace(l, replacement));
                }
            }
            else
            {
                quantityData.Add(1);

                if (removeBracketsRegex.IsMatch(l))
                {
                    quantityStrings.Add("<value> " + removeBracketsRegex.Replace(l, string.Empty));
                }
                else
                {
                    quantityStrings.Add("<value> " + l);
                }
            }
        }

        return (quantityData.ToArray(), quantityStrings.ToArray());
    }
}
