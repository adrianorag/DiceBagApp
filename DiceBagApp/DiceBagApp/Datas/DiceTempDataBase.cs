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
            database.CreateTableAsync<GroupDice>().Wait();
        }


        public void ResetDataBase()
        {
            //resetando para testes
            database.DropTableAsync<DiceTemp>().Wait();
            database.DropTableAsync<GroupDice>().Wait();

            database.CreateTableAsync<DiceTemp>().Wait();
            database.CreateTableAsync<GroupDice>().Wait();
        }

        #region DiceTemp

        public Task<List<DiceTemp>> GetItemsAsync()
        {
            return database.Table<DiceTemp>().ToListAsync();
        }
        
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

        public async Task DeleteItemAsync(GroupDice groupDice)
        {
            var listItem = await GetItemsByGroupDice(groupDice.ID);
            foreach (var item in listItem)
            {
                await this.DeleteItemAsync(item);
            }
        }

        #endregion DiceTemp

        #region GroupDiceTemp

        public async void  SaveGroupDiceAndItemAsync(GroupDice groupDice)
        {
            var taskGroupDice = SaveGroupDiceAsync(groupDice);
            taskGroupDice.Wait();

            foreach (var dice in groupDice.Dices)
            {
                if (dice.Quantity == 0 || dice.NumberFaceOfDice == 0)
                    continue;

                var s = new DiceTemp() { NumberFaceOfDice = dice.NumberFaceOfDice, Quantity = dice.Quantity, GroupDiceID = groupDice.ID };
                await SaveItemAsync(s);
            }
        }

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

        public List<GroupDice> GetGroupDice()
        {
            var taskGroupDice = this.GetGroupDiceAsync();

            var listGroupDice = taskGroupDice.Result;

            foreach (var groupDice in listGroupDice)
            {
                var taskItem = GetItemsByGroupDice(groupDice.ID);

                groupDice.Dices = new List<Dice>();
                foreach (var item in taskItem.Result)
                {
                    groupDice.Dices.Add(new Dice() { NumberFaceOfDice = item.NumberFaceOfDice, Quantity = item.Quantity });
                }
            }

            return listGroupDice;
        }
        #endregion GroupDiceTemp


        #region Bag
        public void SaveBag(Bag bag)
        {
            foreach (var groupDices in bag.GroupDices)
            {
                SaveGroupDiceAndItemAsync(groupDices);
            }
        }

        #endregion Bag
    }
}
