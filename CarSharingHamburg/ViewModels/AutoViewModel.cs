using CarSharingHamburg.Models;
using CarSharingHamburg.Services;
using CarSharingHamburg.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace CarSharingHamburg.ViewModels
{
    public class AutoViewModel : BaseViewModel<Auto>
    {
        public ObservableCollection<Auto> Autos { get; set; }
        private Auto _selectedAuto;
        public ICommand LoadAutosCommand { get; }
        public ICommand AddAutoCommand { get; }
        public AutoViewModel(IDataStore<Auto> dataStore) : base(dataStore)
        {
            IsBusy = true;

            Title = "Autoübersicht";
            Autos = new ObservableCollection<Auto>();

            LoadAutosCommand = new Command(async () => await ExecuteLoadAutosCommand());
            AddAutoCommand= new Command(async () => await ExecuteAddAutoCommand());


            IsBusy = false;
        }
        async Task ExecuteLoadAutosCommand()
        {
            //IsBusy = true;

            Autos.Clear();
            try
            {

                var autos = await DataStore.GetItemsAsync(true);

                
                foreach (var auto  in autos)
                {
                    Autos.Add(auto);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteAddAutoCommand()
        {
            try
            {
                await Shell.Current.GoToAsync(nameof(NewAutoPage));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async void Initialize()
        {
            await ExecuteLoadAutosCommand();

        }
        public Auto SelectedAuto
        {
            get => _selectedAuto;
            set
            {
                SetProperty(ref _selectedAuto, value);
                OnAutoSelected(value);
            }
        }

        async void OnAutoSelected(Auto auto)
        {
            if (auto == null)
                return;

            var navigationParameter = new Dictionary<string, object>
            {
                [nameof(Auto)] = auto
            };

            await Shell.Current.GoToAsync(nameof(AutoDetailsPage), navigationParameter);

        }
    }
}
