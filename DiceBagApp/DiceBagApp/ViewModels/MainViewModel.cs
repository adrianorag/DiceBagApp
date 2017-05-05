using DiceBagApp.Models;
using DiceBagApp.Services;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DiceBagApp.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        //services
        private IDiceService _diceService { get; }

        public MainViewModel(IDiceService diceService)
        {
            _diceService = diceService;

            //firt step
            Bag = _diceService.GetDefaultBag();
            LogRoll = new ObservableCollection<LogRoll>();

            //Commands 
            RollDiceCommand = new Command<Dice>(ExecuteRollDiceCommand);
        }

        #region Public Data
        public Bag Bag { get; set; }
        public ObservableCollection<LogRoll> LogRoll { get; set; }

        #endregion Public Data


        #region Command
        public Command<Dice> RollDiceCommand { get; }


        public void ExecuteRollDiceCommand(Dice dice)
        {

            var result = _diceService.RollDice(dice);
            LogRoll.Add(result);

        }
        #endregion Command
    }
}
