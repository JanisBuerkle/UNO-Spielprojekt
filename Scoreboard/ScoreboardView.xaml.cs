using System.Windows;
using System.Windows.Controls;
using UNO_Spielprojekt.Scoreboard;

namespace UNO_Spielprojekt.Scoreboard;

public partial class ScoreboardView : UserControl
{
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