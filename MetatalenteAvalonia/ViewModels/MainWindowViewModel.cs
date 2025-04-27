using System.Collections.ObjectModel;
using System.Net;
using System.Reflection;
using System.Text.Json;
using Metatalente.Core;

namespace Metatalente.Avalonia.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _mu = 8;
        [ObservableProperty]
        private int _in = 8;
        [ObservableProperty]
        private int _ge = 8;
        [ObservableProperty]
        private int _ff = 8;
        [ObservableProperty]
        private int _skillWildnisleben = 0;
        [ObservableProperty]
        private int _skillSinnenschaerfe = 0;
        [ObservableProperty]
        private int _skillPflanzenkunde = 0;
        [ObservableProperty]
        private int _skillTierkunde = 0;
        [ObservableProperty]
        private int _skillFaehrtensuchen = 0;
        [ObservableProperty]
        private int _skillSchleichen = 0;
        [ObservableProperty]
        private int _skillSichVerstecken = 0;
        [ObservableProperty]
        private int _skillWeapon = 0;
        [ObservableProperty]
        private int _durationForaging = 60;
        [ObservableProperty]
        private int _durationPlantSeeking = 60;
        [ObservableProperty]
        private int _durationHunting = 60;
        [ObservableProperty]
        private int _minDurationHunting = 60;
        [ObservableProperty]
        private string _currentMonth;
        [ObservableProperty]
        private string _currentRegion;
        [ObservableProperty]
        private string _currentLandscape;
        [ObservableProperty]
        private string _currentPlant;
        [ObservableProperty]
        private string _currentAnimal;
        [ObservableProperty]
        private string _usedWeapon;
        [ObservableProperty]
        private string _descriptionForaging = string.Empty;
        [ObservableProperty]
        private string _descriptionWildlife = string.Empty;
        [ObservableProperty]
        private string _descriptionKnownTerrain = string.Empty;
        [ObservableProperty]
        private string _plantSeekingIdentificationMod = string.Empty;
        [ObservableProperty]
        private string _plantSeekingOccurrence = string.Empty;
        [ObservableProperty]
        private string _plantSeekingLoot = string.Empty;
        [ObservableProperty]
        private string _huntingDifficulty = string.Empty;
        [ObservableProperty]
        private string _huntingLoot = string.Empty;
        [ObservableProperty]
        private bool _menuItemUpdateVisible = false;
        [ObservableProperty]
        private bool _isKnownTerrain = false;
        [ObservableProperty]
        private bool _coincidencePlantSeeking = false;
        [ObservableProperty]
        private bool _coincidenceHunting = false;
        [ObservableProperty]
        private bool _isScharfschuetze = false;
        [ObservableProperty]
        private bool _isMeisterschuetze = false;
        [ObservableProperty]
        private bool _canRollForaging = true;
        [ObservableProperty]
        private bool _canRollPlantSeeking = true;
        [ObservableProperty]
        private bool _canRollHunting = true;
        private static readonly MainWindowViewModel _instance = new();
        public Core.Core Core { get; private set; }
        public Foraging Foraging { get; private set; }
        public PlantSeeking PlantSeeking { get; private set; }
        public Hunting Hunting { get; private set; }
        public ObservableCollection<string> KnownTerrains { get; private set; } = [];
        public ObservableCollection<string> Landscapes { get; private set; } = [];
        public ObservableCollection<string> Plants { get; private set; } = [];
        public ObservableCollection<string> Animals { get; private set; } = [];
        public static MainWindowViewModel Instance => _instance;

        public MainWindowViewModel()
        {
            Core = new();
            Foraging = new(Core);
            PlantSeeking = new(Core);
            Hunting = new(Core);
            CurrentMonth = Core.CurrentMonth;
            CurrentRegion = Core.CurrentRegion.Name;
            CurrentLandscape = Core.CurrentLandscape.Name;
            CurrentPlant = PlantSeeking.CurrentPlant.Name;
            CurrentAnimal = Hunting.CurrentAnimal.Name;
            UsedWeapon = Hunting.UsedWeapon.Name;
            if (CheckForUpdates())
            {
                MenuItemUpdateVisible = true;
            }
        }

        private bool CheckForUpdates()
        {
            try
            {
                WebClient webClient = new();
                Version lastVersion = JsonSerializer.Deserialize<Version>(webClient.DownloadString("https://api.jungbluthcloud.de/updates/dsametatalente/version"));
                System.Version? currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                if (currentVersion.Major < lastVersion.Major)
                {
                    return true;
                }
                else if (currentVersion.Minor < lastVersion.Minor)
                {
                    return true;
                }
                else if (currentVersion.Build < lastVersion.Build)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void CheckCanRoll()
        {
            Foraging.SetSkill();
            PlantSeeking.SetSkill();
            Hunting.SetSkill();
            CanRollPlantSeeking = PlantSeeking.IsSet;

            if (Core.CurrentRegion.ForagingMod != null)
            {
                CanRollForaging = Foraging.IsSet;
            }

            if (Core.CurrentRegion.WildlifeMod != null)
            {
                CanRollHunting = Hunting.IsSet;
            }
        }

        private void LoadCharacter()
        {
            try
            {
                Hunting.LoadWeaponFromCharacter();
                UsedWeapon = Hunting.UsedWeapon.Name;
                SkillWeapon = Core.SkillWeapon;
                IsScharfschuetze = Hunting.IsScharfschuetze;
                IsMeisterschuetze = Hunting.IsMeisterschuetze;
            }
            catch { }
            finally
            {
                Mu = Core.MU;
                In = Core.IN;
                Ge = Core.GE;
                Ff = Core.FF;
                SkillWeapon = Core.SkillWeapon;
                SkillSinnenschaerfe = Core.SkillSinnenschaerfe;
                SkillSichVerstecken = Core.SkillSichVerstecken;
                SkillTierkunde = Core.SkillTierkunde;
                SkillWildnisleben = Core.SkillWildnisleben;
                SkillPflanzenkunde = Core.SkillPflanzenkunde;
                SkillSchleichen = Core.SkillSchleichen;
                KnownTerrains = [.. Core.KnownTerrains];
                IsKnownTerrain = Core.IsKnownTerrain;
            }
        }

        #region character

        partial void OnMuChanged(int value)
        {
            Core.MU = value;
        }

        partial void OnInChanged(int value)
        {
            Core.IN = value;
        }

        partial void OnGeChanged(int value)
        {
            Core.GE = value;
        }

        partial void OnFfChanged(int value)
        {
            Core.FF = value;
        }

        partial void OnSkillWildnislebenChanged(int value)
        {
            Core.SkillWildnisleben = value;
            CheckCanRoll();
        }

        partial void OnSkillSinnenschaerfeChanged(int value)
        {
            Core.SkillSinnenschaerfe = value;
            CheckCanRoll();
        }

        partial void OnSkillPflanzenkundeChanged(int value)
        {
            Core.SkillPflanzenkunde = value;
            CheckCanRoll();
        }

        partial void OnSkillTierkundeChanged(int value)
        {
            Core.SkillTierkunde = value;
            CheckCanRoll();
        }

        partial void OnSkillFaehrtensuchenChanged(int value)
        {
            Core.SkillFaehrtensuchen = value;
            CheckCanRoll();
        }

        partial void OnSkillSchleichenChanged(int value)
        {
            Core.SkillSchleichen = value;
            CheckCanRoll();
        }

        partial void OnSkillSichVersteckenChanged(int value)
        {
            Core.SkillSichVerstecken = value;
            CheckCanRoll();
        }

        #endregion

        #region environment

        partial void OnCurrentMonthChanged(string value)
        {
            Core.CurrentMonth = value;
            SetPlantList();
        }

        partial void OnCurrentRegionChanged(string value)
        {
            Core.CurrentRegion = Metatalente.Core.Core.GetRegion(value);
            DescriptionForaging = Core.CurrentRegion.ForagingString;
            DescriptionWildlife = Core.CurrentRegion.WildlifeString;
            Landscapes = [.. Core.CurrentRegion.Landscapes];
            Animals = [.. Core.CurrentRegion.Animals];
            CurrentAnimal = Animals.Count > 0 ? Animals[0] : string.Empty;
            CanRollForaging = Core.CurrentRegion.ForagingMod == null ? false : true;
            CanRollHunting = Core.CurrentRegion.WildlifeMod == null ? false : true;
            SetPlantList();
            SetPlantDescription();
        }

        partial void OnCurrentLandscapeChanged(string value)
        {
            Core.CurrentLandscape = Metatalente.Core.Core.GetLandscape(value);
            string? terrain = Core.CurrentLandscape.Terrain;
            DescriptionKnownTerrain = terrain ?? string.Empty;
            SetPlantList();
            SetPlantDescription();
            if (Core.KnownTerrains.Length > 0 && terrain != null && Core.KnownTerrains.Contains(terrain))
            {
                IsKnownTerrain = true;
            }
            else if (Core.KnownTerrains.Length > 0)
            {
                IsKnownTerrain = false;
            }
        }

        partial void OnIsKnownTerrainChanged(bool value)
        {
            Core.IsKnownTerrain = value;
        }

        #endregion

        #region foraging


        partial void OnDurationForagingChanged(int value)
        {
            Foraging.Duration = value;
        }


        #endregion

        #region plant seeking


        partial void OnDurationPlantSeekingChanged(int value)
        {
            PlantSeeking.Duration = value;
        }

        private void SetPlantList()
        {
            Plant[] plants = Core.CurrentRegion.GetPossiblePlants(Core.CurrentLandscape.Name, Core.CurrentMonth);
            if (plants.Length == 0)
            {
                CurrentPlant = string.Empty;
                CanRollPlantSeeking = false;
                return;
            }

            Plants = [.. from Plant in plants select Plant.Name];

            if (!(from Plant in plants select Plant.Name).Contains(CurrentPlant))
            {
                CurrentPlant = Plants.First();
            }
        }

        private void SetPlantDescription()
        {
            try
            {
                string identificationModText = PlantSeeking.CurrentPlant.IdentificationMod.ToString();

                if (PlantSeeking.CurrentPlant.IdentificationMod > 0)
                {
                    identificationModText = identificationModText.Insert(0, "+");
                }

                PlantSeekingIdentificationMod = identificationModText;
                PlantSeekingOccurrence = Metatalente.Core.Core.OccurToString(PlantSeeking.GetOccurrenceMod(PlantSeeking.CurrentPlant));
                PlantSeekingLoot = PlantSeeking.CurrentPlant.LootDisplayText;
            }
            catch
            {
                PlantSeekingIdentificationMod = string.Empty;
                PlantSeekingOccurrence = string.Empty;
                PlantSeekingLoot = string.Empty;
            }
        }

        #endregion

        #region hunting

        partial void OnSkillWeaponChanged(int value)
        {
            Core.SkillWeapon = value;
        }

        partial void OnDurationHuntingChanged(int value)
        {
            Hunting.Duration = value;
        }

        #endregion
    }
}
