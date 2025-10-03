namespace Metatalente.Core;

public abstract class MetatalentBase : TalentBase
{
    private string[] _baseSkills = [];

    protected MetatalentBase()
    {
        _core.PropertyChanged += Core_OnPropertyChanged;
    }

    private void Core_OnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (_baseSkills.Contains(e.PropertyName))
        {
            SetSkill(_baseSkills);
        }
    }

    public abstract void SetSkill();

    protected void SetSkill(string[] baseSkills)
    {
        _baseSkills = baseSkills;
        List<int> skillValues = [];
        int lowest = 0;
        int sum = 0;

        foreach (string baseSkill in baseSkills)
        {
            if (_core.Character != null)
            {
                try
                {
                    // ReSharper disable once UnusedVariable --- Just to check if the skill exists
                    Ability ability = _core.Character.Talente.Single(a => a.Name == baseSkill);
                }
                catch
                {
                    IsSet = false;
                    return;
                }

                if (!IsSet)
                {
                    IsSet = true;
                }
            }

            switch (baseSkill)
            {
                case "Wildnisleben":
                    skillValues.Add(_core.SkillWildnisleben);
                    break;
                case "Sinnenschärfe":
                    skillValues.Add(_core.SkillSinnenschaerfe);
                    break;
                case "Pflanzenkunde":
                    skillValues.Add(_core.SkillPflanzenkunde);
                    break;
                case "Tierkunde":
                    skillValues.Add(_core.SkillTierkunde);
                    break;
                case "Fährtensuchen":
                    skillValues.Add(_core.SkillFaehrtensuchen);
                    break;
                case "Schleichen":
                    skillValues.Add(_core.SkillSchleichen);
                    break;
                case "Sich Verstecken":
                    skillValues.Add(_core.SkillSichVerstecken);
                    break;
                default:
                    skillValues.Add(_core.SkillWeapon);
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
}
