namespace DSAMetatalente.Controls;

public sealed partial class ResultControl : UserControl
{
    public string DiceResult
    {
        get { return (string) GetValue(DiceResultProperty); }
        set { SetValue(DiceResultProperty, value); }
    }

    public string PointsResult
    {
        get { return (string) GetValue(PointsResultProperty); }
        set { SetValue(PointsResultProperty, value); }
    }

    public string TextResult
    {
        get { return (string) GetValue(TextResultProperty); }
        set { SetValue(TextResultProperty, value); }
    }

    public static readonly DependencyProperty DiceResultProperty =
        DependencyProperty.Register(nameof(DiceResult), typeof(string), typeof(ResultControl),
            new(default(string), OnDiceResultChanged));

    public static readonly DependencyProperty PointsResultProperty =
        DependencyProperty.Register(nameof(PointsResult), typeof(string), typeof(ResultControl),
            new(default(string), OnPointsResultChanged));

    public static readonly DependencyProperty TextResultProperty =
        DependencyProperty.Register(nameof(TextResult), typeof(string), typeof(ResultControl),
            new(default(string), OnTextResultChanged));

    public ResultControl()
    {
        InitializeComponent();
    }

    private static void OnDiceResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue != null)
        {
            (((d as UserControl)!.Content as GroupBox)!.Content as Grid)!.Children.OfType<TextBlock>()
                .FirstOrDefault(t => t.Name == "StringDiceResult")!.Text = e.NewValue.ToString();
        }
    }

    private static void OnPointsResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue != null)
        {
            (((d as UserControl)!.Content as GroupBox)!.Content as Grid)!.Children.OfType<TextBlock>()
                .FirstOrDefault(t => t.Name == "StringPointsResult")!.Text = e.NewValue.ToString();
        }
    }

    private static void OnTextResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue != null)
        {
            (((d as UserControl)!.Content as GroupBox)!.Content as Grid)!.Children.OfType<TextBlock>()
                .FirstOrDefault(t => t.Name == "StringTextResult")!.Text = e.NewValue.ToString();
        }
    }
}