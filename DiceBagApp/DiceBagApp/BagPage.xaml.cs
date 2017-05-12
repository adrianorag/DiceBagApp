using DiceBagApp.Services;
using DiceBagApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiceBagApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BagPage : ContentPage
	{
		public BagPage ()
		{
			InitializeComponent ();
            BindingContext = new BagViewModel(new DiceService());

        }
	}
}