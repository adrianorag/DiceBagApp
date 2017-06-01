
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
        Task<int> DeleteDiceAsync(Dice item);

        Task DeleteDiceByGroupDiceAsync(GroupDice groupDice);

        void SaveGroupDiceAndItemAsync(GroupDice groupDice);
        Task<int> SaveGroupDiceAsync(GroupDice item);
        Task<int> DeleteGroupDiceAsync(GroupDice item);
        Task<List<GroupDice>> GetGroupDiceAsync();
        List<GroupDice> GetGroupDice(int bagID);
        Task<List<GroupDice>> GetGroupDiceByBagID(int bagID);


        void SaveBag(Bag bag);
        Task<int> DeleteBagAsync(Bag bag);
        Task<List<Bag>> GetBagAsync(bool active=true);
        Task<Bag> GetFirstBagAsync(bool active = true);
        Task<Bag> GetBagAsync(int id);
        Task<int> SaveBagAsync(Bag item);
    }
}
