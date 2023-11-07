using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace UNO_Spielprojekt;

public class MainVM : ObservableObject
{
    private PageId _pageID;

    public PageId PageID
    {
        get { return _pageID; }
        set { SetProperty(ref _pageID, value); }
    }

    public ICommand CMDChangePage => new RelayCommand<PageId>(ChangePage);

    void ChangePage(PageId newPage)
    {
        PageID = newPage;
    }
    public MainVM()
    {
        PageID = PageId.C; 
    }
}