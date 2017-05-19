using DiceBagApp.Datas;
using DiceBagApp.Helpers;
using DiceBagApp.Models;
using DiceBagApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
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

            //first step
            Bag = _diceService.GetDefaultBag();
             

            GroupDices = new CustomObservableCollection<GroupDice>();
            LogRoll = new ObservableCollection<LogRoll>();


            //Commands 
            RollDiceCommand = new Command<GroupDice>(ExecuteRollDiceCommand);
            BagPageCommand = new Command(ExecuteBagPageCommand);
        }

        #region Public Data
        public Bag Bag { get; set; }
        public CustomObservableCollection<GroupDice> GroupDices { get; set; }
        public ObservableCollection<LogRoll> LogRoll { get; set; }

        #endregion Public Data


        #region future injection //TODO: Make injection class
        private DiceTempDataBase _diceTempDataBase;

        private DiceTempDataBase DiceTempDataBase
        {
            get
            {
                if (_diceTempDataBase == null)
                    _diceTempDataBase = new DiceTempDataBase(DependencyService.Get<IFileHelper>().GetLocalFilePath("BagDiceSQLite.db3"));

                return _diceTempDataBase;
            }

        }
        #endregion future injection

        public void RefreshListGroupDice()
        {
            var listGroupDice = DiceTempDataBase.GetGroupDice();
            if (listGroupDice == null || listGroupDice.Count == 0)
                listGroupDice = this.Bag.GroupDices;

            GroupDices.Clear();
            foreach (var item in listGroupDice)
            {
                GroupDices.Add(item);
            }
        }

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

            
            GroupDices.ReportItemChange(groupDice);
        }
        #endregion Command
    }
}
