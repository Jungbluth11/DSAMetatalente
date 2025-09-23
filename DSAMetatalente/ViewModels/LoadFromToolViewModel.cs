namespace DSAMetatalente.ViewModels;

public partial class LoadFromToolViewModel : ObservableObject
{
    [ObservableProperty] private Charakter? _selectedCharacter;
    public Charakter[] Characters { get; } = Core.GetInstance().GetCharactersFromTool();

    [RelayCommand]
    private void LoadCharacter()
    {
        if (SelectedCharacter != null)
        {
            WeakReferenceMessenger.Default.Send(SelectedCharacter);
        }
    }
}