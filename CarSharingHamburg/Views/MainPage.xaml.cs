using CarSharingHamburg.ViewModels;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;

namespace CarSharingHamburg.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class MainPage : ContentPage
{
    private MainPageViewModel _viewModel;



    public MainPage()
    {
        InitializeComponent();
        BindingContext = _viewModel = new MainPageViewModel();

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
        var auto = _viewModel.Auto;
        var address = auto.GetAddress();
        IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address);

        Location location = locations?.FirstOrDefault();

        if (location == null)
        {
            return;
        }
        var pin = new Pin
        {
            Label = auto.Kennzeichen,
            Address = address,
            Type = PinType.Place,
            Location = location
        };
        map.Pins.Clear();
        map.Pins.Add(pin);

        MapSpan mapSpan = MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(0.1));
        map.MoveToRegion(mapSpan);


    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var popup = new CustomPopUp();
        popup.Size = new Microsoft.Maui.Graphics.Size(350,200);
        this.ShowPopup(popup);
    }
}

