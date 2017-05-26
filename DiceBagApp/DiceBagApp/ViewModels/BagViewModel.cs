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
        private IDiceService _diceService { get; }
        private IDiceDataBase _diceDataBase { get; }

        public BagViewModel(IDiceService diceService, IDiceDataBase diceDataBase)
        {
            //first step
            _diceService = diceService;
            _diceDataBase = diceDataBase;
            
            ListGroupDice = new ObservableCollection<GroupDice>();

            //Commands 
            GroupDicePageCommand = new Command(ExecuteGroupDicePageCommand);

           
        }

        #region Public Data
        public ObservableCollection<GroupDice> ListGroupDice { get; set; }
        public Bag Bag {get; set;}
        #endregion Public Data

        public Task<List<GroupDice>> RefreshList()
        {
            return _diceDataBase.GetGroupDiceAsync();
        }

        #region Command

        public Command GroupDicePageCommand { get; }
        async void ExecuteGroupDicePageCommand()
        {
            await PushModalAsync<GroupDiceViewModel>(_diceService, _diceDataBase);
        }
        
        public async Task DeleteGroupDiceAsync(GroupDice groupDice)
        {
            await _diceDataBase.DeleteItemAsync(groupDice);
            await _diceDataBase.DeleteGroupDiceAsync(groupDice);
        }
        #endregion Command

    }
}
