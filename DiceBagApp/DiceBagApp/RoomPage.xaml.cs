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
            BindingContext = new RoomViewModel(new DiceService());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ((RoomViewModel)BindingContext).RefreshListGroupDice();
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var groupDice = (sender as ListView).SelectedItem as GroupDice;
            (BindingContext as RoomViewModel)?.RollDiceCommand.Execute(groupDice);

            var lastItem = eListViewLogRoll.ItemsSource.Cast<object>().LastOrDefault();
            eListViewLogRoll.ScrollTo(lastItem, ScrollToPosition.End, false);
        }
    }
}