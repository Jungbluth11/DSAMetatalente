namespace DSAMetatalente.Views;

public sealed partial class TabPlantSeeking : TabViewItem
{
    public TabPlantSeeking()
    {
        InitializeComponent();
    }

    private void UIElement_OnKeyDown(object sender, KeyRoutedEventArgs e)
    {
        if (e.Key == VirtualKey.Enter)
        {
            XamlRoot!.Content!.Focus(FocusState.Programmatic);
        }
    }
}