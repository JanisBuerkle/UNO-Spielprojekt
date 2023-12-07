using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.GamePage;

public partial class GameView
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(GameViewModel), typeof(GameView), new PropertyMetadata(default(GameViewModel)));

    public GameViewModel ViewModel
    {
        get => (GameViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public static readonly DependencyProperty PropertysProperty = DependencyProperty.Register(
        nameof(Propertys), typeof(Propertys), typeof(GameView), new PropertyMetadata(default(Propertys)));

    public Propertys Propertys
    {
        get { return (Propertys)GetValue(PropertysProperty); }
        set { SetValue(PropertysProperty, value); }
    }

    public GameView()
    {
        ViewModel = new GameViewModel(new MainViewModel());
        InitializeComponent();
    }
}