using CarSharingHamburg.Models;
using CarSharingHamburg.Services;
using System.Diagnostics;
using System.Windows.Input;

namespace CarSharingHamburg.ViewModels
{
    [QueryProperty(nameof(Auto), nameof(Auto))]
    public class AutoDetailsViewModel : BaseViewModel<Auto>
    {
        private Auto _auto;
        private string _selectedFarzeugtyp;

        public ICommand AddAutoCommand { get; }
        public ICommand EditAutoCommand { get; }
        public ICommand DeleteAutoCommand { get; }
        public ICommand CancelCommand { get; }
        public ICommand SubmitCommand { get; }


        public AutoDetailsViewModel(IDataStore<Auto> dataStore) : base(dataStore)
        {
            IsBusy = true;
            _auto = new Auto();

         

            SubmitCommand = new Command(async () => await ExecuteSubmitCommand());
            EditAutoCommand = new Command(async () => await ExecuteEditAutoCommand());
            CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
            DeleteAutoCommand = new Command(async () => await ExecuteDeleteAutoCommand());

            IsBusy = false;
        }

        public Auto Auto
        {
            get => _auto;
            set
            {
                SetProperty(ref _auto, value);
                SelectedFahrzeugtyp = Auto.Fahrzeugztyp;
            }
        }

        async Task ExecuteSubmitCommand()
        {
            if (Auto == null)
            {
                return;
            }
            var auto = await DataStore.GetItemAsync(Auto.Id);

            var navigationParameter = new Dictionary<string, object>
            {
                [nameof(Auto)] = Auto
            };

            if (Auto == null)
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

        async Task ExecuteEditAutoCommand()
        {
            IsBusy = true;

            try
            {
                await DataStore.UpdateItemAsync(Auto);
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
        async Task ExecuteDeleteAutoCommand()
        {
            IsBusy = true;

            try
            {
                await DataStore.DeleteItemAsync(Auto.Id);
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
        public string SelectedFahrzeugtyp
        {
            get => _selectedFarzeugtyp;
            set
            {
                SetProperty(ref _selectedFarzeugtyp, value);
                Auto.Fahrzeugztyp = value;
            }
        }
    }
}
