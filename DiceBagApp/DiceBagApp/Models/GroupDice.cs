using SQLite;
using System.Collections.Generic;

namespace DiceBagApp.Models
{
    public class GroupDice
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        //FK
        public int BagID { get; set; }
        public string Name { get; set; }
        public int Modifier { get; set; }
        [Ignore]
        public List<Dice> Dices { get; set; }
        [Ignore]
        public int LastResult { get; set; }
    }
}
