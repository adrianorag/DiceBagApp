using DiceBagApp.ViewModels;
using Xamarin.Forms;

namespace DiceBagApp
{
    public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            BindingContext = new MainViewModel();
		}
	}
}
