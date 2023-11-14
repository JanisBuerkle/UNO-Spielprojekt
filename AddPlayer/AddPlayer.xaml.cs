using System.Linq;
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
            
            foreach (var textBox in TextFieldStackPanel.Children.OfType<TextBox>())
            {
                textBox.TextChanged += TextBox_TextChanged;
            }
        }

        private void HomeButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MainMenu.MainMenu());
        }

        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateWeiterButtonVisibility();
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
                newTextBox.TextChanged += TextBox_TextChanged;
                TextFieldStackPanel.Children.Add(newTextBox);
            }

            UpdateWeiterButtonVisibility();
        }

        private void MinusClicked(object sender, RoutedEventArgs e)
        {
            if (TextFieldStackPanel.Children.Count > 0)
            {
                UIElement lastElement = TextFieldStackPanel.Children[TextFieldStackPanel.Children.Count - 1];
                if (lastElement is TextBox lastTextBox)
                {
                    lastTextBox.TextChanged -= TextBox_TextChanged;
                }
                TextFieldStackPanel.Children.Remove(lastElement);
            }

            UpdateWeiterButtonVisibility();
        }

        private void UpdateWeiterButtonVisibility()
        {
            if (TextFieldStackPanel.Children.Count == 0)
            {
                WeiterButton.Visibility = Visibility.Hidden;
            }
            else
            {
                bool allFieldsFilled = TextFieldStackPanel.Children.OfType<TextBox>().All(textBox => !string.IsNullOrWhiteSpace(textBox.Text));
                WeiterButton.Visibility = allFieldsFilled ? Visibility.Visible : Visibility.Hidden;
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