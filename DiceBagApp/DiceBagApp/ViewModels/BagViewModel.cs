using DiceBagApp.Models;
using DiceBagApp.Services;
using System.Collections.ObjectModel;
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
        }

        #region Public Data
        public ObservableCollection<GroupDice> GroupDice { get; set; }
        public Bag Bag {get; set;}
        #endregion Public Data

        #region Command

        public Command GroupDicePageCommand { get; }
        async void ExecuteGroupDicePageCommand()
        {
            await PushAsync<GroupDiceViewModel>(_diceService);
        }
        #endregion Command

    }
}
