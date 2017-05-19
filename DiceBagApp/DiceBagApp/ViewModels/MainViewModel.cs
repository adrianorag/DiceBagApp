using DiceBagApp.Datas;
using DiceBagApp.Models;
using DiceBagApp.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
             

            GroupDices = new ObservableCollection<GroupDice>();
            LogRoll = new ObservableCollection<LogRoll>();


            //Commands 
            RollDiceCommand = new Command<GroupDice>(ExecuteRollDiceCommand);
            BagPageCommand = new Command(ExecuteBagPageCommand);
        }

        #region Public Data
        public Bag Bag { get; set; }
        public ObservableCollection<GroupDice> GroupDices { get; set; }
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

        public List<GroupDice> RefreshListGroupDice()
        {
            var listGroupDice = DiceTempDataBase.GetGroupDice();
            if (listGroupDice == null || listGroupDice.Count == 0)
                listGroupDice = this.Bag.GroupDices;

            GroupDices = new ObservableCollection<GroupDice>(listGroupDice);

            return listGroupDice;
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

            OnPropertyChanged(nameof(groupDice.LastResult));

            /*var i = GroupDices.IndexOf(groupDice);
            if(i> 0)
            {
                GroupDices.RemoveAt(i);
                GroupDices.Insert(i, groupDice);
            }*/
        }
        #endregion Command
    }
}
