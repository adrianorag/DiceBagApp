using DiceBagApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace DiceBagApp.Datas
{
    class DiceTempDataBase
    {
        readonly SQLiteAsyncConnection database;

        public DiceTempDataBase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<DiceTemp>().Wait();
        }
        
        public Task<List<DiceTemp>> GetItemsAsync()
        {
            return database.Table<DiceTemp>().ToListAsync();
        }

        public Task<List<DiceTemp>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<DiceTemp>("SELECT * FROM [DiceTemp] WHERE [Done] = 0");
        }

        public Task<DiceTemp> GetItemAsync(int id)
        {
            return database.Table<DiceTemp>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(DiceTemp item)
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

        public Task<int> DeleteItemAsync(DiceTemp item)
        {
            return database.DeleteAsync(item);
        }

    }
}
