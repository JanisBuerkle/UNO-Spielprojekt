using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UNO_Spielprojekt.GamePage;

namespace UNO_Spielprojekt.AddPlayer
{
    public partial class AddPlayer
    {
        private PlayerData _playerData;

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
            nameof(ViewModel), typeof(AddPlayerViewModel), typeof(AddPlayer),
            new PropertyMetadata(default(AddPlayerViewModel)));

        public AddPlayerViewModel ViewModel
        {
            get => (AddPlayerViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        public AddPlayer()
        {
            _playerData = new PlayerData();
            InitializeComponent();
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
            if (ViewModel.PlayerNames.Count < 5)
            {
                ViewModel.PlayerNames.Add(new NewPlayerViewModel());
            }
            UpdateWeiterButtonVisibility();
        }

        private void MinusClicked(object sender, RoutedEventArgs e)
        {
            if (ViewModel.PlayerNames.Count > 0)
            {
                ViewModel.PlayerNames.RemoveAt(ViewModel.PlayerNames.Count - 1);
            }

            UpdateWeiterButtonVisibility();
        }


        private void UpdateWeiterButtonVisibility()
        {
            if (ViewModel.PlayerNames.Count == 0)
            {
                WeiterButton.Visibility = Visibility.Hidden;
            }
            else
            {
                for (int i = 0; i < ViewModel.PlayerNames.Count; i++)
                {
                    bool allFieldsFilled = !string.IsNullOrWhiteSpace(ViewModel.PlayerNames[i].Name);
                    WeiterButton.Visibility = allFieldsFilled ? Visibility.Visible : Visibility.Hidden;
                }
            }
        }
    }
}