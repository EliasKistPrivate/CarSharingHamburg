using SQLite;

namespace CarSharingHamburg.Models
{
    [Table("Kunden")]
    public class Kunde
    {
        [PrimaryKey]
        public string Id { get; set; }
        [MaxLength(50)]
        public string Nachname { get; set; }
        [MaxLength(50)]
        public string Vorname { get; set; }
        [MaxLength (50)]
        public string EMail{ get; set;}
        [MaxLength(50)]
        public string Strasse { get; set; }
        [MaxLength(5)] 
        public string PLZ { get; set;}
        [MaxLength(50)]
        public string Ort { get; set;}
    }
}
