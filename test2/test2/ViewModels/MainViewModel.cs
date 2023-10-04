using Avalonia.Media;

namespace test2.ViewModels;

public class MainViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public Color Color
    {
        get;
        set;
    }

    public MainViewModel(Color color)
    {
        Color = color;
    }
}