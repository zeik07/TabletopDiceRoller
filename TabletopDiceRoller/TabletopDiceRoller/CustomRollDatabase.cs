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

        public Task<RollItem> GetItemAsync(int id)
        {
            return database.Table<RollItem>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(RollItem item)
        {
            if (item.ID != 0)
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
