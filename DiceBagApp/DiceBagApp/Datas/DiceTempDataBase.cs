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
            CreateAllBase();
        }


        public void ResetDataBase()
        {
            DropAllDatabe();
            CreateAllBase();
        }

        private void CreateAllBase()
        {

            database.CreateTableAsync<Dice>().Wait();
            database.CreateTableAsync<GroupDice>().Wait();
            database.CreateTableAsync<Bag>().Wait();
        }

        private void DropAllDatabe()
        {
            database.DropTableAsync<Dice>().Wait();
            database.DropTableAsync<GroupDice>().Wait();
            database.DropTableAsync<Bag>().Wait();
        }

        #region Dice

        public Task<List<Dice>> GetDiceAsync()
        {
            return database.Table<Dice>().ToListAsync();
        }
        
        public Task<List<Dice>> GetDiceByGroupDice(int GroupDiceID)
        {
            return database.QueryAsync<Dice>($"SELECT * FROM [{nameof(Dice)}] WHERE [GroupDiceID] = {GroupDiceID}");
        }

        public Task<Dice> GetDiceAsync(int id)
        {
            return database.Table<Dice>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveDiceAsync(Dice item)
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

        public Task<int> DeleteItemAsync(Dice item)
        {
            return database.DeleteAsync(item);
        }

        public async Task DeleteItemAsync(GroupDice groupDice)
        {
            var listItem = await GetDiceByGroupDice(groupDice.ID);
            foreach (var item in listItem)
            {
                await this.DeleteItemAsync(item);
            }
        }

        #endregion Dice

        #region GroupDice

        public async void  SaveGroupDiceAndItemAsync(GroupDice groupDice)
        {
            var taskGroupDice = SaveGroupDiceAsync(groupDice);
            taskGroupDice.Wait();

            foreach (var dice in groupDice.Dices)
            {
                if (dice.Quantity == 0 || dice.NumberFaceOfDice == 0)
                    continue;

                dice.GroupDiceID = groupDice.ID;
                await SaveDiceAsync(dice);
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
                var taskItem = GetDiceByGroupDice(groupDice.ID);

                taskItem.Wait();

                groupDice.Dices = taskItem.Result;
            }

            return listGroupDice;
        }
        #endregion GroupDice


        #region Bag
        public void SaveBag(Bag bag)
        {
            foreach (var groupDices in bag.GroupDices)
            {
                SaveGroupDiceAndItemAsync(groupDices);
            }
        }

        public Task<List<Bag>> GetBagAsync()
        {
            return database.Table<Bag>().ToListAsync();
        }

        public Task<Bag> GetBagAsync(int id)
        {
            return database.Table<Bag>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveBagAsync(Bag item)
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

        #endregion Bag
    }
}
