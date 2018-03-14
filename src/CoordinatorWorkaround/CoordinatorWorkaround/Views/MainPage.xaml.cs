using System;
using CoordinatorWorkaround.CustomViews;
using Xamarin.Forms;
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
	        var cmd = new Command(async () => await DisplayAlert("hehe", "hehe", "ok"), () => true);

	        var page = new CoordinatorPage
	        {
                Title = "Finally Worked Monkeys!",
	            NestedContent = new NestedPage(),
	            ImageSource = "splash.png",
	            StatusBarColor = Color.DarkMagenta,
                ToolbarBackgroundColor = Color.DimGray,
                CoordinatorBackgroundColor = Color.Green,
	            CoordinatorScrimBackgroundColor = Color.Red,
	            FloatingButtonImageSource = "icon.png",
                FloatingButtonBackgroundColor = Color.DarkRed,
                FloatingButtonCommand = cmd
	        };


            await Navigation.PushAsync(page);
	    }
	}
}