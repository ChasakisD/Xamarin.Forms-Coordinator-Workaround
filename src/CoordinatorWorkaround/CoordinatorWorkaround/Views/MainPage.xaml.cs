using CoordinatorWorkaround.CustomViews;
using Xamarin.Forms.Xaml;

namespace CoordinatorWorkaround.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage
	{
		public MainPage ()
		{
			InitializeComponent ();
            
		    Content = new CoordinatorView{NestedContent = new NestedPage()};
		}
	}
}