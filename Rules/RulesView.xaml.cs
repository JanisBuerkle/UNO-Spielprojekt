using System.Windows;
using System.Windows.Controls;

namespace UNO_Spielprojekt;

public partial class RulesView : UserControl
{
    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.Register(
        nameof(ViewModel), typeof(RulesViewModel), typeof(RulesView), new PropertyMetadata(default(RulesViewModel)));

    public RulesViewModel ViewModel
    {
        get => (RulesViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    public RulesView()
    {
        InitializeComponent();
    }
}