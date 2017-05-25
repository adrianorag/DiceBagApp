using Xamarin.Forms;

namespace DiceBagApp
{
    public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
			InitializeComponent();
            this.Master = new MasterPage();
            this.Detail = new NavigationPage(new RoomPage());
            App.MasterDetail = this;
        }
        
    }
}
