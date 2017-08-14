using DiceBagApp.Datas;
using DiceBagApp.Models;
using DiceBagApp.Services;
using DiceBagApp.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiceBagApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoomPage : ContentPage
	{
		public RoomPage()
		{
			InitializeComponent ();
            BindingContext = new RoomViewModel(new DiceService(), DiceDataBase);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var task = ((RoomViewModel)BindingContext).RefreshListGroupDice();
            task.Wait();
            LogRollScrollToEnd();
            
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var groupDice = (sender as ListView).SelectedItem as GroupDice;
            (BindingContext as RoomViewModel)?.RollDiceCommand.Execute(groupDice);

            var lastItem = eListViewLogRoll.ItemsSource.Cast<object>().LastOrDefault();
            eListViewLogRoll.ScrollTo(lastItem, ScrollToPosition.End, false);
            LogRollScrollToEnd();
        }

        private void LogRollScrollToEnd()
        {

            if (eListViewLogRoll.ItemsSource == null)
                return;

            if (eListViewLogRoll.ItemsSource.Cast<object>().Count() == 0)
                return;

            var lastItem = eListViewLogRoll.ItemsSource.Cast<object>().LastOrDefault();
            eListViewLogRoll.ScrollTo(lastItem, ScrollToPosition.End, true);
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