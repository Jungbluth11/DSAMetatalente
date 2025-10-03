namespace Metatalente.Core;

public class SetTraps : TalentBase
{
    public SetTraps()
    {
        _core.PropertyChanged += Core_PropertyChanged;
        SkillPoints = _core.SkillFallenstellen;
    }

    private void Core_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "LoadCharacter":
                IsSet = _core.SkillFallenstellen != 0;

                break;
            case "SkillFallenStellen":
                SkillPoints = _core.SkillFallenstellen;

                break;
        }
    }

    public override void Roll()
    {
        int amount = 0;
        if (_core.CurrentRegion.WildlifeMod == null)
        {
            return;
        }

        if (Duration == 90)
        {
            SkillPoints = (int) Math.Floor(SkillPoints * 1.5);
        }

        int mod = 5 + _core.CurrentRegion.WildlifeMod.Value;
        (int pointsLeft, string stringResult) = Roll(_core.Kl, _core.Ff, _core.Kk, mod);

        if (pointsLeft > 0)
        {
            amount = 1 + pointsLeft / (_core.CurrentRegion.WildlifeMod.Value / 2);
        }

        string textResult = amount + (amount == 1 ? "Ration" : " Rationen");
        LastResult = new(pointsLeft.ToString(), stringResult, textResult);
    }
}