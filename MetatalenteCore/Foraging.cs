namespace DSAMetatalente.Core;

public class Foraging : MetatalentBase
{
    public Foraging(Core core)
    {
        this.core = core;
    }

    public override void SetSkill()
    {
        SetSkill(["Wildnisleben", "Sinnenschärfe", "Pflanzenkunde"]);
    }

    public override void Roll()
    {
        int pointsLeft;
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
        (int pointsReselut, string stringResult) rolldata = Roll(core.MU, core.IN, core.GE, mod);
        pointsLeft = rolldata.pointsReselut;

        if (pointsLeft > 0)
        {
            amount = 1 + (pointsLeft / 3);
        }

        LastResult = new Result(pointsLeft.ToString(), rolldata.stringResult, amount.ToString() + " Rationen");
    }
}
