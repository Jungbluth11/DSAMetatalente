using Version = DSAMetatalente.Models.Version;

namespace DSAMetatalente.ViewModels;

public partial class MainPageViewModel : ObservableObject, IRecipient<Charakter>
{
    private const string UpdateLinkBase = "https://api.jungbluthcloud.de/updates/dsametatalente/";
    private const string VersionLink = "https://api.jungbluthcloud.de/updates/dsametatalente/version";
    private readonly Core _core = Core.GetInstance();
    [ObservableProperty] private bool _isTerrainKnowledge;
    [ObservableProperty] private bool _isLocalKnowledge;
    [ObservableProperty] private bool _isUpdateAvailable;
    [ObservableProperty] private int _ff;
    [ObservableProperty] private int _ge;
    [ObservableProperty] private int _in;
    [ObservableProperty] private int _kk;
    [ObservableProperty] private int _kl;
    [ObservableProperty] private int _mu;
    [ObservableProperty] private int _skillFaehrtensuchen;
    [ObservableProperty] private int _skillFallenstellen;
    [ObservableProperty] private int _skillFischenAngeln;
    [ObservableProperty] private int _skillPflanzenkunde;
    [ObservableProperty] private int _skillSchleichen;
    [ObservableProperty] private int _skillSichVerstecken;
    [ObservableProperty] private int _skillSinnenschaerfe;
    [ObservableProperty] private int _skillTierkunde;
    [ObservableProperty] private int _skillWildnisleben;
    [ObservableProperty] private string _currentLandscape;
    [ObservableProperty] private string _currentMonth;
    [ObservableProperty] private string _currentRegion;
    [ObservableProperty] private string _descriptionForaging = string.Empty;
    [ObservableProperty] private string _descriptionTerrainKnowledge = string.Empty;
    [ObservableProperty] private string _descriptionWildlife = string.Empty;
    [ObservableProperty] private string _loadedCharacter = string.Empty;
    public bool CanLoadFromTool => _core.IsHeldenToolInstalled;
    public ObservableCollection<string> TerrainKnowledges { get; } = [];
    public ObservableCollection<string> Landscapes { get; } = [];
    public string[] Month => Core.Months;
    public IEnumerable<string> Regions => from Region in Core.Regions select Region.Name;

    public MainPageViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
        CurrentMonth = _core.CurrentMonth;
        CurrentRegion = _core.CurrentRegion.Name;
        CurrentLandscape = _core.CurrentLandscape.Name;
        Ff = _core.Ff;
        Ge = _core.Ge;
        In = _core.In;
        Kk = _core.Kk;
        Kl = _core.Kl;
        Mu = _core.Mu;

        if (CheckForUpdates())
        {
            IsUpdateAvailable = true;
        }

#if DEBUG
        Ff = 20;
        Ge = 20;
        In = 20;
        Kk = 20;
        Kl = 20;
        Mu = 20;
        SkillFaehrtensuchen = 20;
        SkillPflanzenkunde = 20;
        SkillSchleichen = 20;
        SkillSichVerstecken = 20;
        SkillSinnenschaerfe = 20;
        SkillTierkunde = 20;
        SkillWildnisleben = 20;
        SkillFallenstellen = 20;
        SkillFischenAngeln = 20;
        CurrentRegion = "Mittelländische Wälder (Gemäßigtes Klima)";
#endif
    }

    public void Receive(Charakter character)
    {
        _core.LoadCharacter(character);
        LoadCharacter();
    }

    public void LoadCharacterFromFile(string filePath)
    {
        _core.LoadCharacterFromFile(filePath);
        LoadCharacter();
    }

    private bool CheckForUpdates()
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

    private void LoadCharacter()
    {
        Mu = _core.Mu;
        In = _core.In;
        Ge = _core.Ge;
        Ff = _core.Ff;
        SkillSinnenschaerfe = _core.SkillSinnenschaerfe;
        SkillSichVerstecken = _core.SkillSichVerstecken;
        SkillTierkunde = _core.SkillTierkunde;
        SkillWildnisleben = _core.SkillWildnisleben;
        SkillPflanzenkunde = _core.SkillPflanzenkunde;
        SkillSchleichen = _core.SkillSchleichen;
        SkillFaehrtensuchen = _core.SkillFaehrtensuchen;
        TerrainKnowledges.Clear();
        TerrainKnowledges.AddRange([.. _core.TerrainKnowledges]);
        IsTerrainKnowledge = _core.IsTerrainKnowledge;
        LoadedCharacter = _core.LoadedCharacterName;
    }

    [RelayCommand]
    private void Update()
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
        _core.Mu = value;
    }


    partial void OnKlChanged(int value)
    {
        _core.Kl = value;
    }

    partial void OnInChanged(int value)
    {
        _core.In = value;
    }

    partial void OnGeChanged(int value)
    {
        _core.Ge = value;
    }

    partial void OnFfChanged(int value)
    {
        _core.Ff = value;
    }

    partial void OnKkChanged(int value)
    {
        _core.Kk = value;
    }

    partial void OnSkillWildnislebenChanged(int value)
    {
        _core.SkillWildnisleben = value;
    }

    partial void OnSkillSinnenschaerfeChanged(int value)
    {
        _core.SkillSinnenschaerfe = value;
    }

    partial void OnSkillPflanzenkundeChanged(int value)
    {
        _core.SkillPflanzenkunde = value;
    }

    partial void OnSkillTierkundeChanged(int value)
    {
        _core.SkillTierkunde = value;
    }

    partial void OnSkillFallenstellenChanged(int value)
    {
        _core.SkillFallenstellen = value;
    }

    partial void OnSkillFischenAngelnChanged(int value)
    {
        _core.SkillFischenAngeln = value;
    }

    partial void OnSkillFaehrtensuchenChanged(int value)
    {
        _core.SkillFaehrtensuchen = value;
    }

    partial void OnSkillSchleichenChanged(int value)
    {
        _core.SkillSchleichen = value;
    }

    partial void OnSkillSichVersteckenChanged(int value)
    {
        _core.SkillSichVerstecken = value;
    }

    #endregion character

    #region environment

    partial void OnCurrentMonthChanged(string value)
    {
        _core.CurrentMonth = value;
    }

    partial void OnCurrentRegionChanged(string value)
    {
        _core.CurrentRegion = Core.GetRegion(value);
        DescriptionForaging = _core.CurrentRegion.ForagingString;
        DescriptionWildlife = _core.CurrentRegion.WildlifeString;
        Landscapes.Clear();
        Landscapes.AddRange([.. _core.CurrentRegion.Landscapes]);

        if (!Landscapes.Contains(CurrentLandscape))
        {
            CurrentLandscape = Landscapes[0];
        }

        IsLocalKnowledge = false;
    }

    partial void OnCurrentLandscapeChanged(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return;
        }

        _core.CurrentLandscape = Core.GetLandscape(value);
        string? terrain = _core.CurrentLandscape.Terrain;
        DescriptionTerrainKnowledge = terrain ?? string.Empty;

        IsTerrainKnowledge = _core.TerrainKnowledges.Length switch
        {
            > 0 when terrain != null && _core.TerrainKnowledges.Contains(terrain) => true,
            > 0 => false,
            _ => IsTerrainKnowledge
        };
    }

    partial void OnIsTerrainKnowledgeChanged(bool value)
    {
        _core.IsTerrainKnowledge = value;
    }

    partial void OnIsLocalKnowledgeChanged(bool value)
    {
        _core.IsLocalKnowledge = value;
    }

    #endregion environment
}