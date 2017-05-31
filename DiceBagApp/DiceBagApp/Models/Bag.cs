using SQLite;
using System.Collections.Generic;

namespace DiceBagApp.Models
{
    class Bag
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        [Ignore]
        public List<GroupDice> GroupDices { get; set; }
        public bool Active { get; set; }

        //TODO: Create another class for configuration
        public string Color { get; set; }

    }
}
