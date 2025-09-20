namespace DSAMetatalente.ViewModels;

public partial class TabForagingViewModel : ObservableObject
{
    [ObservableProperty] private int _duration;
    [ObservableProperty] private int _minDuration;
    [ObservableProperty] private bool _canRoll = true;
    [ObservableProperty] private string _diceResult = string.Empty;
    [ObservableProperty] private string _pointsResult = string.Empty;
    [ObservableProperty] private string _textResult = string.Empty;
    private readonly Core _core = Core.GetInstance();
    private readonly Foraging _foraging = new();

    public TabForagingViewModel()
    {
        _core.PropertyChanged += Core_PropertyChanged;
        Duration = _foraging.Duration;
        MinDuration = _foraging.MinDuration;
    }

    private void Core_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName is "CurrentRegion" or "CurrentLandscape")
        {
            CheckCanRoll();
        }
    }

    partial void OnDurationChanged(int value)
    {
        _foraging.Duration = value;
    }

    private void CheckCanRoll()
    {
        _foraging.SetSkill();

        CanRoll = _core.CurrentRegion.ForagingMod != null && _foraging.IsSet;
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
