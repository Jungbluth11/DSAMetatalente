using Version = DSAMetatalente.Models.Version;

namespace DSAMetatalente.ViewModels;

public partial class MainPageViewModel : ObservableObject, IRecipient<Charakter>
{
    const string UpdateLinkBase = "https://api.jungbluthcloud.de/updates/dsametatalente/";
    const string VersionLink = "https://api.jungbluthcloud.de/updates/dsametatalente/version";
    [ObservableProperty] bool _isKnownTerrain;
    [ObservableProperty] bool _isUpdateAvailable;
    [ObservableProperty] int _ff;
    [ObservableProperty] int _ge;
    [ObservableProperty] int _in;
    [ObservableProperty] int _mu;
    [ObservableProperty] int _skillFaehrtensuchen;
    [ObservableProperty] int _skillPflanzenkunde;
    [ObservableProperty] int _skillSchleichen;
    [ObservableProperty] int _skillSichVerstecken;
    [ObservableProperty] int _skillSinnenschaerfe;
    [ObservableProperty] int _skillTierkunde;
    [ObservableProperty] int _skillWildnisleben;
    [ObservableProperty] string _currentLandscape;
    [ObservableProperty] string _currentMonth;
    [ObservableProperty] string _currentRegion;
    [ObservableProperty] string _descriptionForaging = string.Empty;
    [ObservableProperty] string _descriptionKnownTerrain = string.Empty;
    [ObservableProperty] string _descriptionWildlife = string.Empty;
    public bool CanLoadFromTool => Core.IsHeldenToolInstalled;
    public Core Core { get; } = Core.GetInstance();
    public ObservableCollection<string> KnownTerrains { get; private set; } = [];
    public ObservableCollection<string> Landscapes { get; } = [];
    public string[] Month => Core.Months;
    public IEnumerable<string> Regions => from Region in Core.Regions select Region.Name;

    public MainPageViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
        CurrentMonth = Core.CurrentMonth;
        CurrentRegion = Core.CurrentRegion.Name;
        CurrentLandscape = Core.CurrentLandscape.Name;
        Ff = Core.Ff;
        Ge = Core.Ge;
        In = Core.In;
        Mu = Core.Mu;

        if (CheckForUpdates())
        {
            IsUpdateAvailable = true;
        }
    }

    public void Receive(Charakter message)
    {
        Core.LoadCharacter(message);
        LoadCharacter();
    }

    public void LoadCharacterFromFile(string filePath)
    {
        Core.LoadCharacterFromFile(filePath);
        LoadCharacter();
    }

    bool CheckForUpdates()
    {
        try
        {
#pragma warning disable SYSLIB0014
            WebClient webClient = new();
#pragma warning restore SYSLIB0014
            Version lastVersion = JsonSerializer.Deserialize<Version>(
                webClient.DownloadString(VersionLink));

            System.Version currentVersion = Assembly.GetExecutingAssembly().GetName().Version!;

            if (currentVersion.Major < lastVersion.Major)
            {
                return true;
            }

            if (currentVersion.Minor < lastVersion.Minor)
            {
                return true;
            }

            return currentVersion.Build > lastVersion.Build;
        }
        catch (Exception)
        {
            return false;
        }
    }

    void LoadCharacter()
    {
        Mu = Core.Mu;
        In = Core.In;
        Ge = Core.Ge;
        Ff = Core.Ff;
        SkillSinnenschaerfe = Core.SkillSinnenschaerfe;
        SkillSichVerstecken = Core.SkillSichVerstecken;
        SkillTierkunde = Core.SkillTierkunde;
        SkillWildnisleben = Core.SkillWildnisleben;
        SkillPflanzenkunde = Core.SkillPflanzenkunde;
        SkillSchleichen = Core.SkillSchleichen;
        KnownTerrains = [.. Core.KnownTerrains];
        IsKnownTerrain = Core.IsKnownTerrain;
    }

    [RelayCommand]
    void Update()
    {
        if (OperatingSystem.IsLinux())
        {
#pragma warning disable CS4014 // no need to wait for an link to open in browser
            Launcher.LaunchUriAsync(new($"{UpdateLinkBase}linux"));
        }
        else if (OperatingSystem.IsMacOS())
        {
            Launcher.LaunchUriAsync(new($"{UpdateLinkBase}mac"));
        }
        else
        {
            Launcher.LaunchUriAsync(new($"{UpdateLinkBase}win"));
        }
#pragma warning restore CS4014
    }

    #region character

    partial void OnMuChanged(int value)
    {
        Core.Mu = value;
    }

    partial void OnInChanged(int value)
    {
        Core.In = value;
    }

    partial void OnGeChanged(int value)
    {
        Core.Ge = value;
    }

    partial void OnFfChanged(int value)
    {
        Core.Ff = value;
    }

    partial void OnSkillWildnislebenChanged(int value)
    {
        Core.SkillWildnisleben = value;
    }

    partial void OnSkillSinnenschaerfeChanged(int value)
    {
        Core.SkillSinnenschaerfe = value;
    }

    partial void OnSkillPflanzenkundeChanged(int value)
    {
        Core.SkillPflanzenkunde = value;
    }

    partial void OnSkillTierkundeChanged(int value)
    {
        Core.SkillTierkunde = value;
    }

    partial void OnSkillFaehrtensuchenChanged(int value)
    {
        Core.SkillFaehrtensuchen = value;
    }

    partial void OnSkillSchleichenChanged(int value)
    {
        Core.SkillSchleichen = value;
    }

    partial void OnSkillSichVersteckenChanged(int value)
    {
        Core.SkillSichVerstecken = value;
    }

    #endregion character

    #region environment

    partial void OnCurrentMonthChanged(string value)
    {
        Core.CurrentMonth = value;
    }

    partial void OnCurrentRegionChanged(string value)
    {
        Core.CurrentRegion = Core.GetRegion(value);
        DescriptionForaging = Core.CurrentRegion.ForagingString;
        DescriptionWildlife = Core.CurrentRegion.WildlifeString;
        Landscapes.Clear();
        Landscapes.AddRange([.. Core.CurrentRegion.Landscapes]);

        if (!Landscapes.Contains(CurrentLandscape))
        {
            CurrentLandscape = Landscapes[0];
        }
    }

    partial void OnCurrentLandscapeChanged(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        Core.CurrentLandscape = Core.GetLandscape(value);
        string? terrain = Core.CurrentLandscape.Terrain;
        DescriptionKnownTerrain = terrain ?? string.Empty;

        IsKnownTerrain = Core.KnownTerrains.Length switch
        {
            > 0 when terrain != null && Core.KnownTerrains.Contains(terrain) => true,
            > 0 => false,
            _ => IsKnownTerrain
        };
    }

    partial void OnIsKnownTerrainChanged(bool value)
    {
        Core.IsKnownTerrain = value;
    }

    #endregion environment
}
