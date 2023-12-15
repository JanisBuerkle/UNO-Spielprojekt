using System.Windows;

namespace UNO_Spielprojekt.GamePage;

public partial class ChooseColorView : System.Windows.Window
{
    public ChooseColorView()
    {
        InitializeComponent();
    }
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(ChooseColorViewModel), typeof(ChooseColorView), new PropertyMetadata(default(ChooseColorViewModel)));
    public ChooseColorViewModel ViewModel
    {
        get => (ChooseColorViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
}