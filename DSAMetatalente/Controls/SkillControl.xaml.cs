namespace DSAMetatalente.Controls;

public sealed partial class SkillControl : UserControl
{
    public string Text
    {
        get => (string) GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public int Value
    {
        get => (int) GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(nameof(Text), typeof(string), typeof(SkillControl),
            new(default(string), OnTextChanged));

    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(int), typeof(SkillControl), new(0, OnValueChanged));

    public event TypedEventHandler<NumberBox, NumberBoxValueChangedEventArgs>? ValueChanged;

    public SkillControl()
    {
        InitializeComponent();
    }

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((d as UserControl)!.Content as Grid)!.Children.OfType<TextBlock>().FirstOrDefault()!.Text =
            e.NewValue.ToString();
    }

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((d as UserControl)!.Content as Grid)!.Children.OfType<NumberBox>().FirstOrDefault()!.Value =
            (int) e.NewValue;
    }

    private void NumberBox_ValueChanged(NumberBox sender, NumberBoxValueChangedEventArgs args)
    {
        try
        {
            ValueChanged!.Invoke(sender, args);
        }
        catch
        {
        }
    }
}