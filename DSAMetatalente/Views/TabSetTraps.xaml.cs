namespace DSAMetatalente.Views;
public sealed partial class TabSetTraps : TabViewItem
{
    public TabSetTraps()
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
