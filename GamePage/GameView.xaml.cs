using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
    
    public GameView()
    {
        InitializeComponent();
    }

    private void HomeButtonClicked(object sender, RoutedEventArgs e)
    {
        
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
    public void Test()
    {
            
    }
}
