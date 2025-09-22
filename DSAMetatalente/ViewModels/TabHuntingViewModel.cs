using System.ComponentModel;

namespace DSAMetatalente.ViewModels;

public partial class TabHuntingViewModel : ObservableObject
{
    [ObservableProperty] private bool _canRoll = true;
    [ObservableProperty] private bool _coincidence;
    [ObservableProperty] private bool _isMeisterschuetze;
    [ObservableProperty] private bool _isScharfschuetze;
    private readonly Core _core = Core.GetInstance();
    private readonly Hunting _hunting = new();
    [ObservableProperty] private int _duration;
    [ObservableProperty] private int _minDuration;
    [ObservableProperty] private int _skillWeapon;
    [ObservableProperty] private string _currentAnimal;
    [ObservableProperty] private string _diceResult = string.Empty;
    [ObservableProperty] private string _difficulty = string.Empty;
    [ObservableProperty] private string _loot = string.Empty;
    [ObservableProperty] private string _pointsResult = string.Empty;
    [ObservableProperty] private string _textResult = string.Empty;
    [ObservableProperty] private string _usedWeapon;

    public ObservableCollection<string> Animals { get; } = [];
    public IEnumerable<string> Weapons => from Weapon in Hunting.Weapons select Weapon.Name;

    public TabHuntingViewModel()
    {
        _core.PropertyChanged += Core_PropertyChanged;
        CurrentAnimal = _hunting.CurrentAnimal.Name;
        Duration = _hunting.Duration;
        MinDuration = _hunting.MinDuration;
        UsedWeapon = _hunting.UsedWeapon.Name;
    }

    private void CheckCanRoll()
    {
        _hunting.SetSkill();

        if (_core.CurrentRegion.WildlifeMod != null)
        {
            CanRoll = _hunting.IsSet;
        }
    }

    private void Core_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "CurrentRegion" or "CurrentLandscape":
                CheckCanRoll();
                Animals.Clear();
                Animals.AddRange([.. _core.CurrentRegion.Animals]);
                CurrentAnimal = Animals.Count > 0 ? Animals[0] : string.Empty;

                break;
            case "LoadCharacter":
                CheckCanRoll();
                LoadCharacter();

                break;
        }
    }

    private void LoadCharacter()
    {
        try
        {
            _hunting.LoadWeaponFromCharacter();
            UsedWeapon = _hunting.UsedWeapon.Name;
            SkillWeapon = _core.SkillWeapon;
            IsScharfschuetze = _hunting.IsScharfschuetze;
            IsMeisterschuetze = _hunting.IsMeisterschuetze;
        }
        catch
        {
        }
    }

    partial void OnCoincidenceChanged(bool value)
    {
        _hunting.Coincidence = value;
    }

    partial void OnCurrentAnimalChanged(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        _hunting.CurrentAnimal = _hunting.GetAnimalByName(value);
        Loot = _hunting.CurrentAnimal.LootDisplayText;
        Difficulty = _hunting.CurrentAnimal.Difficulty.ToString().Insert(0, "+");
    }

    partial void OnDurationChanged(int value)
    {
        _hunting.Duration = value;
    }

    partial void OnSkillWeaponChanged(int value)
    {
        _core.SkillWeapon = value;
    }

    partial void OnUsedWeaponChanged(string value)
    {
        _hunting.UsedWeapon = _hunting.GetWeaponByName(value);
    }

    [RelayCommand]
    private void Roll()
    {
        _hunting.Roll();
        DiceResult = _hunting.LastResult.DiceResult;
        PointsResult = _hunting.LastResult.PointsLeft;
        TextResult = _hunting.LastResult.TextResult;
    }

    [RelayCommand]
    private void ToggleHuntingVariant(string value)
    {
        _hunting.Variant = value == "Pirschjagd" ? Hunting.Variants.Pirschjagd : Hunting.Variants.Ansitzjagd;
    }
}
