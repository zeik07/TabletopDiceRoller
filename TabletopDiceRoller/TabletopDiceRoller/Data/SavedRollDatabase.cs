using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TabletopDiceRoller
{
    public class SavedRollDatabase
    {
        readonly SQLiteAsyncConnection database;

        public SavedRollDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            //database.CreateTableAsync<Profile>().Wait();
            //database.CreateTableAsync<Folder>().Wait();
            database.CreateTableAsync<RollItem>().Wait();
            //database.CreateTableAsync<HasLevels>().Wait();
        }

        public Task<List<RollItem>> GetItemsAsync()
        {
            return database.GetAllWithChildrenAsync<RollItem>();
        }

        public Task<RollItem> GetItemAsync(int id)
        {
            return database.GetWithChildrenAsync<RollItem>(id);
        }

        public Task SaveItemAsync(RollItem item)
        {
            if (item.RollID != 0)
            {
                return database.UpdateWithChildrenAsync(item);
            }
            else
            {
                return database.InsertWithChildrenAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(RollItem item)
        {
            return database.DeleteAsync(item);
        }
    }
}
