
using DiceBagApp.Services;

namespace DiceBagApp.ViewModels
{
    class GroupDiceViewModel : BaseViewModel
    {

        //services
        private IDiceService _diceService;

        public GroupDiceViewModel(IDiceService diceService) {

            //first step
            _diceService = diceService;
        }
    }
}
