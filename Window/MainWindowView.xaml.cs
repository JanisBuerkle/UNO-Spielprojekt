namespace UNO_Spielprojekt.Window
{
    public partial class MainWindowView
    {
        public MainWindowView()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainMenu.MainMenuView());
        }
    }
}