using Avalonia.Controls;

namespace Metatalente.Avalonia.Views;

public partial class ResultControl : UserControl
{
    public MetatalentBase? Metatalent { get; set; }

    public ResultControl()
    {
        DataContext = new ResultControlViewModel();
        InitializeComponent();
    }

    public void SetResults()
    {
        StringDiceResult.Text = Metatalent.LastResult.DiceResult;
        StringPointsResult.Text = Metatalent.LastResult.PointsLeft;
        TBoxTextResult.Text = Metatalent.LastResult.TextResult;
    }
}