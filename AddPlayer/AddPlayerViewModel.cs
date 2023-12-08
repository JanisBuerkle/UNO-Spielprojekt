using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CommunityToolkit.Mvvm.Input;
using UNO_Spielprojekt.GamePage;
using UNO_Spielprojekt.Window;

namespace UNO_Spielprojekt.AddPlayer;

public class AddPlayerViewModel : INotifyPropertyChanged
{
    private readonly MainViewModel _mainViewModel;
    public RelayCommand GoToMainMenuCommand { get; }
    public event PropertyChangedEventHandler? PropertyChanged;
    public RelayCommand WeiterButtonCommand { get; }

    public AddPlayerViewModel(MainViewModel mainViewModel)
    {
        _mainViewModel = mainViewModel;
        PlayerNames = new ObservableCollection<NewPlayerViewModel>();
        GoToMainMenuCommand = new RelayCommand(_mainViewModel.GoToMainMenu);
        WeiterButtonCommand = new RelayCommand(WeiterButtonCommandMethod);
    }

    private void WeiterButtonCommandMethod()
    {
        foreach (var t in PlayerNames) GameLogic.prop.Players.Add(new Propertys { PlayerName = t.Name });
        
        _mainViewModel.GoToRules();
    }

    public ObservableCollection<NewPlayerViewModel> PlayerNames { get; }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}