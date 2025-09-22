using System.ComponentModel;

namespace DSAMetatalente.ViewModels;

public partial class TabForagingViewModel : ObservableObject
{
    [ObservableProperty] private bool _canRoll = true;
    private readonly Core _core = Core.GetInstance();
    private readonly Foraging _foraging = new();
    [ObservableProperty] private int _duration;
    [ObservableProperty] private int _minDuration;
    [ObservableProperty] private string _diceResult = string.Empty;
    [ObservableProperty] private string _pointsResult = string.Empty;
    [ObservableProperty] private string _textResult = string.Empty;

    public TabForagingViewModel()
    {
        _core.PropertyChanged += Core_PropertyChanged;
        Duration = _foraging.Duration;
        MinDuration = _foraging.MinDuration;
    }

    private void CheckCanRoll()
    {
        _foraging.SetSkill();

        CanRoll = _core.CurrentRegion.ForagingMod != null && _foraging.IsSet;
    }

    private void Core_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is "CurrentRegion" or "CurrentLandscape" or "LoadCharacter")
        {
            CheckCanRoll();
        }
    }

    partial void OnDurationChanged(int value)
    {
        _foraging.Duration = value;
    }

    [RelayCommand]
    private void Roll()
    {
        _foraging.Roll();
        DiceResult = _foraging.LastResult.DiceResult;
        PointsResult = _foraging.LastResult.PointsLeft;
        TextResult = _foraging.LastResult.TextResult;
    }
}
