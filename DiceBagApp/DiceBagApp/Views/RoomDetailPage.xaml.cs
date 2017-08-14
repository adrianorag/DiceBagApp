using DiceBagApp.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DiceBagApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoomDetailPage : ContentPage
	{
		public RoomDetailPage ()
		{
			InitializeComponent();
            BindingContext = new RoomDetailViewModel();
		}
	}
}