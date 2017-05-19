using DiceBagApp.Datas;
using DiceBagApp.Models;
using DiceBagApp.Services;
using System.Collections.Generic;
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
            ListGroupDice = new ObservableCollection<GroupDice>();

            //Commands 
            GroupDicePageCommand = new Command(ExecuteGroupDicePageCommand);

           
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
        public ObservableCollection<GroupDice> ListGroupDice { get; set; }
        public Bag Bag {get; set;}
        #endregion Public Data

        public Task<List<GroupDice>> RefreshList()
        {
            return DiceTempDataBase.GetGroupDiceAsync();
        }

        #region Command

        public Command GroupDicePageCommand { get; }
        async void ExecuteGroupDicePageCommand()
        {
            await PushModalAsync<GroupDiceViewModel>(_diceService);

        }


        public async Task DeleteGroupDiceAsync(GroupDice groupDice)
        {

            await DiceTempDataBase.DeleteItemAsync(groupDice);
            await DiceTempDataBase.DeleteGroupDiceAsync(groupDice);

        }
        #endregion Command

    }
}
