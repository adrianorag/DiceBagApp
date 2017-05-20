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
            ///Bag = _diceService.GetDefaultBag();
            IsLoading = false;


            GroupDices = new CustomObservableCollection<GroupDice>();
            LogRoll = new ObservableCollection<LogRoll>();


            //Commands 
            RollDiceCommand = new Command<GroupDice>(ExecuteRollDiceCommand);
            BagPageCommand = new Command(ExecuteBagPageCommand);
            ResetBagCommand = new Command(ExecuteResetBagCommand);

            
        }

        #region Public Data
        public Bag Bag { get; set; }
        public CustomObservableCollection<GroupDice> GroupDices { get; set; }
        public ObservableCollection<LogRoll> LogRoll { get; set; }

        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading, value); }
        }


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
            //First Login in app
            if (listGroupDice == null || listGroupDice.Count == 0)
                ExecuteResetBagCommand();

            GroupDices.Clear();
            foreach (var item in listGroupDice)
            {
                GroupDices.Add(item);
            }
        }

        #region Command
        
        public Command BagPageCommand { get; }

        async void ExecuteBagPageCommand()
        {
            await PushAsync<BagViewModel>(_diceService);
        }

        public Command ResetBagCommand { get; }

        public async void ExecuteResetBagCommand()
        {
            IsLoading = true;
            GroupDices.Clear();
            await Task.Run(() =>
                {
                 DiceTempDataBase.ResetDataBase();
                 Bag = _diceService.GetDefaultBag();
                 DiceTempDataBase.SaveBag(Bag);
                 Task.WaitAll();
                 RefreshListGroupDice();
                 IsLoading = false;
                }
            );
        }


        public Command<GroupDice> RollDiceCommand { get; }
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
