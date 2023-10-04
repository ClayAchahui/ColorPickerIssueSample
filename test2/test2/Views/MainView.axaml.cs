using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace test2.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        AvaloniaXamlLoader.Load(this);
    }
}