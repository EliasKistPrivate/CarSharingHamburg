using CarSharingHamburg.ViewModels;

namespace CarSharingHamburg.Views;

public partial class NewAutoPage : ContentPage
{
    private NewAutoViewModel _viewModel;
    private Entry[] _txtFelder;
    public NewAutoPage(NewAutoViewModel viewModel)
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
    private void PickerFahrzeugtyp_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        _viewModel.Auto.Fahrzeugztyp = e.PropertyName;
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