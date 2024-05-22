using System.Text.RegularExpressions;
using System.Xml;
using DSAUtils.HeldentoolInterop;

namespace Metatalente.Core
{
    public class Hunting : MetatalentBase
    {
        private Variants _variant = Variants.Pirschjagd;
        public Variants Variant
        {
            get => _variant;
            set
            {
                _variant = value;
                if (_variant == Variants.Pirschjagd)
                {
                    MinDuration = 60;
                }
                else
                {
                    MinDuration = 90;
                }
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

        public static Weapon[] Weapons => new Weapon[]
        {
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
        };

        public static Animal[] Animals => new Animal[]
        {
            new(15, "Löwenaffe", new string[] { "0.5 bis 2 Rationen Fleisch", "Fell (besser)" }),
            new(15, "Moosaffe", new string[] { "0.5 bis 2 Rationen Fleisch", "Fell (besser)" }),
            new(2, "Otan-Otan", new string[] { "bis 40 Rationen Fleisch (zäh)", "Fell (besser)" }),
            new(15, "Purzelaffe", new string[] { "0.5 bis 2 Rationen Fleisch", "Fell (besser)" }),
            new(5, "Riesenaffe", new string[] { "190 Rationen Fleisch (zäh)", "Fell (besser)" }),
            new(2, "Sumpfranze", new string[] { "25 Rationen Fleisch (ungenießbar)", "Fell (wertlos)" }),
            new(12, "Al'Kebir-Antilope", new string[] { "140 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" }),
            new(12, "Gabelantilope", new string[] { "9 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" }),
            new(12, "Halmar-Antilope", new string[] { "15 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" }),
            new(10, "Karen", new string[] { "40 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (besser)" }),
            new(12, "Springbock", new string[] { "19 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" }),
            new(10, "Vy'Tagga-Antilope", new string[] { "15 Rationen Fleisch (ungenießbar)", "Geweih (Trophäe)", "Fell (besser) oder Leder (besser)" }),
            new(6, "Baumbär", new string[] { "15 bis 20 Rationen Fleisch (zäh)", "Fell (teuer)" }),
            new(12, "Baumwürger", new string[] { "30 Rationen Fleisch (zäh)", "Fell (teuer)" }),
            new(5, "Borkenbär", new string[] { "110 Rationen Fleisch", "Fell (einfach)" }),
            new(12, "Firunsbär", new string[] { "600 Rationen Fleisch", "Fell (Luxusartikel)" }),
            new(6, "Höhlenbär", new string[] { "350 Rationen Fleisch", "Fell (teuer)" }),
            new(7, "Orklandbär", new string[] { "50 Rationen Fleisch", "Fell (billig)" }),
            new(4, "Schwarzbär", new string[] { "380 Rationen Fleisch", "Fell (Luxusartikel)" }),
            new(9, "Schneedachs", new string[] { "5 Rationen Fleisch", "Fell (einfach)" }),
            new(12, "Streifendachs", new string[] { "4 Rationen Fleisch", "Fell (einfach)" }),
            new(7, "Brabaker Waldelefant", new string[] { "1500 bis 2000 Rationen Fleisch", "Haut (Leder, teuer)", "Stoßzähne (5 D je Stein / pro Zahn bis zu 15 Stein)" }),
            new(10, "Mammut", new string[] { "2800 bis 3400 Rationen Fleisch", "Haut (Fell oder Leder, teuer)", "Stoßzähne (4 D je Stein / pro Zahn bis zu 40 Stein)" }),
            new(8, "Mastodon", new string[] { "1600 bis 2400 Rationen Fleisch", "Haut (Fell oder Leder, teuer)", "Stoßzähne (3 D je Stein / pro Zahn bis zu 20 Stein)" }),
            new(4, "Zwergelefant", new string[] { "500 Rationen Fleisch", "Haut (Fell oder Leder, teuer)", "Stoßzähne (3 D je Stein / pro Zahn bis zu 30 Stein)" }),
            new(13, "Gelbfuchs", new string[] { "3 Rationen Fleisch (sehr zäh)", "Pelz (billig)" }),
            new(9, "Rotfuchs", new string[] { "3 Rationen Fleisch (sehr zäh)", "Pelz (teuer)" }),
            new(15, "Blaufuchs", new string[] { "3 Rationen Fleisch (sehr zäh)", "Pelz (Luxusartikel)" }),
            new(6, "Auerhahn", new string[] { "3 Rationen Fleisch" }),
            new(5, "Fasan", new string[] { "1 bis 2 Rationen Fleisch", "Fell (besser)" }),
            new(5, "Regenbogenfasan", new string[] { "1 Ration Fleisch", "Fell (besser)" }),
            new(5, "Rebhuhn", new string[] { "1 bis 2 Rationen Fleisch" }),
            new(5, "Trappe", new string[] { "5 Rationen Fleisch" }),
            new(7, "Karnickel", new string[] { "1 bis 2 Rationen Fleisch", "Fell (besser)" }),
            new(7, "Orklandkarnickel", new string[] { "1 bis 2 Rationen Fleisch", "Fell (besser)" }),
            new(7, "Pfeifhase", new string[] { "1 bis 2 Rationen Fleisch", "Fell (besser)" }),
            new(4, "Riesenlöffler", new string[] { "2 Rationen Fleisch", "Fell (besser)" }),
            new(12, "Rotpüschel", new string[] { "1 Ration Fleisch", "Fell (besser)" }),
            new(10, "Silberbock", new string[] { "1 bis 2 Rationen Fleisch", "Fell (besser)" }),
            new(4, "Elch", new string[] { "450 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" }),
            new(8, "Firunshirsch", new string[] { "60 Rationen Fleisch", "Geweih (Trophäe)", "Fell (teuer, in reinem Weiß Luxusware) oder Leder (teuer)" }),
            new(6, "Kronenhirsch", new string[] { "110 Rationen Fleisch", "Geweih (Trophäe)", "Fell (teuer) oder Leder (teuer)" }),
            new(5, "Rehwild", new string[] { "11 Rationen Fleisch", "Geweih (Trophäe)", "Fell (besser) oder Leder (teuer)" }),
            new(10, "Steppenhund", new string[] { "12 Rationen Fleisch (zäh)", "Fell (billig)" }),
            new(20, "Wildkatze", new string[] { "3 Rationen Fleisch", "Fell (einfach)" }),
            new(12, "Berglöwe", new string[] { "65 Rationen Fleisch", "Fell (Luxusartikel)" }),
            new(6, "Sandlöwe", new string[] { "100 Rationen Fleisch", "Fell (Luxusartikel)" }),
            new(12, "Lioma", new string[] { "90 Rationen Fleisch", "Fell (Luxusartikel)" }),
            new(9, "Waldlöwe", new string[] { "60 Rationen Fleisch", "Fell (Luxusartikel)" }),
            new(20, "Firnluchs", new string[] { "12 Rationen Fleisch", "Fell (teuer)" }),
            new(15, "Gänseluchs", new string[] { "3 Rationen Fleisch", "Fell (einfach)" }),
            new(15, "Raschtulsluchs", new string[] { "10 Rationen Fleisch", "Fell (besser)" }),
            new(15, "Rotluchs", new string[] { "10 Rationen Fleisch", "Fell (besser)" }),
            new(15, "Sonnenluchs", new string[] { "10 Rationen Fleisch", "Fell (besser)" }),
            new(15, "Fleckenpanther", new string[] { "40 Rationen Fleisch", "Fell (Luxusartikel)" }),
            new(15, "Khômgepard", new string[] { "15 Rationen Fleisch", "Fell (Luxusartikel)" }),
            new(8, "Auerochse", new string[] { "550 Rationen Fleisch", "Hörner (Trophäe)", "Haut (einfach)", "Leder (teuer)" }),
            new(7, "Firnyak", new string[] { "140 Rationen Fleisch", "Hörner (Trophäe)", "Fell (teuer, in reinem weiß Luxusware) oder Leder (teuer)" }),
            new(4, "Ongalobulle", new string[] { "300 Rationen Fleisch", "Hörner (Trophäe)", "Haut (einfach)", "Leder (besser)" }),
            new(12, "Steppenrind", new string[] { "600 Rationen Fleisch", "Hörner (Trophäe)", "Haut (einfach)", "Leder (teuer)" }),
            new(8, "Meerkalb", new string[] { "bis 250 Rationen Fleisch", "Tran (5-7 D)", "Haut (Leder, besser)" }),
            new(13, "Seetiger", new string[] { "250 bis 300 Rationen Fleisch", "Tran (12-15 D)", "Haut (Leder, besser)", "Bein (teuer)", "Ambra (3 D)" }),
            new(10, "Dschungeltiger", new string[] { "100 Rationen Fleisch", "Fell (Luxusartikel)", "Zähne (Trophäe)" }),
            new(10, "Silberlöwe", new string[] { "100 Rationen Fleisch", "Fell (Luxusartikel)", "Zähne (Trophäe)" }),
            new(10, "Steppentiger", new string[] { "100 Rationen Fleisch", "Fell (Luxusartikel)", "Zähne (Trophäe)" }),
            new(2, "Wildschwein", new string[] { "bis 130 Rationen Fleisch", "Hauer (Trophäe)", "Fell (billig) oder Leder (einfach)" }),
            new(2, "Warzenschwein", new string[] { "bis 130 Rationen Fleisch", "Hauer (Trophäe)", "Fell (billig) oder Leder (einfach)" }),
            new(4, "Strauß", new string[] { "50 Rationen Fleisch", "15 bis 60 Eier im Nest (jedes Ei eine Ration)", "Gefieder (teuer)" }),
            new(6, "Vielfraß", new string[] { "8 Rationen Fleisch (ungenießbar)", "Fell (besser)" }),
            new(5, "Baumschleimer", new string[] { "6 Rationen Fleisch (ungenießbar)", "Fell (wertlos)" }),
            new(20, "Gebirgsbock", new string[] { "60 Rationen Fleisch (zäh)", "Fell (einfach)", "Horn (Trophäe)" }),
            new(2, "Felsrobbe", new string[] { "bis 45 Rationen Fleisch", "Talg (Fett", "Öl", "5 S)", "Tran (1 D)", "Haut (Fell, besser, von Jungtieren teuer)" }),
            new(7, "Maraskanisches Stachelschwein", new string[] { "1 Ration Fleisch", "2W20+30 Stacheln (FF-Probe, um sie inklusive Giftdrüse aus dem Tier zu ziehen)" }),
            new(7, "Klippechse", new string[] { "10 Rationen Fleisch", "Haut (Leder, teuer)", "Eier (diverse alchemistische Anwendungen)" }),
            new(5, "Schreckkatze",  new string[] { "12 Rationen Fleisch (nur für Orks und Goblins genießbar)", "Fell wertlos" })
        };

        #endregion hard coded data

        public Hunting(Core core)
        {
            this.core = core;
        }

        public override void SetSkill()
        {
            string skill;
            if (Variant == Variants.Pirschjagd)
            {
                skill = "Schleichen";
            }
            else
            {
                skill = "Sich Verstecken";
            }
            SetSkill(new string[] { "Wildnisleben", "Tierkunde", "Fährtensuchen", skill, UsedWeapon.UsedSkill });
            if (core.character != null)
            {
                if (core.character.Sonderfertigkeiten.Contains("Scharfschütze"))
                {
                    IsScharfschuetze = true;
                }
                if (core.character.Sonderfertigkeiten.Contains("Meisterschütze"))
                {
                    IsMeisterschuetze = true;
                }
            }
        }

        public override void Roll()
        {
            int intervall;
            int mod = 0;
            int lowRangeMod = 0;
            string pointsLeft = string.Empty;
            string diceResult = string.Empty;
            string textResult = string.Empty;
            if (core.CurrentRegion.WildlifeMod is null)
            {
                return;
            }
            if (Coincidence)
            {
                mod += 7 + (int)core.CurrentRegion.WildlifeMod;
            }
            else
            {
                mod += CurrentAnimal.Difficulty + (int)core.CurrentRegion.WildlifeMod;
            }
            if (UsedWeapon.MaxRange == 20)
            {
                lowRangeMod = 7;
            }
            else if (UsedWeapon.MaxRange < 51)
            {
                lowRangeMod = 3;
            }
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
            intervall = Duration / MinDuration;

            if (Coincidence)
            {
                int amount = 0;
                for (int i = 0; i < intervall; i++)
                {
                    (int pointsResult, string[] resultStrings) rolldata = GetRollData(mod, i, intervall > 1);
                    amount += rolldata.pointsResult;
                    pointsLeft += rolldata.resultStrings[0];
                    diceResult += rolldata.resultStrings[1];
                }
                textResult += amount.ToString() + " Rationen";
            }
            else
            {
                int huntedAnimals = 0;
                int[] quantityTotal = Array.Empty<int>();
                string[] quantityStrings = Array.Empty<string>();
                for (int i = 0; i < intervall; i++)
                {
                    (int pointsResult, string[] resultStrings) rolldata = GetRollData(mod, i, intervall > 1);
                    pointsLeft += rolldata.resultStrings[0];
                    diceResult += rolldata.resultStrings[1];
                    if (rolldata.pointsResult > 0)
                    {
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
                }
                textResult = huntedAnimals.ToString() + (huntedAnimals > 1 ? " Tiere" : " Tier") + "\n\n";
                for (int i = 0; i < quantityTotal.Length; i++)
                {
                    textResult += quantityStrings[i].Replace("<value>", quantityTotal[i].ToString()) + "\n";
                }
            }

            LastResult = new Result(pointsLeft, diceResult, textResult);
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
            if (core.character != null)
            {
                try
                {
                    XmlDocument xml = new();
                    xml.LoadXml(core.character.XML);
                    string number = xml.SelectSingleNode("//heldenausruestung[@name='jagtwaffe']").Attributes["nummer"].Value;
#pragma warning disable CS8600
                    XmlNode weapon = xml.SelectSingleNode("//heldenausruestung[@name='fkwaffe" + number + "']");
#pragma warning restore CS8600
                    Ability weaponAbility = core.character.Talente.Single(a => a.Name == weapon.Attributes["talent"].Value);
                    UsedWeapon = GetWeaponByName(weapon.Attributes["waffenname"].Value);
                    core.SkillWeapon = weaponAbility.Wert;
                    IsScharfschuetze = core.character.Sonderfertigkeiten.Contains("Scharfschütze (" + weaponAbility.Name + ")");
                    IsMeisterschuetze = core.character.Sonderfertigkeiten.Contains("Meisterschütze (" + weaponAbility.Name + ")");
                }
                catch
                {
                    throw new Exception("No used Weapon defined");
                }
            }
        }

        private (int pointsResult, string[] resultStrings) GetRollData(int mod, int intervall, bool multiple)
        {
            string[] resultStrings = new string[2];
            string count = string.Empty;
            (int pointsResult, string stringResult) rolldata = Roll(core.MU, core.IN, core.GE, mod);
            if (multiple)
            {
                count += "Wurf " + (intervall + 1).ToString() + ": ";
            }
            resultStrings[0] = count + rolldata.pointsResult.ToString() + "\n";
            resultStrings[1] = count + rolldata.stringResult + "\n";
            return (rolldata.pointsResult, resultStrings);
        }

        private (int[] quantityData, string[] quantityStrings) GenerateLootQuantity(string[] loot)
        {
            string replacement = "<value>";
            Regex variableQuantityRegex = new("([0-9]+)?\\s?bis\\s?(?:zu\\s)?([0-9]+)");
            Regex fixedQuantityRegex = new("[0-9]+");
            Regex removeBracketsRegex = new("\\(((?!\\)).)*\\)\\s?");
            Regex unitRegex = new("[0-9]+(\\s\\w{3,})(?!\\s[0-9])");
            Random random = new();
            List<int> quantityData = new();
            List<string> quantityStrings = new();
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
}