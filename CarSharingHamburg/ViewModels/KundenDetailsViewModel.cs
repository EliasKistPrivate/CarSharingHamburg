using CarSharingHamburg.Models;
using CarSharingHamburg.Services;
using CarSharingHamburg.Views;
using System.Diagnostics;
using System.Windows.Input;

namespace CarSharingHamburg.ViewModels
{
    [QueryProperty(nameof(Kunde), nameof(Kunde))]
    public class KundenDetailsViewModel : BaseViewModel<Kunde>
    {
        private Kunde _kunde;
        public ICommand AddKundeCommand { get; }
        public ICommand EditKundeCommand { get; }
        public ICommand DeleteKundeCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SubmitCommand { get; }

        public KundenDetailsViewModel(IDataStore<Kunde> dataStore) : base(dataStore)
        {
            IsBusy = true;
            _kunde = new Kunde();

            SubmitCommand = new Command(async () => await ExecuteSubmitCommand());
            EditKundeCommand = new Command(async () => await ExecuteEditKundeCommand());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
            DeleteKundeCommand = new Command(async () => await ExecuteDeleteKundeCommand());

            IsBusy = false;
        }

        async Task ExecuteSubmitCommand()
        {
            if (Kunde == null)
            {
                return;
            }
            var kunde = await DataStore.GetItemAsync(Kunde.Id);

            var navigationParameter = new Dictionary<string, object>
            {
                [nameof(Kunde)] = Kunde
            };

            if (kunde == null)
            {
                //await DataStore.AddItemAsync(Kunde);
                await Shell.Current.GoToAsync("MainPage", navigationParameter);
            }
            else
            {
                //await DataStore.UpdateItemAsync(Kunde);
                await Shell.Current.GoToAsync("../..", navigationParameter);
            }


        }

        async Task ExecuteEditKundeCommand()
        {
            IsBusy = true;

            try
            {
                await DataStore.UpdateItemAsync(Kunde);
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
        async Task ExecuteDeleteKundeCommand()
        {
            IsBusy = true;

            try
            {
                await DataStore.DeleteItemAsync(Kunde.Id);
                await Shell.Current.GoToAsync("..");
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
        public Kunde Kunde
        {
            get => _kunde;
            set
            {
                SetProperty(ref _kunde, value);
                OnPropertyChanged();
            }
        }
    }
}
