using CarSharingHamburg.Models;
using CarSharingHamburg.Services;
using CarSharingHamburg.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace CarSharingHamburg.ViewModels
{
    public class KundenViewModel : BaseViewModel<Kunde>
    {
        public ObservableCollection<Kunde> Kunden { get; set; }
        private Kunde _selecetedKunde;
        public ICommand LoadKundenCommand { get; }
        public ICommand AddKundeCommand { get; }


        public KundenViewModel(IDataStore<Kunde> dataStore) : base(dataStore)
        {
            IsBusy = true;
            Title = "Kundenübersicht";
            Kunden = new ObservableCollection<Kunde>();

            LoadKundenCommand = new Command(async () => await ExecuteLoadKundenCommand());
            AddKundeCommand = new Command(async()=> await ExecuteAddKundeCommand());

            IsBusy = false;
        }

        async Task ExecuteLoadKundenCommand()
        {
           // IsBusy = true;

            Kunden.Clear();
            try
            {

                var kunden = await DataStore.GetItemsAsync(true);

                foreach (var kunde in kunden)
                {
                    Kunden.Add(kunde);
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

        async Task ExecuteAddKundeCommand()
        {
            try
            {
                await Shell.Current.GoToAsync("NewKundePage");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async void Initialize()
        {
          await  ExecuteLoadKundenCommand();

        }
        public Kunde SelectedKunde
        {
            get => _selecetedKunde;
            set
            {
                SetProperty(ref _selecetedKunde, value);
                OnKundeSelected(value);
            }
        }

        async void OnKundeSelected(Kunde kunde)
        {
            if (kunde == null)
                return;

            var navigationParameter = new Dictionary<string, object>
            {
                [nameof(Kunde)] = kunde
            };

            await Shell.Current.GoToAsync(nameof(KundenDetailsPage), navigationParameter);

        }
    }
}
