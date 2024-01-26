using System.Windows.Controls;
using Metatalente.Core;

namespace Metatalente.WPF
{
    /// <summary>
    /// Interaktionslogik für ResultControl.xaml
    /// </summary>
    public partial class ResultControl : UserControl
    {
        public MetatalentBase? Metatalent { get; set; }
        public ResultControl()
        {
            InitializeComponent();
        }

        public void SetResults()
        {
            StringDiceResult.Text = Metatalent.LastResult.DiceResult;
            StringPointsResult.Text = Metatalent.LastResult.PointsLeft;
            TBoxTextResult.Text = Metatalent.LastResult.TextResult;
        }
    }
}