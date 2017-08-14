using SQLite;
using System;

namespace DiceBagApp.Models
{
    public class LogRoll
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime Date { get; set; }
        [Ignore]
        public GroupDice GroupDice { get; set; }
        public int Result { get; set; }
        public string Description { get; set; }
    }
}
