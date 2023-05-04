namespace CarSharingHamburg.Services
{
    public interface IDataStore<T>
    {
        public Task<bool> AddItemAsync(T item);
        public Task<bool> UpdateItemAsync(T item);
        public Task<bool> DeleteItemAsync(string id);
        public Task<T> GetItemAsync(string id);
        public Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
