using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using UNO_Spielprojekt.Resources;

namespace UNO_Spielprojekt.Setting;

public class Language : INotifyPropertyChanged
{
    private static ResourceManager _resourceManager;

    static Language()
    {
        _resourceManager = new ResourceManager("UNO_Spielprojekt.Resources.Resource", typeof(Resource).Assembly);
    }

    public string? Flag { get; set; }
    public string? LangName { get; set; }
    public string? CultureName { get; set; }
    public string? LangString { get; set; }
    public CultureInfo LangCulture { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;

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