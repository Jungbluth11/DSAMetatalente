using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using DSAUtils.UI.WPF;
using Metatalente.Core;
using Microsoft.Win32;

namespace Metatalente.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Core.Core core;
        private readonly Foraging foraging;
        private readonly PlantSeeking plantSeeking;
        private readonly Hunting hunting;

        public MainWindow()
        {
            core = new();
            foraging = new(core);
            plantSeeking = new(core);
            hunting = new(core);
            InitializeComponent();
            MenuItemCharacterLoadFromTool.IsEnabled = core.IsHeldenToolInstalled;
            ResultControlForaging.Metatalent = foraging;
            ResultControlPlantSeeking.Metatalent = plantSeeking;
            ResultControlHunting.Metatalent = hunting;
            DropdownMonth.ItemsSource = Core.Core.Months;
            DropdownMonth.SelectedIndex = 0;
            DropdownRegion.ItemsSource = from Region in Core.Core.Regions select Region.Name;
            DropdownRegion.SelectedIndex = 0;
            DropdownWeapon.ItemsSource = from Weapon in Hunting.Weapons select Weapon.Name;
            DropdownWeapon.SelectedIndex = 0;
            TabControl.SelectedIndex = 0;
            if (CheckForUpdates())
            {
                MenuItemUpdate.Visibility = Visibility.Visible;
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
            foraging.SetSkill();
            plantSeeking.SetSkill();
            hunting.SetSkill();
            BtnRollPlantseeking.IsEnabled = plantSeeking.IsSet;
            if (core.CurrentRegion.ForagingMod != null)
            {
                BtnRollForaging.IsEnabled = foraging.IsSet;
            }
            if (core.CurrentRegion.WildlifeMod != null)
            {
                BtnRollHunting.IsEnabled = hunting.IsSet;
            }
        }

        private void LoadCharacter()
        {
            try
            {
                hunting.LoadWeaponFromCharacter();
                DropdownWeapon.SelectedValue = hunting.UsedWeapon.Name;
                InputSkillWeapon.Value = core.SkillWeapon;
                CbIsScharfschuetze.IsChecked = hunting.IsScharfschuetze;
                CbIsMeisterschuetze.IsChecked = hunting.IsMeisterschuetze;
            }
            catch { }
            finally
            {
                InputMU.Value = core.MU;
                InputIN.Value = core.IN;
                InputGE.Value = core.GE;
                InputFF.Value = core.FF;
                InputSkillWeapon.Value = core.SkillWeapon;
                InputSkillSinnenschaerfe.Value = core.SkillSinnenschaerfe;
                InputSkillSichVerstecken.Value = core.SkillSichVerstecken;
                InputSkillTierkunde.Value = core.SkillTierkunde;
                InputSkillWildnisleben.Value = core.SkillWildnisleben;
                InputSkillPflanzenkunde.Value = core.SkillPflanzenkunde;
                InputSkillSchleichen.Value = core.SkillSchleichen;
                KnownTerrainsPanel.ItemsSource = core.KnownTerrains;
                CbIsKnownTerrain.IsChecked = core.IsKnownTerrain;
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabItem selectedTab = (TabItem)TabControl.SelectedItem;
            if (selectedTab == TabForaging || selectedTab == TabPlantSeeking)
            {
                GridSkillSinnenschaerfe.Visibility = Visibility.Visible;
                GridSkillPflanzenkunde.Visibility = Visibility.Visible;
                GridSkillTierkunde.Visibility = Visibility.Collapsed;
                GridSkillFaehrtensuchen.Visibility = Visibility.Collapsed;
                GridSkillSchleichen.Visibility = Visibility.Collapsed;
                GridSkillSichVerstecken.Visibility = Visibility.Collapsed;
            }
            else
            {
                GridSkillSinnenschaerfe.Visibility = Visibility.Collapsed;
                GridSkillPflanzenkunde.Visibility = Visibility.Collapsed;
                GridSkillTierkunde.Visibility = Visibility.Visible;
                GridSkillFaehrtensuchen.Visibility = Visibility.Visible;
                GridSkillSchleichen.Visibility = Visibility.Visible;
                GridSkillSichVerstecken.Visibility = Visibility.Visible;
            }
        }

        private void MenuItemCharacterLoadFromTool_Click(object sender, RoutedEventArgs e)
        {
            LoadFromToolWindow loadFromToolWindow = new(core.GetCharactersFromTool(), core.LoadCharacter);
            loadFromToolWindow.ShowDialog();
            LoadCharacter();
        }

        private void MenuItemCharacterLoadFromFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "XML | *.xml";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    core.LoadCharacterFromFile(openFileDialog.FileName);
                    LoadCharacter();
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(ex.Message, "Metatalente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        private void MenuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            new About("Metatalente", "1.0.0").Show();
        }

        private void MenuItemUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start("explorer.exe", "https://api.jungbluthcloud.de/updates/dsametatalente/link");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Metatalente", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        #region character

        private void InputMU_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.MU = InputMU.Value;
        }

        private void InputIN_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.IN = InputIN.Value;
        }

        private void InputGE_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.GE = InputGE.Value;
        }

        private void InputFF_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.FF = InputFF.Value;
        }

        private void InputSkillWildnisleben_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.SkillWildnisleben = InputSkillWildnisleben.Value;
            CheckCanRoll();
        }

        private void InputSkillSinnenschaerfe_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.SkillSinnenschaerfe = InputSkillSinnenschaerfe.Value;
            CheckCanRoll();
        }

        private void InputSkillPflanzenkunde_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.SkillPflanzenkunde = InputSkillPflanzenkunde.Value;
            CheckCanRoll();
        }

        private void InputSkillTierkunde_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.SkillTierkunde = InputSkillTierkunde.Value;
            CheckCanRoll();
        }

        private void InputSkillFaehrtensuchen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.SkillFaehrtensuchen = InputSkillFaehrtensuchen.Value;
            CheckCanRoll();
        }

        private void InputSkillSchleichen_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.SkillSchleichen = InputSkillSchleichen.Value;
            CheckCanRoll();
        }

        private void InputSkillSichVerstecken_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.SkillSichVerstecken = InputSkillSchleichen.Value;
            CheckCanRoll();
        }

        #endregion character

        #region environment

        private void DropdownMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
