using DiceBagApp.Models;
using DiceBagApp.Services;
using DiceBagApp.ViewModels;
using System.Linq;
using Xamarin.Forms;

namespace DiceBagApp
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            BindingContext = new MainViewModel(new DiceService());
		}
        
        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var dice = (sender as ListView).SelectedItem as Dice;
            (BindingContext as MainViewModel)?.RollDiceCommand.Execute(dice);

            var lastItem = eListViewLogRoll.ItemsSource.Cast<object>().LastOrDefault();
            eListViewLogRoll.ScrollTo(lastItem, ScrollToPosition.End, false);
        }
    }
}
