using CarSharingHamburg.Models;
using SQLite;

namespace CarSharingHamburg.Services
{
    public class DbKundenStore : IDataStore<Kunde>
    {
        private const SQLiteOpenFlags SqliteFlags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        private static readonly string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "carsharing.db3");

        private static SQLiteAsyncConnection _database;

        public DbKundenStore()
        {
            _database = new SQLiteAsyncConnection(DatabasePath, SqliteFlags);
            _database.CreateTableAsync<Kunde>().Wait();
        }



        public async Task<bool> AddItemAsync(Kunde auto)
        {
            if (string.IsNullOrEmpty(auto.Id))
            {
                auto.Id = Guid.NewGuid().ToString();
            }

            return await _database.InsertAsync(auto) == 1;
        }

        public async Task<bool> UpdateItemAsync(Kunde auto)
        {
            return await _database.UpdateAsync(auto) == 1;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            return await _database.DeleteAsync<Kunde>(id) == 1;
        }

        public async Task<Kunde> GetItemAsync(string id)
        {
            return await _database.Table<Kunde>()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Kunde>> GetItemsAsync(bool forceRefresh = false)
        {
            var kunden = await _database.Table<Kunde>().ToListAsync();
            if (kunden.Count == 0)
            {
                await SeedData();
                return await GetItemsAsync();
            }

            return kunden;
        }

        private async Task SeedData()
        {
            List<Kunde> kunden = new List<Kunde>()
            {
                new Kunde()
                {
                    Nachname = "Heinemann",
                    Vorname = "Bernd",
                    EMail = "bHeinemann@bvl.de",
                    Strasse = "Goethestr. 12",
                    PLZ ="22587",
                    Ort = "Hamburg"
                },
                new Kunde()
                {
                    Nachname = "Berger",
                    Vorname = "Anne",
                    EMail = "aBerger@offline.de",
                    Strasse = "Drosselweg 67",
                    PLZ ="21057",
                    Ort = "Hamburg"
                },
                new Kunde()
                {
                    Nachname = "Gossmann",
                    Vorname = "Albert",
                    EMail = "aGoss@wideworld.de",
                    Strasse = "Berliner Str. 90",
                    PLZ ="21039",
                    Ort = "Hamburg"
                },
                new Kunde()
                {
                    Nachname = "Neumann",
                    Vorname = "Kirsten",
                    EMail = "kNeumann@offline.de",
                    Strasse = "Bremer. 145",
                    PLZ ="22589",
                    Ort = "Hamburg"
                },
            };

            foreach (var item in kunden)
            {
                await AddItemAsync(item);
            }
        }
    }
}
