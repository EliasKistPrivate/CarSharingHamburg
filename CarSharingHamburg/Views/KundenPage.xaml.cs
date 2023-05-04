using CarSharingHamburg.ViewModels;

namespace CarSharingHamburg.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class KundenPage : ContentPage
{
    private KundenViewModel _viewModel;



    public KundenPage(KundenViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }


    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _viewModel.Initialize();
        base.OnNavigatedTo(args);
    }
}