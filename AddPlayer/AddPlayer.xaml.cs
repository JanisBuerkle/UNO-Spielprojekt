using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace UNO_Spielprojekt.AddPlayer
{
    public partial class AddPlayer
    {
        private PlayerData _playerData;

        public AddPlayer()
        {
            _playerData = new PlayerData();
            InitializeComponent();
        }

        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainMenu.MainMenu());
        }

        private void PlusClicked(object sender, RoutedEventArgs e)
        {
            if (TextFieldStackPanel.Children.Count < 5)
            {
                TextBox newTextBox = new TextBox
                {
                    Margin = new Thickness(0, 10, 0, 0),
                    FontSize = 40,
                    Background = Brushes.DarkGray
                };
                TextFieldStackPanel.Children.Add(newTextBox);
            }

            if (TextFieldStackPanel.Children.Count >= 2)
            {
                WeiterButton.Visibility = Visibility.Visible;
            }
        }

        private void MinusClicked(object sender, RoutedEventArgs e)
        {
            if (TextFieldStackPanel.Children.Count > 0)
            {
                UIElement lastElement = TextFieldStackPanel.Children[TextFieldStackPanel.Children.Count - 1];
                TextFieldStackPanel.Children.Remove(lastElement);
            }

            if (TextFieldStackPanel.Children.Count <= 1)
            {
                WeiterButton.Visibility = Visibility.Hidden;
            }
        }

        private void WeiterButtonClicked(object sender, RoutedEventArgs e)
        {
            _playerData = new PlayerData();
            for (int i = 0; i < TextFieldStackPanel.Children.Count; i++)
            {
                if (TextFieldStackPanel.Children[i] is TextBox textBox)
                {
                    _playerData.PlayerName.Add(textBox.Text);
                }
            }

            NavigationService?.Navigate(new Rules(_playerData));
        }
    }
}