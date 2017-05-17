
using DiceBagApp.Models;
using DiceBagApp.Services;

namespace DiceBagApp.ViewModels
{
    class GroupDiceViewModel : BaseViewModel
    {

        //services
        private IDiceService _diceService;

        public GroupDiceViewModel(IDiceService diceService) {

            //first step
            _diceService = diceService;

            Quantity = 1;
            Dice = 20;
        }

        #region public data
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

        #endregion public data

    }
}
