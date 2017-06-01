using DiceBagApp.Datas;
using DiceBagApp.Helpers;
using DiceBagApp.Models;
using DiceBagApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DiceBagApp.ViewModels
{
    class RoomViewModel : BaseViewModel
    {
        //services
        private IDiceService _diceService { get; }
        private IDiceDataBase _diceDataBase { get; }

        public RoomViewModel(IDiceService diceService, IDiceDataBase diceDataBase, Bag bag = null)
        {
            _diceService = diceService;
            _diceDataBase = diceDataBase;
            Bag = bag == null ? GetBagConfiguration() : bag;

            IsLoading = false;


            GroupDices = new CustomObservableCollection<GroupDice>();
            LogRoll = new ObservableCollection<LogRoll>();


            //Commands 
            RollDiceCommand = new Command<GroupDice>(ExecuteRollDiceCommand);
            BagPageCommand = new Command(ExecuteBagPageCommand);
            ResetBagCommand = new Command(ExecuteResetBagCommand);
            ClearLogCommand = new Command(ExecuteClearLogCommand);
        }

        private Bag GetBagConfiguration()
        {

            var taskBag = _diceDataBase.GetFirstBagAsync(true);
            taskBag.Wait();
            var bag = taskBag.Result;
            return bag;
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
        
        public async void RefreshListGroupDice()
        {
            await Task.Run(() =>
            {
                var listGroupDice = _diceDataBase.GetGroupDice(Bag.ID);
                //First Login in app
                if (Bag == null || Bag.ID == 0)
                {
                    ExecuteResetBagCommand();
                }
                else if (listGroupDice == null || listGroupDice.Count == 0)
                {
                    DesactiveBag(Bag);
                    return;
                }
                   

                GroupDices.Clear();
                foreach (var item in listGroupDice)
                {
                    GroupDices.Add(item);
                }
            });
        }

        private void DesactiveBag(Bag bag)
        {
            
            var taskDel =_diceDataBase.DeleteBagAsync(bag);
            taskDel.Wait();
            Bag = GetBagConfiguration();
            RefreshListGroupDice();

            base.RefreshMasterPage();
        }

        #region Command

        public Command BagPageCommand { get; }

        async void ExecuteBagPageCommand()
        {
            await PushAsync<BagViewModel>(_diceService, _diceDataBase, Bag);
        }


        public Command ClearLogCommand { get; }
        public void ExecuteClearLogCommand()
        {
            LogRoll.Clear();
        }


        public Command ResetBagCommand { get; }

        public async void ExecuteResetBagCommand()
        {
            IsLoading = true;
            GroupDices.Clear();
            await Task.Run(() =>
                {
                    _diceDataBase.ResetDataBase();
                    Bag = _diceService.GetDefaultBag();
                    _diceDataBase.SaveBag(Bag);
                    Task.WaitAll();
                    RefreshListGroupDice();
                    IsLoading = false;
                    base.RefreshMasterPage();
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
