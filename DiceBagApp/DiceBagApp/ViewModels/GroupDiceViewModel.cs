
using DiceBagApp.Datas;
using DiceBagApp.Models;
using DiceBagApp.Services;
using Xamarin.Forms;

namespace DiceBagApp.ViewModels
{
    class GroupDiceViewModel : BaseViewModel
    {

        //services
        private IDiceService _diceService;

        public GroupDiceViewModel(IDiceService diceService) {

            //first step
            _diceService = diceService;

            Quantity = 1;
            Dice = 20;

            //Command
            AddDiceCommand = new Command(ExecuteAddDiceCommand);
            CancelDiceCommand = new Command(ExecuteCancelDiceCommand);
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

        #region public data
        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set {
                SetProperty(ref _quantity, value);
            }
        }

        private int _dice;

        public int Dice
        {
            get { return _dice; }
            set
            {
                SetProperty(ref _dice, value);
            }
        }

        #endregion public data


        #region Command
        public Command AddDiceCommand { get; }

        async void ExecuteAddDiceCommand()
        {
            
            var diceTemp = new DiceTemp()
            {
                NumberFaceOfDice = Dice,
                Quantity = Quantity,
            };

            var id = await DiceTempDataBase.SaveItemAsync(diceTemp);

            await PopAsync();
        }


        public Command CancelDiceCommand { get; }
        async void ExecuteCancelDiceCommand()
        {

            await PopAsync();
        }
        #endregion Command

    }
}
