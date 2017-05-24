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
            foreach (var dice in logRoll.GroupDice.Dices)
            {
                dice.Result = new List<int>();
                for (int i = 0; i < dice.Quantity ; i++)
                {
                    dice.Result.Add(rnd.Next(1, (1 + dice.NumberFaceOfDice)));
                }
            }
            logRoll.Result = logRoll.GroupDice.Dices.Sum(_ => _.Result.Sum());


            WriteDescriptionRollDice(ref logRoll);
            return logRoll;
        }


        private void WriteDescriptionRollDice(ref LogRoll logRoll )
        {
            var description = logRoll.GroupDice.Name + "(";

            foreach (var dice in logRoll.GroupDice?.Dices)
            {
                if (!dice.Equals(logRoll.GroupDice.Dices.First()))
                    description += "+";

                var aux = "";
                foreach (var itemResult in dice.Result)
                {
                    description += $"{aux}{itemResult} ";
                    aux = "+";
                }
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
                Dices = new List<Dice>(new Dice[] { new Dice { NumberFaceOfDice = 4, Quantity=1 } })
                ,LastResult =0
                
            });

            ListGroupDice.Add(new GroupDice()
            {
                Name = "D6",
                Dices = new List<Dice>(new Dice[] { new Dice { NumberFaceOfDice = 6, Quantity = 1 } })
                ,
                LastResult = 0
            });
            

            ListGroupDice.Add(new GroupDice()
            {
                Name = "D8",
                Dices = new List<Dice>(new Dice[] { new Dice { NumberFaceOfDice = 8, Quantity = 1 } })
                ,
                LastResult = 0
            });

            ListGroupDice.Add(new GroupDice()
            {
                Name = "D10",
                Dices = new List<Dice>(new Dice[] { new Dice { NumberFaceOfDice = 10, Quantity = 1 } })
                ,
                LastResult = 0
            });

            ListGroupDice.Add(new GroupDice()
            {
                Name = "D12",
                Dices = new List<Dice>(new Dice[] { new Dice { NumberFaceOfDice = 12, Quantity = 1 } })
                ,
                LastResult = 0
            });
            ListGroupDice.Add(new GroupDice()
            {
                Name = "D20",
                Dices = new List<Dice>(new Dice[] { new Dice { NumberFaceOfDice = 20, Quantity = 1 } })
                ,
                LastResult = 0
            });

            Bag.GroupDices = ListGroupDice;
            return Bag;
        }



        public string NameDefaultGroupDice(GroupDice groupDice)
        {
            var returnName = "";

            var aux = "";
            var auxNumber = "";
            foreach (var dice in groupDice.Dices)
            {
                auxNumber = (dice.Quantity > 1) ? dice.Quantity.ToString() : ""; 
                returnName += $"{aux}{auxNumber}D{dice.NumberFaceOfDice}";

                aux = " + ";
            }

            returnName = returnName.Trim();

            return returnName;
        }

    }
}
