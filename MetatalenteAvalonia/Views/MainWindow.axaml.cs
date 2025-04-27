using Avalonia.Controls;

namespace Metatalente.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel viewModel = new();
        public MainWindow()
        {
            DataContext = viewModel;
            InitializeComponent();
            ResultControlForaging.Metatalent = viewModel.Foraging;
            ResultControlPlantSeeking.Metatalent = viewModel.PlantSeeking;
            ResultControlHunting.Metatalent = viewModel.Hunting;
            DropdownMonth.ItemsSource = Core.Core.Months;
            DropdownRegion.ItemsSource = from Region in Core.Core.Regions select Region.Name;
            DropdownWeapon.ItemsSource = from Weapon in Hunting.Weapons select Weapon.Name;
            TabControl.SelectedIndex = 0;
        }
    }
}