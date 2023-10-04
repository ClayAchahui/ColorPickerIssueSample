using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace test2.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        AvaloniaXamlLoader.Load(this);
    }
}