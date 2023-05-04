using CarSharingHamburg.ViewModels;

namespace CarSharingHamburg.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]

public partial class NewKundePage : ContentPage
{
    private NewKundeViewModel _viewModel;
    private readonly Entry[] _txtFelder;
    public NewKundePage(NewKundeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;

        _txtFelder = new Entry[]
       {
            TxtEMail,
            TxtFirstName,
            TxtLastName,
            TxtOrt,
            TxtPLZ,
            TxtStrasse
       };
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _viewModel.Initialize();
        base.OnNavigatedTo(args);

    }

    private void ValidateInput()
    {
        foreach (var item in _txtFelder)
        {
            if (string.IsNullOrEmpty(item.Text))
            {
                BttnOk.IsEnabled = false;
                return;
            }
        }
        BttnOk.IsEnabled = true;
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidateInput();
    }
}