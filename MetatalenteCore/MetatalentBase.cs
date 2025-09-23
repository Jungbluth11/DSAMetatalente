using DSAUtils.HeldentoolInterop;

namespace Metatalente.Core;

public abstract class MetatalentBase
{
#pragma warning disable CS8618 // is set in child classes
    protected Core core;
#pragma warning restore CS8618
    public int Duration { get; set; } = 60;
    public int MinDuration { get; protected set; } = 60;
    public int SkillPoints { get; set; }
    public bool IsSet { get; protected set; } = true;
    public Result LastResult { get; protected set; }

    public abstract void SetSkill();

    public abstract void Roll();

    protected void SetSkill(string[] baseSkills)
    {
        List<int> skillValues = [];
        int lowest = 0;
        int sum = 0;

        foreach (string baseSkill in baseSkills)
        {
            if (core.Character != null)
            {
                try
                {
                    Ability ability = core.Character.Talente.Single(a => a.Name == baseSkill);

                    if (!IsSet)
                    {
                        IsSet = true;
                    }
                }
                catch
                {
                    IsSet = false;
                    return;
                }
            }

            switch (baseSkill)
            {
                case "Wildnisleben":
                    skillValues.Add(core.SkillWildnisleben);
                    break;
                case "Sinnenschärfe":
                    skillValues.Add(core.SkillSinnenschaerfe);
                    break;
                case "Pflanzenkunde":
                    skillValues.Add(core.SkillPflanzenkunde);
                    break;
                case "Tierkunde":
                    skillValues.Add(core.SkillTierkunde);
                    break;
                case "Fährtensuchen":
                    skillValues.Add(core.SkillFaehrtensuchen);
                    break;
                case "Schleichen":
                    skillValues.Add(core.SkillSchleichen);
                    break;
                case "Sich Verstecken":
                    skillValues.Add(core.SkillSichVerstecken);
                    break;
                default:
                    skillValues.Add(core.SkillWeapon);
                    break;
            }
        }

        foreach (int value in skillValues)
        {
            if (value < lowest || lowest == 0)
            {
                lowest = value;
            }
            sum += value;
        }

        if (sum / skillValues.Count > lowest * 2)
        {
            SkillPoints = lowest * 2;
        }
        else
        {
            SkillPoints = sum / skillValues.Count;
        }
    }

    protected (int, string) Roll(int attribute1, int attribute2, int attribute3, int mod, int skillPoints = 0)
    {
        if (!IsSet)
        {
            throw new("The loaded Character has not this Skill. Can't roll");
        }

        if (Duration < MinDuration)
        {
            throw new("Duration can't be less than MinDuration");
        }

        if (skillPoints == 0)
        {
            skillPoints = SkillPoints;
        }

        if (core.IsKnownTerrain)
        {
            mod -= 3;
        }

        (int punkteUeber, int[] wuerfelergebnisse, string text) = DSA.TaP(attribute1, attribute2, attribute3, skillPoints, mod);
        string textdata = wuerfelergebnisse[0].ToString() + "/" + wuerfelergebnisse[1].ToString() + "/" + wuerfelergebnisse[2].ToString();

        if (!string.IsNullOrEmpty(text))
        {
            textdata += " (" + text + ")";
        }

        return (punkteUeber, textdata);
    }
}
