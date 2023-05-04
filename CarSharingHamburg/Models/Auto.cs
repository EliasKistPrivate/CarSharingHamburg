using SQLite;

namespace CarSharingHamburg.Models
{
    [Table("Autos")]
    public class Auto
    {
        [PrimaryKey]
        public string Id { get; set; }
        [MaxLength(50)]
        public string Kennzeichen { get; set; }
        [MaxLength(50)]
        public string Modell { get; set; }
        [MaxLength(50)]
        public string Fahrzeugztyp { get; set; }
        [MaxLength(50)]
        public string Strasse { get; set; }
        [MaxLength(5)]
        public string PLZ { get; set; }
        [MaxLength(80)]
        public string Ort { get; set; }

        public string GetAddress()
        {
            return $"{Strasse}, {PLZ} {Ort}";
        }

        public double GetPricePerHour()
        {
            if(Fahrzeugztyp == "Kleinwagen")
            {
                return 1;
            }
            if (Fahrzeugztyp == "Limousine")
            {
                return 1.5;
            }
            if (Fahrzeugztyp == "Kombi")
            {
                return 2;
            }
            return 0;
        }

        public double GetPricePerKm()
        {
            if (Fahrzeugztyp == "Kleinwagen")
            {
                return 0.1;
            }
            if (Fahrzeugztyp == "Limousine")
            {
                return 0.2;
            }
            if (Fahrzeugztyp == "Kombi")
            {
                return 0.3;
            }
            return 0;
        }
    }
}
