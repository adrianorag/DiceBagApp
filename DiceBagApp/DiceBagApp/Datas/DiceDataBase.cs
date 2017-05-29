using DiceBagApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace DiceBagApp.Datas
{
    class DiceDataBase : IDiceDataBase
    {
        readonly SQLiteAsyncConnection database;

        public DiceDataBase(string dbPath)
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

        public List<GroupDice> GetGroupDice(int BagID)
        {
            var taskGroupDice = this.GetGroupDiceByBagID(BagID);

            var listGroupDice = taskGroupDice.Result;

            foreach (var groupDice in listGroupDice)
            {
                var taskItem = GetDiceByGroupDice(groupDice.ID);

                taskItem.Wait();

                groupDice.Dices = taskItem.Result;
            }

            return listGroupDice;
        }

        public Task<List<GroupDice>> GetGroupDiceByBagID(int BagID)
        {
            return database.QueryAsync<GroupDice>($"SELECT * FROM [{nameof(GroupDice)}] WHERE [BagID] = {BagID}");
        }

        #endregion GroupDice


        #region Bag
        public void SaveBag(Bag bag)
        {

            var task = SaveBagAsync(bag);
            task.Wait();

            foreach (var groupDices in bag.GroupDices)
            {
                groupDices.BagID = bag.ID;
                SaveGroupDiceAndItemAsync(groupDices);
            }
        }

        public Task<List<Bag>> GetBagAsync()
        {
            return database.Table<Bag>().ToListAsync();
        }

        public Task<Bag> GetBagAsync(int id)
        {
            var taskMaster = Task.Run(() => {
                var taskBag = database.Table<Bag>().Where(i => i.ID == id).FirstOrDefaultAsync();
                taskBag.Wait();
                Bag bag = taskBag.Result;
                List<GroupDice> groupDices = GetGroupDice(bag.ID);
                bag.GroupDices = groupDices;

                return bag;
            });
            return taskMaster;
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
