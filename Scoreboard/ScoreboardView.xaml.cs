using System.Windows;

namespace UNO_Spielprojekt.Scoreboard;

public partial class ScoreboardView
{
    public static readonly DependencyProperty GameDataProperty = DependencyProperty.Register(
        nameof(GameData), typeof(GameData), typeof(ScoreboardView), new PropertyMetadata(default(GameData)));

    public GameData GameData
    {
        get => (GameData)GetValue(GameDataProperty);
        set => SetValue(GameDataProperty, value);
    }

    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(ScoreboardViewModel), typeof(ScoreboardView),
        new PropertyMetadata(default(ScoreboardViewModel)));

    public ScoreboardViewModel ViewModel
    {
        get => (ScoreboardViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public ScoreboardView()
    {
        InitializeComponent();
    }
}