using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

    public static readonly DependencyProperty PlayerProperty = DependencyProperty.Register(
        nameof(Player), typeof(Players), typeof(GameView), new PropertyMetadata(default(Players)));

    public Players Player
    {
        get => (Players)GetValue(PlayerProperty);
        set => SetValue(PlayerProperty, value);
    }

    private void CardButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is CardViewModel card)
        {
            int selectedIndex = ViewModel.CurrentHand.IndexOf(card);
            ViewModel.SelectedCardIndex = selectedIndex;
            ViewModel.LegenCommandMethod();
        }
    }

    private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        var scrollViewer = (ScrollViewer)sender;
        double scrollFactor = 1.0;
        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta * scrollFactor);
        e.Handled = true;
    }

    public GameView()
    {
        InitializeComponent();
    }
}