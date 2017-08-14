using SQLite;
using System.Collections.Generic;

namespace DiceBagApp.Models
{
    public class Dice
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int NumberFaceOfDice { get; set; }
        public int Quantity { get; set; }
        [Ignore]
        public List<int> Result { get; set; }
        //FK
        public int GroupDiceID { get; set; }
    }
}
