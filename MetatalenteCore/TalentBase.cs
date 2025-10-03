namespace Metatalente.Core;

public abstract class TalentBase
{
    protected Core _core = Core.GetInstance();
    public int Duration { get; set; } = 60;
    public int MinDuration { get; protected set; } = 60;
    public int SkillPoints { get; set; }
    public bool IsSet { get; protected set; } = true;
    public Result LastResult { get; protected set; }

    public abstract void Roll();

    protected (int pointsLeft, string stringResult) Roll(int attribute1, int attribute2, int attribute3, int mod, int skillPoints = 0)
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

        if (_core.IsTerrainKnowledge)
        {
            mod -= 3;
        }

        if (_core.IsLocalKnowledge)
        {
            mod -= 7;
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