#pragma warning disable CS8601
            core.CurrentMonth = DropdownMonth.SelectedValue.ToString();
            SetPlantList();
#pragma warning restore CS8601
        }

        private void DropdownRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
#pragma warning disable CS8604
            core.CurrentRegion = Core.Core.GetRegion(DropdownRegion.SelectedValue.ToString());
#pragma warning restore CS8604
            DropdownLandscape.ItemsSource = core.CurrentRegion.Landscapes;
            DropdownLandscape.SelectedIndex = 0;
            StringForaging.Text = core.CurrentRegion.ForagingString;
            StringWildlife.Text = core.CurrentRegion.WildlifeString;
            DropdownAnimal.ItemsSource = core.CurrentRegion.Animals;
            if (core.CurrentRegion.Animals.Length > 0)
            {
                DropdownAnimal.SelectedIndex = 0;
            }
            BtnRollForaging.IsEnabled = core.CurrentRegion.ForagingMod == null ? false : true;
            BtnRollHunting.IsEnabled = core.CurrentRegion.WildlifeMod == null ? false : true;
            SetPlantList();
            SetPlantDescription();
        }

        private void DropdownLandscape_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DropdownLandscape.SelectedValue != null)
            {
#pragma warning disable CS8604
                core.CurrentLandscape = Core.Core.GetLandscape(DropdownLandscape.SelectedValue.ToString());
#pragma warning restore CS8604
                string? terrain = core.CurrentLandscape.Terrain;
                StringKnownTerrain.Text = terrain ?? string.Empty;
                SetPlantList();
                SetPlantDescription();
                if (core.KnownTerrains.Length > 0 && terrain != null && core.KnownTerrains.Contains(terrain))
                {
                    CbIsKnownTerrain.IsChecked = true;
                }
                else if (core.KnownTerrains.Length > 0)
                {
                    CbIsKnownTerrain.IsChecked = false;
                }
            }
        }

        private void CbIsKnownTerrain_CheckedChanged(object sender, RoutedEventArgs e)
        {
            core.IsKnownTerrain = (bool)CbIsKnownTerrain.IsChecked;
        }

        #endregion environment

        #region foraging

        private void InputDurationForaging_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            foraging.Duration = InputDurationForaging.Value;
        }

        private void BtnRollForaging_Click(object sender, RoutedEventArgs e)
        {
            foraging.Roll();
            ResultControlForaging.SetResults();
        }

        #endregion foraging

        #region plant seeking

        private void InputDurationPlantSeeking_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            plantSeeking.Duration = InputDurationPlantSeeking.Value;
        }

        private void BtnRollPlantseeking_Click(object sender, RoutedEventArgs e)
        {
            plantSeeking.Roll();
            ResultControlPlantSeeking.SetResults();
        }

        private void CbCoincidencePlantSeeking_CheckedChanged(object sender, RoutedEventArgs e)
        {
            plantSeeking.Coincidence = (bool)CbCoincidencePlantSeeking.IsChecked;
            DropdownPlant.IsEnabled = (bool)CbCoincidencePlantSeeking.IsChecked ? false : true;
        }

        private void DropdownPlant_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DropdownPlant.SelectedValue != null)
            {
                plantSeeking.CurrentPlant = PlantSeeking.Plants.SingleOrDefault(p => p.Name == DropdownPlant.SelectedValue.ToString());
                SetPlantDescription();
            }
        }

        private void SetPlantList()
        {
            Plant[] plants = core.CurrentRegion.GetPossiblePlants(core.CurrentLandscape.Name, core.CurrentMonth);
            DropdownPlant.ItemsSource = from Plant in plants select Plant.Name;
            if (DropdownPlant.SelectedIndex > 0 && !(from Plant in plants select Plant.Name).Contains(DropdownPlant.SelectedValue.ToString()) || DropdownPlant.SelectedIndex == -1)
            {
                DropdownPlant.SelectedIndex = 0;
            }
        }

        private void SetPlantDescription()
        {
            string identificationModText = plantSeeking.CurrentPlant.IdentificationMod.ToString();
            if (plantSeeking.CurrentPlant.IdentificationMod > 0)
            {
                identificationModText = identificationModText.Insert(0, "+");
            }
            StringIdentificationMod.Text = identificationModText;
            StringOccurrence.Text = Core.Core.OccurToString(plantSeeking.GetOccurrenceMod(plantSeeking.CurrentPlant));
            StringLootPlantSeeking.Text = plantSeeking.CurrentPlant.LootDisplayText;
        }

        #endregion plant seeking

        #region hunting

        private void InputDurationHunting_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            hunting.Duration = InputDurationHunting.Value;
        }

        private void InputSkillWeapon_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            core.SkillWeapon = InputSkillWeapon.Value;
        }

        private void BtnRollHunting_Click(object sender, RoutedEventArgs e)
        {
            hunting.Roll();
            ResultControlHunting.SetResults();
        }

        private void CbCoincidenceHunting_CheckedChanged(object sender, RoutedEventArgs e)
        {
            hunting.Coincidence = (bool)CbCoincidenceHunting.IsChecked;
            DropdownAnimal.IsEnabled = (bool)CbCoincidenceHunting.IsChecked ? false : true;
        }

        private void CbIsScharfschuetze_CheckedChanged(object sender, RoutedEventArgs e)
        {
            hunting.IsScharfschuetze = (bool)CbIsScharfschuetze.IsChecked;
        }

        private void CbIsMeisterschuetze_CheckedChanged(object sender, RoutedEventArgs e)
        {
            hunting.IsMeisterschuetze = (bool)CbIsMeisterschuetze.IsChecked;
        }

        private void DropdownAnimal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DropdownAnimal.SelectedValue != null)
            {
#pragma warning disable CS8604
                hunting.CurrentAnimal = hunting.GetAnimalByName(DropdownAnimal.SelectedValue.ToString());
                StringDifficulty.Text = "+" + hunting.CurrentAnimal.Difficulty.ToString();
                StringLootHunting.Text = hunting.CurrentAnimal.LootDisplayText;
#pragma warning restore CS8604
            }
        }

        private void DropdownWeapon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DropdownAnimal.SelectedValue != null)
            {
#pragma warning disable CS8604
                hunting.UsedWeapon = hunting.GetWeaponByName(DropdownWeapon.SelectedValue.ToString());
#pragma warning restore CS8604
            }
            hunting.SetSkill();
            BtnRollHunting.IsEnabled = hunting.IsSet;
        }

        private void RadioHuntingSkillPirschjagd_Checked(object sender, RoutedEventArgs e)
        {
            hunting.Variant = Hunting.Variants.Pirschjagd;
            InputDurationHunting.Value = hunting.MinDuration;
            InputDurationHunting.MinValue = hunting.MinDuration;
        }

        private void RadioHuntingSkillAnsitzjagd_Checked(object sender, RoutedEventArgs e)
        {
            hunting.Variant = Hunting.Variants.Ansitzjagd;
            InputDurationHunting.Value = hunting.MinDuration;
            InputDurationHunting.MinValue = hunting.MinDuration;
        }

        #endregion hunting
    }
}