using System;
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
		}

	    private async void Button_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new CoordinatorPage { NestedContent = new NestedPage(), ImageSource = "splash.png" });
	    }
	}
}