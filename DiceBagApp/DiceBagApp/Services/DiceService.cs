using System;
using DiceBagApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace DiceBagApp.Services
{
    class DiceService : IDiceService
    {

        public LogRoll RollDice(GroupDice groupDice) {
            var logRoll = new LogRoll();
            logRoll.GroupDice = groupDice;
            logRoll.Date = DateTime.Now;

            Random rnd = new Random();
            logRoll.Result = 0;
            foreach (var diceLog in logRoll.GroupDice?.Dices)
            {
                diceLog.Result = rnd.Next(1, (1 + diceLog.MaximumRollValue));
            }
            logRoll.Result = logRoll.GroupDice.Dices.Sum(_ => _.Result);


            WriteDescriptionRollDice(ref logRoll);
            return logRoll;
        }


        private void WriteDescriptionRollDice(ref LogRoll logRoll )
        {
            var description = "(";

            foreach (var dice in logRoll.GroupDice?.Dices)
            {
                if (!dice.Equals(logRoll.GroupDice.Dices.First()))
                    description += " + ";
                description += $"{dice.Result} ";
            }
            description += $")";

            logRoll.Description = description;
        }



        public Bag GetDefaultBag()
        {
            var Bag = new Bag();
            Bag.Name = "Default";

            var ListGroupDice = new List<GroupDice>();

            ListGroupDice.Add(new GroupDice() {
                Name = "D4",
                Dices = new List<Dice>(new Dice[] { new Dice { Name = "D4", MaximumRollValue = 4 } })
                ,LastResult =0
                
            });

            ListGroupDice.Add(new GroupDice()
            {
                Name = "D6",
                Dices = new List<Dice>(new Dice[] { new Dice { Name = "D6", MaximumRollValue = 6 } })
                ,
                LastResult = 0
            });

            ListGroupDice.Add(new GroupDice()
            {
                Name = "2D6D10",
                Dices = new List<Dice>(new Dice[] { new Dice { Name = "D6", MaximumRollValue = 6 }, new Dice { Name = "D6", MaximumRollValue = 6 } , new Dice { Name = "D10", MaximumRollValue = 10 } })
                
            });

            ListGroupDice.Add(new GroupDice()
            {
                Name = "D8",
                Dices = new List<Dice>(new Dice[] { new Dice { Name = "D8", MaximumRollValue = 8 } })
                ,
                LastResult = 0
            });

            ListGroupDice.Add(new GroupDice()
            {
                Name = "D10",
                Dices = new List<Dice>(new Dice[] { new Dice { Name = "D10", MaximumRollValue = 10 } })
                ,
                LastResult = 0
            });

            ListGroupDice.Add(new GroupDice()
            {
                Name = "D12",
                Dices = new List<Dice>(new Dice[] { new Dice { Name = "D12", MaximumRollValue = 12 } })
                ,
                LastResult = 0
            });
            ListGroupDice.Add(new GroupDice()
            {
                Name = "D20",
                Dices = new List<Dice>(new Dice[] { new Dice { Name = "D20", MaximumRollValue = 20 } })
                ,
                LastResult = 0
            });

            Bag.GroupDices = ListGroupDice;
            return Bag;
        }
    }
}
