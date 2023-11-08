namespace UNO_Spielprojekt.Window
{
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new MainMenu.MainMenu());
        }
    }
}