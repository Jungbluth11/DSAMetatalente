namespace DSAMetatalente.Views;

public sealed partial class MainPage : Page
{
    private readonly ApplicationDataContainer _localSettings = ApplicationData.Current.LocalSettings;
    private readonly List<string> _addiontialTabs = [];
    private MainPageViewModel ViewModel => (MainPageViewModel) DataContext;

    public MainPage()
    {
        InitializeComponent();
        ViewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        switch ((string) _localSettings.Values["theme"])
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

        foreach (string tab in JsonSerializer.Deserialize<string[]>((string) _localSettings.Values["additionalTabs"])!)
        {
            if (tab == typeof(TabSetTraps).ToString())
            {
                _addiontialTabs.Add(tab);
                TabView.TabItems.Add(new TabSetTraps());
                MenuTabSetTraps.IsChecked = true;
            }
            else if (tab == typeof(TabFishing).ToString())
            {
                _addiontialTabs.Add(tab);
                TabView.TabItems.Add(new TabFishing());
                MenuTabFishing.IsChecked = true;
            }
        }

        if (_addiontialTabs.Count == 0)
        {
            return;
        }

        TextKl.Visibility = Visibility.Visible;
        TextKk.Visibility = Visibility.Visible;
        NumberBoxKl.Visibility = Visibility.Visible;
        NumberBoxKk.Visibility = Visibility.Visible;
    }

    private async void MenuAbout_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            AboutDialog dialog = new()
            {
                XamlRoot = XamlRoot
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);
        }
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

                ViewModel.LoadCharacterFromFile(file.Path);
            }
            catch
            {
                throw new(file.Path + " ist keine gültige Helden-Software Datei oder kann nicht gelesen werden");
            }
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);

        }
    }

    private async void MenuCharacterLoadFromTool_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            LoadFromToolDialog dialog = new()
            {
                XamlRoot = XamlRoot
            };

            await dialog.ShowAsync();
        }
        catch (Exception ex)
        {
            await ErrorMessageHelper.ShowErrorDialog(ex.Message, XamlRoot!);

        }
    }

    private void MenuTabFishing_OnClick(object sender, RoutedEventArgs e)
    {
        SetTabs<TabFishing>();
    }

    private void MenuTabSetTraps_OnClick(object sender, RoutedEventArgs e)
    {
        SetTabs<TabSetTraps>();
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

    private void TabView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        switch (TabView.SelectedItem)
        {
            case TabHunting:
                SkillTierkunde.Visibility = Visibility.Visible;
                SkillFaehrtensuchen.Visibility = Visibility.Visible;
                SkillSchleichen.Visibility = Visibility.Visible;
                SkillSichVerstecken.Visibility = Visibility.Visible;
                SkillSinnenschaerfe.Visibility = Visibility.Collapsed;
                SkillPflanzenkunde.Visibility = Visibility.Collapsed;
                SkillFallenstellen.Visibility = Visibility.Collapsed;
                SkillFischenAngeln.Visibility = Visibility.Collapsed;
                SkillWildnisleben.Visibility = Visibility.Visible;

                break;
            case TabSetTraps:
                SkillTierkunde.Visibility = Visibility.Collapsed;
                SkillFaehrtensuchen.Visibility = Visibility.Collapsed;
                SkillSchleichen.Visibility = Visibility.Collapsed;
                SkillSichVerstecken.Visibility = Visibility.Collapsed;
                SkillSinnenschaerfe.Visibility = Visibility.Collapsed;
                SkillPflanzenkunde.Visibility = Visibility.Collapsed;
                SkillFallenstellen.Visibility = Visibility.Visible;
                SkillFischenAngeln.Visibility = Visibility.Collapsed;
                SkillWildnisleben.Visibility = Visibility.Collapsed;

                break;
            case TabFishing:
                SkillTierkunde.Visibility = Visibility.Collapsed;
                SkillFaehrtensuchen.Visibility = Visibility.Collapsed;
                SkillSchleichen.Visibility = Visibility.Collapsed;
                SkillSichVerstecken.Visibility = Visibility.Collapsed;
                SkillSinnenschaerfe.Visibility = Visibility.Collapsed;
                SkillPflanzenkunde.Visibility = Visibility.Collapsed;
                SkillFallenstellen.Visibility = Visibility.Collapsed;
                SkillFischenAngeln.Visibility = Visibility.Visible;
                SkillWildnisleben.Visibility = Visibility.Collapsed;

                break;
            default:
                SkillTierkunde.Visibility = Visibility.Collapsed;
                SkillFaehrtensuchen.Visibility = Visibility.Collapsed;
                SkillSchleichen.Visibility = Visibility.Collapsed;
                SkillSichVerstecken.Visibility = Visibility.Collapsed;
                SkillSinnenschaerfe.Visibility = Visibility.Visible;
                SkillPflanzenkunde.Visibility = Visibility.Visible;
                SkillFallenstellen.Visibility = Visibility.Collapsed;
                SkillFischenAngeln.Visibility = Visibility.Collapsed;
                SkillWildnisleben.Visibility = Visibility.Visible;

                break;
        }
    }

    private void UIElement_OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            XamlRoot!.Content!.Focus(FocusState.Programmatic);
        }
    }

    private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "LoadedCharacter")
        {
            ApplicationView.GetForCurrentView().Title = $"Metatalente - {ViewModel.LoadedCharacter}";
        }
    }

    private void SetTabs<T>() where T : TabViewItem, new()
    {
        if (!TabView.TabItems.OfType<T>().Any())
        {
            _addiontialTabs.Add(typeof(T).ToString());
            TabView.TabItems.Add(new T());
            TabView.SelectedItem = TabView.TabItems.OfType<T>().First();
        }
        else
        {
            _addiontialTabs.Remove(typeof(T).ToString());
            TabView.TabItems.Remove(TabView.TabItems.OfType<T>().First());
        }

        if (_addiontialTabs.Count > 0)
        {
            TextKl.Visibility = Visibility.Visible;
            TextKk.Visibility = Visibility.Visible;
            NumberBoxKl.Visibility = Visibility.Visible;
            NumberBoxKk.Visibility = Visibility.Visible;
        }
        else
        {
            TextKl.Visibility = Visibility.Collapsed;
            TextKk.Visibility = Visibility.Collapsed;
            NumberBoxKl.Visibility = Visibility.Collapsed;
            NumberBoxKk.Visibility = Visibility.Collapsed;
        }

        _localSettings.Values["additionalTabs"] = JsonSerializer.Serialize(_addiontialTabs.ToArray());
    }
}