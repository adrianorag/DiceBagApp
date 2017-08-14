using DiceBagApp.Models;
using System.Collections.Generic;

namespace DiceBagApp.Services
{
    public interface IDiceService
    {
        Bag GetDefaultBag();

        LogRoll RollDice(GroupDice dice);


        string NameDefaultGroupDice(GroupDice groupDice);
    }
}
