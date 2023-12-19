using System;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.GamePage;

public class ChooseColorViewModel : ViewModelBase
{
    public RelayCommand<string> ChooseCommand { get; }
    public int choosenColor { get; set; }

    public ChooseColorViewModel()
    {
        ChooseCommand = new RelayCommand<string>(Test);
    }

    private void Test(string color)
    {
        choosenColor = Convert.ToInt32(color);
    }
}