using System;
using System.Windows;
using CommunityToolkit.Mvvm.Input;

namespace UNO_Spielprojekt.GamePage;

public partial class ChooseColorView : System.Windows.Window
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(ChooseColorViewModel), typeof(ChooseColorView), new PropertyMetadata(default(ChooseColorViewModel)));
    public ChooseColorViewModel ViewModel
    {
        get => (ChooseColorViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }
    public ChooseColorView()
    {
        ViewModel = new ChooseColorViewModel();
        InitializeComponent();
    }
    private void CloseWindow(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
