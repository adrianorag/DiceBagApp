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
            var bagDefault = _diceService.GetDefaultBag();
            GroupDices = new ObservableCollection<GroupDice>(bagDefault.GroupDices);
            LogRoll = new ObservableCollection<LogRoll>();


            //Commands 
            RollDiceCommand = new Command<GroupDice>(ExecuteRollDiceCommand);
            BagPageCommand = new Command(ExecuteBagPageCommand);
        }

        #region Public Data
        public ObservableCollection<GroupDice> GroupDices { get; set; }
        public ObservableCollection<LogRoll> LogRoll { get; set; }

        #endregion Public Data


        #region Command
        public Command<GroupDice> RollDiceCommand { get; }
        public Command BagPageCommand { get; }

        async void ExecuteBagPageCommand()
        {
            await PushAsync<BagViewModel>(_diceService);
        }


        public void ExecuteRollDiceCommand(GroupDice groupDice)
        {

            var result = _diceService.RollDice(groupDice);
            LogRoll.Add(result);
            groupDice.LastResult = result.Result;

            var i = GroupDices.IndexOf(groupDice);
            GroupDices.RemoveAt(i);
            GroupDices.Insert(i, groupDice);
        }
        #endregion Command
    }
}
