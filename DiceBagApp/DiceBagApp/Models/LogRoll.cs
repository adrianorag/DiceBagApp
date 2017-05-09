using System;

namespace DiceBagApp.Models
{
    class LogRoll
    {
        public DateTime Date { get; set; }
        public GroupDice GroupDice { get; set; }
        public int Result { get; set; }
        public string Description { get; set; }
    }
}
