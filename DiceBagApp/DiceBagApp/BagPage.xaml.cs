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

        private void Button_Clicked(object sender, System.EventArgs e)
        {

        }
    }
}