using System.Globalization;

namespace Metatalente.Core;

public class Fishing : TalentBase
{
    public Fishing()
    {
        _core.PropertyChanged += Core_PropertyChanged;
        SkillPoints = _core.SkillFischenAngeln;
    }

    private void Core_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "LoadCharacter":
                IsSet = _core.SkillFischenAngeln != 0;

                break;
            case "SkillFischenAngeln":
                SkillPoints = _core.SkillFischenAngeln;

                break;
        }
    }

    public override void Roll()
    {
        double amount = 0;
        int intervall = Duration / MinDuration;
        string pointsLeft = string.Empty;
        string diceResult = string.Empty;

        for (int i = 0; i < intervall; i++)
        {
            (int pointsResult, string stringResult) = Roll(_core.In, _core.Ff, _core.Kk, 0);

            pointsLeft += pointsResult + "\n";
            diceResult += stringResult + "\n";

            if (pointsResult <= 0)
            {
                continue;
            }

            // ReSharper disable once PossibleLossOfFraction
            amount += 1 + ((pointsResult / 3) * 0.5);
        }

        string textResult = amount.ToString(CultureInfo.CurrentCulture) + ((int) amount == 1 ? "Ration" : " Rationen");
        LastResult = new(pointsLeft, diceResult, textResult);
    }
}