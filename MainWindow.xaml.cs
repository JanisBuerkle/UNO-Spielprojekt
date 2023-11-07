using System;
using System.Windows;
using System.Windows.Media.Imaging;
using UNO_Spielprojekt.MainMenu;

namespace UNO_Spielprojekt
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainMenu.MainMenu());
        }
    }
}