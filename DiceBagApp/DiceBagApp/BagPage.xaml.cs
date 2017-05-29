using DiceBagApp.Datas;
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
            BindingContext = new BagViewModel(new DiceService(), DiceDataBase, null);

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