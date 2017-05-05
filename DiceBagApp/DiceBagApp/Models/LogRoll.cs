using System;

namespace DiceBagApp.Models
{
    class LogRoll
    {
        public DateTime Date { get; set; }
        public Dice Dice { get; set; }
        public int Result { get; set; }
        public string Description { get; set; }
    }
}
