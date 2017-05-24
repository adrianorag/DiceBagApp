using DiceBagApp.Models;
using DiceBagApp.Services;
using DiceBagApp.ViewModels;
using System.Linq;
using Xamarin.Forms;

namespace DiceBagApp
{
    public partial class MainPage : TabbedPage
	{
		public MainPage()
		{
			InitializeComponent();
            BindingContext = new MainViewModel(new DiceService());
		}


        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((MainViewModel)BindingContext).RefreshListGroupDice();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var groupDice = (sender as ListView).SelectedItem as GroupDice;
            (BindingContext as MainViewModel)?.RollDiceCommand.Execute(groupDice);

            var lastItem = eListViewLogRoll.ItemsSource.Cast<object>().LastOrDefault();
            eListViewLogRoll.ScrollTo(lastItem, ScrollToPosition.End, false);
        }
    }
}
