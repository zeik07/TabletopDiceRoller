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
            database.CreateTablesAsync<RollItem, HasLevels>().Wait();
        }        

        public Task<List<RollItem>> GetItemsAsync()
        {
            return database.Table<RollItem>().ToListAsync();
        }

        public Task<RollItem> GetItemAsync(int id)
        {
            return database.GetWithChildrenAsync<RollItem>(id, recursive: true);
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
