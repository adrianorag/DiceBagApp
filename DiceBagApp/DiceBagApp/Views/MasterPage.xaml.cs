using DiceBagApp.Datas;
using DiceBagApp.Models;
using DiceBagApp.Services;
using DiceBagApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiceBagApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MasterPage : ContentPage
	{
		public MasterPage()
		{
            InitializeComponent();
            BindingContext = new MasterViewModel(new DiceService(), DiceDataBase);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((MasterViewModel)BindingContext).RefreshListBag();
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

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var bag = (sender as ListView).SelectedItem as Bag;
            (BindingContext as MasterViewModel)?.SelectBagToRoom(bag);
        }
    }
}