using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace UNO_Spielprojekt.GamePage;

public partial class GameView
{
    private GameViewModel _viewModel;

    public GameView()
    {
        InitializeComponent();
        _viewModel = new GameViewModel();
        DataContext = _viewModel;
    }

    private void LegenButton(object sender, RoutedEventArgs e)
    {
        _viewModel.LegenButtonMethod();
    }

    private void HomeButtonClicked(object sender, RoutedEventArgs e)
    {
        NavigationService?.Navigate(new MainMenu.MainMenuView());
    }

    public void UpdateMiddleCard(string content, string color, string value)
    {
        MiddleCard.Content = content;

        if (color == "wild")
        {
            ImageBrush imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/wild/{color}.png")),
                Stretch = Stretch.Fill
            };

            MiddleCard.Background = imageBrush;
        }
        else
        {
            ImageBrush imageBrush = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Assets/cards/{value}/{color}.png")),
                Stretch = Stretch.Fill
            };

            MiddleCard.Background = imageBrush;
        }
    }
}
