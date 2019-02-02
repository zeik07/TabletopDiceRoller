using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TabletopDiceRoller
{
    public class CustomRollDatabase
    {
        readonly SQLiteAsyncConnection database;

        public CustomRollDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<RollItem>().Wait();
        }

        public Task<List<RollItem>> GetItemsAsync()
        {
            return database.Table<RollItem>().ToListAsync();
        }

        public Task<int> SaveItemAsync(RollItem item)
        {
            if (item.Name != null)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(RollItem item)
        {
            return database.DeleteAsync(item);
        }
    }
}
