using CarSharingHamburg.Models;
using CarSharingHamburg.Views;
using System.Windows.Input;

namespace CarSharingHamburg.ViewModels
{
    [QueryProperty(nameof(Kunde), nameof(Kunde))]
    [QueryProperty(nameof(Auto), nameof(Auto))]
    public class MainPageViewModel : BaseViewModel
    {
        private Kunde _kunde;
        private Auto _auto;

        public ICommand NavigateToKunde { get; }
        public ICommand NavigateToAuto { get; }
        public ICommand CalculateRentCommand { get; }


        private DateTime _fromDate = DateTime.Now.Date;
        private DateTime _toDate = DateTime.Now.AddHours(1).Date;
        private TimeSpan _fromTime = DateTime.Now.TimeOfDay; /*= TimeSpan.FromTicks(DateTime.Now.Ticks);*/
        private TimeSpan _toTime = DateTime.Now.AddHours(1).TimeOfDay; /*TimeSpan.FromTicks(DateTime.Now.Ticks);*/

        private int _fromToTime;

        private double _miete;
        private double _kilometers;

        private bool _validAuto = false;
        private bool _validKunde = false;
        private bool _validTime = false;
        private bool _validInput = false;

        public MainPageViewModel()
        {
            IsBusy = true;
            Title = "Carsharing Hamburg";

            NavigateToKunde = new Command(() => { Shell.Current.GoToAsync(nameof(KundenPage)); });
            NavigateToAuto = new Command(() => { Shell.Current.GoToAsync(nameof(AutoPage)); });
            CalculateRentCommand = new Command(async () => await ExecuteCalculateRentCommand());
            ValidateInput();

            IsBusy = false;
        }
        public DateTime FromDate
        {
            get => _fromDate;
            set
            {
                SetProperty(ref _fromDate, value);
                CalculateTimeDiff();
                ValidateInput();
            }
        }
        public DateTime ToDate
        {
            get => _toDate;
            set
            {
                SetProperty(ref _toDate, value);
                CalculateTimeDiff();
                ValidateInput();
            }
        }
        public TimeSpan FromTime
        {
            get => _fromTime;
            set
            {
                SetProperty(ref _fromTime, value);
                CalculateTimeDiff();
                ValidateInput();
            }
        }
        public TimeSpan ToTime
        {
            get => _toTime;
            set
            {
                SetProperty(ref _toTime, value);
                CalculateTimeDiff();
                ValidateInput();
            }
        }





        public string KundenName
        {
            get
            {
                if (_kunde != null && !string.IsNullOrEmpty(_kunde.Vorname) && !string.IsNullOrEmpty(_kunde.Nachname))
                {
                    return _kunde.Vorname + " " + _kunde.Nachname;
                }
                OnPropertyChanged();
                return "";
            }

        }

        public Kunde Kunde
        {
            get => _kunde;
            set
            {
                SetProperty(ref _kunde, value);
                OnPropertyChanged();
                ValidateInput();
            }
        }

        public Auto Auto
        {
            get => _auto;
            set
            {
                SetProperty(ref _auto, value);
                ValidateInput();
            }
        }

        public int FromToTime
        {
            get => _fromToTime;
            set
            {
                SetProperty(ref _fromToTime, value);
            }
        }

        public double Miete
        {
            get => _miete;
            set
            {
                SetProperty(ref _miete, value);
                ValidateInput();
            }
        }
        public double Kilometers
        {
            get => _kilometers;
            set
            {
                SetProperty(ref _kilometers, value);
                ValidateInput();
            }
        }

        public bool ValidKunde
        {
            get => _validKunde;
            set
            {
                SetProperty(ref _validKunde, value);
            }
        }
        public bool ValidAuto
        {
            get => _validAuto;
            set
            {
                SetProperty(ref _validAuto, value);
            }
        }
        public bool ValidTime
        {
            get => _validTime;
            set
            {
                SetProperty(ref _validTime, value);
            }
        }
        public bool ValidInput
        {
            get => _validInput;
            set
            {
                SetProperty(ref _validInput, value);
            }
        }

        private void CalculateTimeDiff()
        {

            var dif = (ToDate.Add(ToTime) - FromDate.Add(FromTime)) + TimeSpan.FromMinutes(59);
            FromToTime = dif.Days * 24 + dif.Hours;
        }

        async Task ExecuteCalculateRentCommand()
        {

            Miete = FromToTime * Auto.GetPricePerHour() + Kilometers * Auto.GetPricePerKm();
        }

        private void ValidateInput()
        {
            ValidInput = true;
            ValidKunde = true;
            ValidAuto = true;
            ValidTime = true;

            if (Kunde == null || string.IsNullOrEmpty(Kunde.Id))
            {
                ValidKunde = false;
                ValidInput = false;
            }
            if (Auto == null || string.IsNullOrEmpty(Auto.Strasse) || string.IsNullOrEmpty(Auto.PLZ) || string.IsNullOrEmpty(Auto.Ort))
            {
                ValidAuto = false;
                ValidInput = false;
            }
            if (ToDate.Add(ToTime) < FromDate.Add(FromTime) || FromToTime < 1)
            {
                ValidTime = false;
                ValidInput = false;
            }
            if (Kilometers < 1)
            {
                ValidInput = false;
            }

        }
        public async override Task Initialize()
        {
            ValidateInput();
            await base.Initialize();
        }

    }
}
