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

            //resetando sempre para testes
            //database.DropTableAsync<DiceTemp>().Wait();
            //database.DropTableAsync<GroupDice>().Wait();

            database.CreateTableAsync<DiceTemp>().Wait();
            database.CreateTableAsync<GroupDice>().Wait();
        }

        #region DiceTemp

        public Task<List<DiceTemp>> GetItemsAsync()
        {
            return database.Table<DiceTemp>().ToListAsync();
        }

        /*public Task<List<DiceTemp>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<DiceTemp>("SELECT * FROM [DiceTemp] WHERE [Done] = 0");
        }*/
        public Task<List<DiceTemp>> GetItemsByGroupDice(int GroupDiceID)
        {
            return database.QueryAsync<DiceTemp>($"SELECT * FROM [DiceTemp] WHERE [GroupDiceID] = {GroupDiceID}");
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
        #endregion DiceTemp

        #region GroupDiceTemp
        public Task<int> SaveGroupDiceAsync(GroupDice item)
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
        public Task<int> DeleteGroupDiceAsync(GroupDice item)
        {
            return database.DeleteAsync(item);
        }

        public Task<List<GroupDice>> GetGroupDiceAsync()
        {
            return database.Table<GroupDice>().ToListAsync();
        }

        #endregion GroupDiceTemp
    }
}
