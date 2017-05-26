﻿
using DiceBagApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiceBagApp.Datas
{
    interface IDiceDataBase
    { 
        void ResetDataBase();

        Task<List<Dice>> GetDiceAsync();
        Task<List<Dice>> GetDiceByGroupDice(int GroupDiceID);
        Task<Dice> GetDiceAsync(int id);
        Task<int> SaveDiceAsync(Dice item);
        Task<int> DeleteItemAsync(Dice item);

        Task DeleteItemAsync(GroupDice groupDice);
        void SaveGroupDiceAndItemAsync(GroupDice groupDice);
        Task<int> SaveGroupDiceAsync(GroupDice item);
        Task<int> DeleteGroupDiceAsync(GroupDice item);
        Task<List<GroupDice>> GetGroupDiceAsync();
        List<GroupDice> GetGroupDice();

        void SaveBag(Bag bag);
        Task<List<Bag>> GetBagAsync();
        Task<Bag> GetBagAsync(int id);
        Task<int> SaveBagAsync(Bag item);
    }
}
