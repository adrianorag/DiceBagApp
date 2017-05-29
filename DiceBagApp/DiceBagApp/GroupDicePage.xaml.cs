using DiceBagApp.Datas;
using DiceBagApp.Models;
using DiceBagApp.Services;
using DiceBagApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiceBagApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GroupDicePage : ContentPage
	{
		public GroupDicePage ()
		{
			InitializeComponent ();
            BindingContext = new GroupDiceViewModel(new DiceService(), DiceDataBase, new Bag());
		}

        #region future injection //TODO: Make injection class
        private DiceDataBase _diceDataBase;

        private DiceDataBase DiceDataBase
        {
            get
            {
                if (_diceDataBase == null)
                    _diceDataBase = new DiceDataBase(DependencyService.Get<IFileHelper>().GetLocalFilePath("BagDiceSQLite.db3"));

                return _diceDataBase;
            }

        }
        #endregion future injection

    }
}