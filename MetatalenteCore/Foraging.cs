namespace Metatalente.Core;

public class Foraging : MetatalentBase
{
    public Foraging()
    {
        core = Core.GetInstance();
    }

    public override void SetSkill()
    {
        SetSkill(["Wildnisleben", "Sinnenschärfe", "Pflanzenkunde"]);
    }

    public override void Roll()
    {
        int amount = 0;

        if (core.CurrentRegion.ForagingMod is null)
        {
            return;
        }

        int mod = (int)core.CurrentRegion.ForagingMod;
        int durationMod = (Duration - 60) / 15;

        if (durationMod > SkillPoints / 2)
        {
            durationMod = SkillPoints / 2;
        }

        mod -= durationMod;
        (int pointsLeft, string stringResult) = Roll(core.Mu, core.In, core.Ge, mod);

        if (pointsLeft > 0)
        {
            amount = 1 + (pointsLeft / 3);
        }

        LastResult = new(pointsLeft.ToString(), stringResult, amount.ToString() + " Rationen");
    }
}
