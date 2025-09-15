namespace DSAMetatalente.Controls;

public sealed partial class ResultControl : UserControl
{
    public string DiceResult
    {
        get => (string)GetValue(DiceResultProperty);
        set => SetValue(DiceResultProperty, value);
    }

    public string PointsResult
    {
        get => (string)GetValue(PointsResultProperty);
        set => SetValue(PointsResultProperty, value);
    }

    public string TextResult
    {
        get => (string)GetValue(TextResultProperty);
        set => SetValue(TextResultProperty, value);
    }

    public static readonly DependencyProperty DiceResultProperty =
        DependencyProperty.Register(nameof(DiceResult), typeof(string), typeof(ResultControl), new(default(string), (d, e) =>
        {
            ((d as UserControl)!.Content as Grid)!.Children.OfType<TextBlock>().FirstOrDefault(t => t.Name == "StringDiceResult")!.Text = e.NewValue.ToString();
        }));

    public static readonly DependencyProperty PointsResultProperty =
        DependencyProperty.Register(nameof(PointsResult), typeof(string), typeof(ResultControl), new(default(string), (d, e) =>
        {
            ((d as UserControl)!.Content as Grid)!.Children.OfType<TextBlock>().FirstOrDefault(t => t.Name == "StringPointsResult")!.Text = e.NewValue.ToString();
        }));

    public static readonly DependencyProperty TextResultProperty =
        DependencyProperty.Register(nameof(TextResult), typeof(string), typeof(ResultControl), new(default(string), (d, e) =>
        {
            ((d as UserControl)!.Content as Grid)!.Children.OfType<TextBlock>().FirstOrDefault(t => t.Name == "StringTextResult")!.Text = e.NewValue.ToString();
        }));

    public ResultControl()
    {
        InitializeComponent();
    }
}
