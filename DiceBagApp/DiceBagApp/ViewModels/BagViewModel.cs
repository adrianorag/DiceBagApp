using DiceBagApp.Datas;
using DiceBagApp.Models;
using DiceBagApp.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DiceBagApp.ViewModels
{
    class BagViewModel : BaseViewModel
    {
        //services
        private IDiceService _diceService;

        public BagViewModel(IDiceService diceService)
        {
            //first step
            _diceService = diceService;
            GroupDice = new ObservableCollection<GroupDice>();

            //Commands 
            GroupDicePageCommand = new Command(ExecuteGroupDicePageCommand);

            var listDice = DiceTempDataBase.GetItemsAsync();


        }

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

        #region Public Data
        public ObservableCollection<GroupDice> GroupDice { get; set; }
        public Bag Bag {get; set;}
        #endregion Public Data

        #region Command

        public Command GroupDicePageCommand { get; }
        async void ExecuteGroupDicePageCommand()
        {
            await PushAsync<GroupDiceViewModel>(_diceService);
            
            var listDice = DiceTempDataBase.GetItemsAsync();

        }
        #endregion Command

    }
}
