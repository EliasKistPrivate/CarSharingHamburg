using CarSharingHamburg.ViewModels;

namespace CarSharingHamburg.Views;

public partial class AutoDetailsPage : ContentPage
{
    private AutoDetailsViewModel _viewModel;
    private Entry[] _txtFelder;
    public AutoDetailsPage(AutoDetailsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;

        _txtFelder = new Entry[]
        {
            TxtKennzeichen,
            TxtModell,
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
                BttnSubmit.IsEnabled = false;
                BttnSaveChanges.IsEnabled = false;
                return;
            }
        }
        BttnSaveChanges.IsEnabled = true;
        BttnSubmit.IsEnabled = true;
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        ValidateInput();
    }
    private void BttnChangeClicked(object sender, EventArgs e)
    {
        BttnSaveChanges.IsVisible = true;
        BttnEdit.IsVisible = false;
        BttnCancel.IsVisible = false;
        BttnDelete.IsVisible = false;
        BttnSubmit.IsVisible = false;
        foreach (var item in _txtFelder)
        {
            item.IsEnabled = true;
        }
        PickerFahrzeugtyp.IsEnabled = true;
    }

    private void BttnSaveClicked(object sender, EventArgs e)
    {
        BttnSaveChanges.IsVisible = false;
        BttnEdit.IsVisible = true;
        BttnCancel.IsVisible = true;
        BttnDelete.IsVisible = true;
        BttnSubmit.IsVisible = true;

        foreach (var item in _txtFelder)
        {
            item.IsEnabled = false;
        }
        PickerFahrzeugtyp.IsEnabled = false;
    }

}