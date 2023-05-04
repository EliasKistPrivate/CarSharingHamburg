using CarSharingHamburg.ViewModels;

namespace CarSharingHamburg.Views;

public partial class AutoPage : ContentPage
{
    private AutoViewModel _viewModel;
    public AutoPage(AutoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.Initialize();
    }
}