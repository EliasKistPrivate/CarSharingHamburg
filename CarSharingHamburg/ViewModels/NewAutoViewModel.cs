using CarSharingHamburg.Models;
using CarSharingHamburg.Services;
using System.Windows.Input;

namespace CarSharingHamburg.ViewModels
{
    public class NewAutoViewModel : BaseViewModel<Auto>
    {
        public ICommand AddCommand { get; }
        private Auto _auto;
        private string _selectedFarzeugtyp;

        public NewAutoViewModel(IDataStore<Auto> dataStore) : base(dataStore)
        {
            IsBusy = true;

            AddCommand = new Command(async () => ExecuteAddCommand());

            Auto = new Auto();
            Auto.Id = Guid.NewGuid().ToString();

            

            IsBusy = false;
        }

        async Task ExecuteAddCommand()
        {
            await DataStore.AddItemAsync(Auto);

            var navigationParameter = new Dictionary<string, object>
            {
                [nameof(Auto)] = Auto
            };

            await Shell.Current.GoToAsync("../..", true, navigationParameter);
        }
        public Auto Auto
        {
            get => _auto;
            set
            {
                SetProperty(ref _auto, value);
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
