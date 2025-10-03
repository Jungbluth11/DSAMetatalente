namespace DSAMetatalente.ViewModels;
public partial class TabFishingViewModel : ObservableObject
{
    private readonly Core _core = Core.GetInstance();
    private readonly Fishing _fishing = new();
    [ObservableProperty] private bool _canRoll = true;
    [ObservableProperty] private int _duration;
    [ObservableProperty] private int _minDuration;
    [ObservableProperty] private string _diceResult = string.Empty;
    [ObservableProperty] private string _pointsResult = string.Empty;
    [ObservableProperty] private string _textResult = string.Empty;

    public TabFishingViewModel()
    {
        _core.PropertyChanged += Core_PropertyChanged;
        Duration = _fishing.Duration;
        MinDuration = _fishing.MinDuration;

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

        CanRoll = _fishing.IsSet;
    }

    partial void OnDurationChanged(int value)
    {
        _fishing.Duration = value;
    }


    [RelayCommand]
    private void Roll()
    {
        _fishing.Roll();
        DiceResult = _fishing.LastResult.DiceResult;
        PointsResult = _fishing.LastResult.PointsLeft;
        TextResult = _fishing.LastResult.TextResult;
    }
}
