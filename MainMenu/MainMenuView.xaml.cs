using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.Localization;

namespace UNO_Spielprojekt.MainMenu;

public partial class MainMenuView : UserControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(MainMenuViewModel), typeof(MainMenuView),
        new PropertyMetadata(default(MainMenuViewModel)));

    public MainMenuViewModel ViewModel
    {
        get => (MainMenuViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public MainMenuView()
    {
        InitializeComponent();
        SetLanguage(Thread.CurrentThread.CurrentUICulture);
    }

    private void SetLanguage(CultureInfo culture)
    {
        LocalizationManager.SetLanguage(culture);
    }
}