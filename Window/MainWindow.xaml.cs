namespace UNO_Spielprojekt.Window
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainMenu.MainMenu());
        }
    }
}