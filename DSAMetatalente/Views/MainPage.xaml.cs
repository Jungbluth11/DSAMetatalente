namespace DSAMetatalente.Views;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
        DropdownMonth.ItemsSource = Core.Core.Months;
        DropdownRegion.ItemsSource = from Region in Core.Core.Regions select Region.Name;
        DropdownWeapon.ItemsSource = from Weapon in Hunting.Weapons select Weapon.Name;
    }
}
