using System.ComponentModel;

namespace DSAMetatalente.ViewModels;

public partial class TabPlantSeekingViewModel : ObservableObject
{
    [ObservableProperty] private bool _canRoll = true;
    [ObservableProperty] private bool _coincidence;
    private readonly Core _core = Core.GetInstance();
    [ObservableProperty] private int _duration;
    [ObservableProperty] private int _minDuration;
    private readonly PlantSeeking _plantSeeking = new();
    [ObservableProperty] private string _currentPlant;
    [ObservableProperty] private string _diceResult = string.Empty;
    [ObservableProperty] private string _identificationMod = string.Empty;
    [ObservableProperty] private string _loot = string.Empty;
    [ObservableProperty] private string _occurrence = string.Empty;
    [ObservableProperty] private string _pointsResult = string.Empty;
    [ObservableProperty] private string _textResult = string.Empty;
    public ObservableCollection<string> Plants { get; } = [];

    public TabPlantSeekingViewModel()
    {
        _core.PropertyChanged += Core_PropertyChanged;
        CurrentPlant = _plantSeeking.CurrentPlant.Name;
        Duration = _plantSeeking.Duration;
        MinDuration = _plantSeeking.MinDuration;
    }

    private void CheckCanRoll()
    {
        _plantSeeking.SetSkill();

        if (Plants.Count > 0 && _plantSeeking.IsSet)
        {
            CanRoll = true;
        }
        else
        {
            CanRoll = false;
        }
    }

    private void Core_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "CurrentRegion" or "CurrentLandscape" or "CurrentMonth":
                SetPlantList();
                SetPlantDescription();
                CheckCanRoll();

                break;
            case "LoadCharacter":
                CheckCanRoll();

                break;
        }
    }

    partial void OnCurrentPlantChanged(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        _plantSeeking.CurrentPlant = _plantSeeking.GetPlantByName(value);
        SetPlantDescription();
    }

    partial void OnDurationChanged(int value)
    {
        _plantSeeking.Duration = value;
    }

    [RelayCommand]
    private void Roll()
    {
        _plantSeeking.Roll();
        DiceResult = _plantSeeking.LastResult.DiceResult;
        PointsResult = _plantSeeking.LastResult.PointsLeft;
        TextResult = _plantSeeking.LastResult.TextResult;
    }

    private void SetPlantDescription()
    {
        try
        {
            string identificationModText = _plantSeeking.CurrentPlant.IdentificationMod.ToString();

            if (_plantSeeking.CurrentPlant.IdentificationMod > 0)
            {
                identificationModText = identificationModText.Insert(0, "+");
            }

            IdentificationMod = identificationModText;
            Occurrence = Core.OccurToString(_plantSeeking.GetOccurrenceMod(_plantSeeking.CurrentPlant));
            Loot = _plantSeeking.CurrentPlant.LootDisplayText;
        }
        catch
        {
            IdentificationMod = string.Empty;
            Occurrence = string.Empty;
            Loot = string.Empty;
        }
    }

    private void SetPlantList()
    {
        Plants.Clear();
        Plant[] plants = _core.CurrentRegion.GetPossiblePlants(_core.CurrentLandscape.Name, _core.CurrentMonth);

        if (plants.Length == 0)
        {
            CurrentPlant = string.Empty;

            return;
        }

        Plants.AddRange([.. from Plant in plants select Plant.Name]);

        if (!(from Plant in plants select Plant.Name).Contains(CurrentPlant))
        {
            CurrentPlant = Plants.First();
        }
    }
}
