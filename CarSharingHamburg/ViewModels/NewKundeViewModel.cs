using CarSharingHamburg.Models;
using CarSharingHamburg.Services;
using System.Windows.Input;

namespace CarSharingHamburg.ViewModels
{
    public class NewKundeViewModel : BaseViewModel<Kunde>
    {
        public ICommand AddCommand { get; }

        private Kunde _kunde;

        public NewKundeViewModel(IDataStore<Kunde> dataStore) : base(dataStore)
        {
            IsBusy = true;

            AddCommand = new Command(async () => ExecuteAddCommand());

            Kunde = new Kunde();
            Kunde.Id = Guid.NewGuid().ToString();

            IsBusy = false;
        }

        async Task ExecuteAddCommand()
        {
            await DataStore.AddItemAsync(Kunde);

            var navigationParameter = new Dictionary<string, object>
            {
                [nameof(Kunde)] = Kunde
            };


            //await Shell.Current.GoToAsync("../..",true,navigationParameter);
            await Shell.Current.GoToAsync("..", true);
        }

        void ExecuteValidateInputCommand()
        {

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

      
        public void Initialize()
        {

            IsBusy = true;

            IsBusy = false;
        }

    }
}
