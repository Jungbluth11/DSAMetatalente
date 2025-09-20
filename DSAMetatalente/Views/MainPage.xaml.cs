namespace DSAMetatalente.Views;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (TabView.SelectedIndex == 2)
        {
            SkillTierkunde.Visibility = Visibility.Visible;
            SkillFaehrtensuchen.Visibility = Visibility.Visible;
            SkillSchleichen.Visibility = Visibility.Visible;
            SkillSichVerstecken.Visibility = Visibility.Visible;
            SkillSinnenschaerfe.Visibility = Visibility.Collapsed;
            SkillPflanzenkunde.Visibility = Visibility.Collapsed;
        }
        else
        {
            SkillTierkunde.Visibility = Visibility.Collapsed;
            SkillFaehrtensuchen.Visibility = Visibility.Collapsed;
            SkillSchleichen.Visibility = Visibility.Collapsed;
            SkillSichVerstecken.Visibility = Visibility.Collapsed;
            SkillSinnenschaerfe.Visibility = Visibility.Visible;
            SkillPflanzenkunde.Visibility = Visibility.Visible;
        }
    }

    private void MenuCharacterLoadFromTool_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void MenuCharacterLoadFromFile_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void MenuThemeLight_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void MenuThemeDark_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void MenuThemeSystem_OnClick(object sender, RoutedEventArgs e)
    {

    }

    private void MenuAbout_OnClick(object sender, RoutedEventArgs e)
    {

    }
}
