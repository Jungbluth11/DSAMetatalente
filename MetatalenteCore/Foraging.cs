namespace Metatalente.Core;

public class Foraging : MetatalentBase
{
    public Foraging()
    {
        SetSkill();
    }

    public sealed override void SetSkill()
    {
        SetSkill(["Wildnisleben", "Sinnenschärfe", "Pflanzenkunde"]);
    }

    public override void Roll()
    {
        int amount = 0;

        if (_core.CurrentRegion.ForagingMod is null)
        {
            return;
        }

        int mod = (int) _core.CurrentRegion.ForagingMod;
        int durationMod = (Duration - 60) / 15;

        if (durationMod > SkillPoints / 2)
        {
            durationMod = SkillPoints / 2;
        }

        mod -= durationMod;
        (int pointsLeft, string stringResult) = Roll(_core.Mu, _core.In, _core.Ge, mod);

        if (pointsLeft > 0)
        {
            amount = 1 + (pointsLeft / 3);
        }

        LastResult = new(pointsLeft.ToString(), stringResult, amount.ToString() + " Rationen");
    }
}
