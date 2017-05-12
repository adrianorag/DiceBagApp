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
            BindingContext = new GroupDiceViewModel(new DiceService());
		}
	}
}