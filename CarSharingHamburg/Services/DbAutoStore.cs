using CarSharingHamburg.Models;
using SQLite;

namespace CarSharingHamburg.Services
{
    public class DbAutoStore : IDataStore<Auto>
    {
        private const SQLiteOpenFlags SqliteFlags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        private static readonly string DatabasePath = Path.Combine(FileSystem.AppDataDirectory, "carsharing.db3");

        private static SQLiteAsyncConnection _database;

        public DbAutoStore()
        {
            _database = new SQLiteAsyncConnection(DatabasePath, SqliteFlags);
            _database.CreateTableAsync<Auto>().Wait();
        }


        public async Task<bool> AddItemAsync(Auto auto)
        {
            if (string.IsNullOrEmpty(auto.Id))
            {
                auto.Id = Guid.NewGuid().ToString();

            }
            return await _database.InsertAsync(auto) == 1;
        }

        public async Task<bool> UpdateItemAsync(Auto auto)
        {
            return await _database.UpdateAsync(auto) == 1;
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            return await _database.DeleteAsync<Auto>(id) == 1;
        }

        public async Task<Auto> GetItemAsync(string id)
        {
            return await _database.Table<Auto>()
                .Where(s => s.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Auto>> GetItemsAsync(bool forceRefresh = false)
        {
            var autos = await _database.Table<Auto>().ToListAsync();

            if (autos.Count == 0)
            {
                await SeedData();
                return await GetItemsAsync();
            }
            return autos;
        }

        private async Task SeedData()
        {
            List<Auto> autos = new List<Auto>()
            {
                new Auto()
                {
                    Kennzeichen = "HH D 3349",
                    Modell = "Commander",
                    Fahrzeugztyp = "Limousine",
                    Strasse = "Lokstedter Grenzstr. 2",
                    PLZ = "22527",
                    Ort = "Hamburg"
                },
                 new Auto()
                {
                    Kennzeichen = "HH S 2209",
                    Modell = "Windy",
                    Fahrzeugztyp = "Kleinwagen",
                    Strasse = "Flughafenstr. 1-3",
                    PLZ = "22335",
                    Ort = "Hamburg"
                }, new Auto()
                {
                    Kennzeichen = "HH L 8573",
                    Modell = "Lion",
                    Fahrzeugztyp = "Kombi",
                    Strasse = "Hachmannplatz 16",
                    PLZ = "20099",
                    Ort = "Hamburg"
                },
                  new Auto()
                {
                    Kennzeichen = "HH P 3510",
                    Modell = "Zefir",
                    Fahrzeugztyp = "Kleinwagen",
                    Strasse = "Kehrwieder 2",
                    PLZ = "20457",
                    Ort = "Hamburg"
                }
            };

            foreach (var item in autos)
            {
                await AddItemAsync(item);
            }

        }

    }
}
