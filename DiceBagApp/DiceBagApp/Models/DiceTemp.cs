using SQLite;

namespace DiceBagApp.Models
{
    class DiceTemp
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int Quantity { get; set; }
        public int NumberFaceOfDice { get; set; }
    }
}
