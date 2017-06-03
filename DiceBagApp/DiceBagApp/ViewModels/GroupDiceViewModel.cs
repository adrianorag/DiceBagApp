using DiceBagApp.Datas;
using DiceBagApp.Models;
using DiceBagApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DiceBagApp.ViewModels
{
    class GroupDiceViewModel : BaseViewModel
    {
        
        //services
        private IDiceService _diceService { get; }
        private IDiceDataBase _diceDataBase { get; }

        public GroupDiceViewModel(IDiceService diceService, IDiceDataBase diceDataBase, Bag bag)
        {
            //first step
            _diceService = diceService;
            _diceDataBase = diceDataBase;
            Bag = bag;

            ListDices = new ObservableCollection<Dice>();
            ListDices.Add(new Dice() { Quantity = 1, NumberFaceOfDice = 20 });

            //Command
            AddDiceCommand = new Command(ExecuteAddDiceCommand);
            RemoveLastDiceCommand = new Command(ExecuteRemoveLastDiceCommand);
            SaveCommand = new Command(ExecuteSaveCommand);
            CancelDiceCommand = new Command(ExecuteCancelDiceCommand);
            AddModifierCommand = new Command(ExecuteAddModifierCommand);
        }

        #region public data
        public Bag Bag { get; set; }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set {
                SetProperty(ref _quantity, value);
            }
        }

        private int _dice;
        public int Dice
        {
            get { return _dice; }
            set
            {
                SetProperty(ref _dice, value);
            }
        }

        private int _modifier;
        public int Modifier
        {
            get { return _modifier; }
            set
            {
                SetProperty(ref _modifier, value);
            }
        }

        public ObservableCollection<Dice> ListDices { get; set;}

        #endregion public data


        #region Command
        public Command AddDiceCommand { get; }

        void ExecuteAddDiceCommand()
        {

           ListDices.Add(new Dice() { Quantity =1 });

        }

        public Command SaveCommand { get; }
        async void ExecuteSaveCommand()
        {
            var groupDice = new GroupDice();
            groupDice.Dices = new List<Dice>();
            groupDice.BagID = Bag.ID;
            groupDice.Modifier = Modifier;

            foreach (var dice in ListDices)
            {
                if (dice.Quantity == 0 || dice.NumberFaceOfDice == 0)
                    continue;

                groupDice.Dices.Add(new Dice()
                {
                    Quantity = dice.Quantity,
                    NumberFaceOfDice = dice.NumberFaceOfDice
                });
            }

            if (groupDice.Dices.Count == 0)
            {
                await PopModalAsync();
                return;
            }

            groupDice.Name = _diceService.NameDefaultGroupDice(groupDice);
            _diceDataBase.SaveGroupDiceAndItemAsync(groupDice);

            await PopModalAsync();
        }

        public Command RemoveLastDiceCommand { get; }
        void ExecuteRemoveLastDiceCommand()
        {

            if (ListDices.Count == 0)
                return;

            ListDices.RemoveAt(ListDices.Count - 1);
        }

        public Command CancelDiceCommand { get; }
        async void ExecuteCancelDiceCommand()
        {

            await PopModalAsync();
        }

        public Command AddModifierCommand { get; }
        void ExecuteAddModifierCommand(object param)
        {
            var val = int.Parse(param.ToString());

            Modifier += val;
        }
        #endregion Command

    }
}
