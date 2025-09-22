namespace DSAMetatalente.Views;

public sealed partial class MainPage : Page
{
    private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;

    public MainPage()
    {
        InitializeComponent();
    }

    private void MenuAbout_OnClick(object sender, RoutedEventArgs e)
    {
    }

    private async void MenuCharacterLoadFromFile_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            FileOpenPicker fileOpenPicker = new()
            {
                FileTypeFilter = { ".xml" },
                CommitButtonText = "Auswählen"
            };

            StorageFile file = await fileOpenPicker.PickSingleFileAsync();

            try
            {
                if (file == null)
                {
                    return;
                }

                (DataContext as MainPageViewModel)!.LoadCharacterFromFile(file.Path);
            }
            catch
            {
                throw new(file.Path + " ist keine gültige Heldentool-Datei oder kann nicht gelesen werden");
            }
        }
        catch (Exception ex)
        {
            ContentDialog dialog = new()
            {
                Content = ex.Message,
                XamlRoot = XamlRoot,
                CloseButtonText = "OK"
            };

            await dialog.ShowAsync();
        }
    }

    private async void MenuCharacterLoadFromTool_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            ContentDialog dialog = new LoadFromToolDialog
            {
                XamlRoot = XamlRoot
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            ContentDialog dialog = new()
            {
                Content = ex.Message,
                XamlRoot = XamlRoot,
                CloseButtonText = "OK"
            };

            await dialog.ShowAsync();
        }
    }

    private void MenuThemeDark_OnClick(object sender, RoutedEventArgs e)
    {
        (XamlRoot!.Content as FrameworkElement)!.RequestedTheme = ElementTheme.Dark;
        _localSettings.Values["theme"] = nameof(ElementTheme.Dark);
    }

    private void MenuThemeLight_OnClick(object sender, RoutedEventArgs e)
    {
        (XamlRoot!.Content as FrameworkElement)!.RequestedTheme = ElementTheme.Light;
        _localSettings.Values["theme"] = nameof(ElementTheme.Light);
    }

    private void MenuThemeSystem_OnClick(object sender, RoutedEventArgs e)
    {
        (XamlRoot!.Content as FrameworkElement)!.RequestedTheme = ElementTheme.Default;
        _localSettings.Values["theme"] = nameof(ElementTheme.Default);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        switch ((string)_localSettings.Values["theme"])
        {
            case "Light":
                (XamlRoot!.Content as FrameworkElement)!.RequestedTheme = ElementTheme.Light;
                MenuThemeLight.IsChecked = true;

                break;
            case "Dark":
                (XamlRoot!.Content as FrameworkElement)!.RequestedTheme = ElementTheme.Dark;
                MenuThemeDark.IsChecked = true;

                break;
            default:
                (XamlRoot!.Content as FrameworkElement)!.RequestedTheme = ElementTheme.Default;
                MenuThemeSystem.IsChecked = true;

                break;
        }
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
}
