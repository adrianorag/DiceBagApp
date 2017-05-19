using System.Collections.Generic;

namespace DiceBagApp.Models
{
    class Dice
    {

        public string Name { get; set; }
        public int MaximumRollValue { get; set; }
        //public int Result { get; set; }

        public int NumberFaceOfDice { get; set; }
        public int Quantity { get; set; }
        public List<int> Result { get; set; }
    }
}
