using DiceBagApp.Services;

namespace DiceBagApp.ViewModels
{
    class BagViewModel : BaseViewModel
    {
        //services
        private IDiceService _diceService;

        public BagViewModel(IDiceService diceService)
        {
            _diceService = diceService;
        }
    }
}
