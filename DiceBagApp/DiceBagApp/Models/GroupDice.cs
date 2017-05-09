using System.Collections.Generic;

namespace DiceBagApp.Models
{
    class GroupDice
    {
        public string Name { get; set; }
        public int Modifier { get; set; }
        public List<Dice> Dices { get; set; }
        public int LastResult { get; set; }
    }
}
