using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Winner;

namespace UNO_Spielprojekt.Winner;

public partial class WinnerView : UserControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(WinnerViewModel), typeof(WinnerView), new PropertyMetadata(default(WinnerViewModel)));

    public WinnerViewModel ViewModel
    {
        get => (WinnerViewModel)GetValue(ViewModelProperty); 
        set => SetValue(ViewModelProperty, value); 
    }

    public static readonly DependencyProperty GameViewModelProperty = DependencyProperty.Register(
        nameof(GameViewModel), typeof(GameViewModel), typeof(WinnerView), new PropertyMetadata(default(GameViewModel)));

    public GameViewModel GameViewModel
    {
        get => (GameViewModel)GetValue(GameViewModelProperty);
        set => SetValue(GameViewModelProperty, value); 
    }

    public WinnerView()
    {
        InitializeComponent();
    }
}