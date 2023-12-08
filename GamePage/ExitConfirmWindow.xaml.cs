using System.Windows;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.GamePage;

public partial class ExitConfirmWindow : System.Windows.Window
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(ExitConfirmViewModel), typeof(ExitConfirmWindow),
        new PropertyMetadata(default(ExitConfirmViewModel)));

    public ExitConfirmViewModel ViewModel
    {
        get => (ExitConfirmViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    private readonly MainViewModel MainViewModel;

    public ExitConfirmWindow(MainViewModel mainViewModel)
    {
        MainViewModel = mainViewModel;
        ViewModel = new ExitConfirmViewModel(MainViewModel);
        InitializeComponent();
    }

    private void CloseExitConfirmWindow(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void ConfirmButtonClicked(object sender, RoutedEventArgs e)
    {
        Close();
        ViewModel.ConfirmButtonCommandMethod();
    }
}