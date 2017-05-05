using System;
using DiceBagApp.Models;
using System.Collections.Generic;

namespace DiceBagApp.Services
{
    class DiceService : IDiceService
    {

        public LogRoll RollDice(Dice dice) {
            var logRoll = new LogRoll();
            logRoll.Dice = dice;
            logRoll.Date = DateTime.Now;

            Random rnd = new Random();
            logRoll.Result = rnd.Next(1, (1+logRoll.Dice.MaximumRollValue));

            WriteDescriptionRollDice(ref logRoll);
            return logRoll;
        }


        private void WriteDescriptionRollDice(ref LogRoll logRoll )
        {
            var description = $"( 1{logRoll.Dice.Name} )";

            logRoll.Description = description;
        }



        public Bag GetDefaultBag()
        {
            var Bag = new Bag();
            Bag.Name = "Default";

            var ListDice = new List<Dice>();
            ListDice.Add( new Dice() { Name = "D4", MaximumRollValue=4, Modifier=0 });
            ListDice.Add(new Dice() { Name = "D6", MaximumRollValue = 6, Modifier = 0 });
            ListDice.Add(new Dice() { Name = "D8", MaximumRollValue = 8, Modifier = 0 });
            ListDice.Add(new Dice() { Name = "D10", MaximumRollValue = 10, Modifier = 0 });
            ListDice.Add(new Dice() { Name = "D12", MaximumRollValue = 12, Modifier = 0 });
            ListDice.Add(new Dice() { Name = "D20", MaximumRollValue = 20, Modifier = 0 });

            Bag.Dices = ListDice;
            return Bag;
        }
    }
}
