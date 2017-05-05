using DiceBagApp.Models;
using System.Collections.Generic;

namespace DiceBagApp.Services
{
    interface IDiceService
    {
        Bag GetDefaultBag();

        LogRoll RollDice(Dice dice);
    }
}
