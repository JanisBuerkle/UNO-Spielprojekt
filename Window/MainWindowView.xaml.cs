namespace UNO_Spielprojekt.Window;

public partial class MainWindowView : System.Windows.Window
{
    public static MainWindowView Instance { get; private set; }

    public MainWindowView()
    {
        InitializeComponent();
        Instance = this;
    }
}