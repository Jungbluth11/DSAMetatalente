namespace DSAMetatalente.ViewModels;

public partial class TabSetTrapsViewModel : ObservableObject
{
    private readonly Core _core = Core.GetInstance();
    private readonly SetTraps _setTraps = new();
    [ObservableProperty] private bool _canRoll = true;
    [ObservableProperty] private int _duration;
    [ObservableProperty] private int _minDuration;
    [ObservableProperty] private string _diceResult = string.Empty;
    [ObservableProperty] private string _pointsResult = string.Empty;
    [ObservableProperty] private string _textResult = string.Empty;

    public TabSetTrapsViewModel()
    {
        Duration = _setTraps.Duration;
        MinDuration = _setTraps.MinDuration;
        _core.PropertyChanged += Core_PropertyChanged;
    }

    private void Core_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "LoadCharacter")
        {
            CheckCanRoll();
        }
    }

    private void CheckCanRoll()
    {
        CanRoll = _core.CurrentRegion.WildlifeMod != null && _setTraps.IsSet;
    }

    partial void OnDurationChanged(int value)
    {
        _setTraps.Duration = value;
    }

    [RelayCommand]
    private void Roll()
    {
        _setTraps.Roll();
        DiceResult = _setTraps.LastResult.DiceResult;
        PointsResult = _setTraps.LastResult.PointsLeft;
        TextResult = _setTraps.LastResult.TextResult;
    }
}