using DiceBagApp.Models;
using DiceBagApp.Services;
using DiceBagApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiceBagApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BagPage : ContentPage
    {
        public BagPage()
        {
            InitializeComponent();
            BindingContext = new BagViewModel(new DiceService());

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listViewGroupDice.ItemsSource = await ((BagViewModel)BindingContext).RefreshList();
        }

        private async void ButtonClickedRemoveGroupDice(object sender, System.EventArgs e)
        {
            var btn= (Button)sender;
            var groupDice = (GroupDice)btn.CommandParameter;

            await((BagViewModel)BindingContext).DeleteGroupDiceAsync(groupDice);
            listViewGroupDice.ItemsSource = await ((BagViewModel)BindingContext).RefreshList();
        }
    }
}