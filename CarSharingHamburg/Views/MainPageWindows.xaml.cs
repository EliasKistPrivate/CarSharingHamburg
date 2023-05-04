using CarSharingHamburg.ViewModels;
using CommunityToolkit.Maui.Views;

namespace CarSharingHamburg.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class MainPageWindows : ContentPage
{
    private MainPageWindowsViewModel _viewModel;



    public MainPageWindows()
    {
        InitializeComponent();
        BindingContext = _viewModel = new MainPageWindowsViewModel();

    }
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await _viewModel.Initialize();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.Initialize();
        _viewModel.ValidInput = false;
    }


    private async void BttnShowLocation_Clicked(object sender, EventArgs e)
    {
     


    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var popup = new CustomPopUp();
        popup.Size = new Microsoft.Maui.Graphics.Size(350,200);
        this.ShowPopup(popup);
    }
}

