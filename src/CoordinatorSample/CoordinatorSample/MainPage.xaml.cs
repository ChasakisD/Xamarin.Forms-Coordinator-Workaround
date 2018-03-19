using System;
using CoordinatorWorkaround.CustomViews;
using Xamarin.Forms;

namespace CoordinatorSample
{
	public partial class MainPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

	    private async void Button_OnClicked(object sender, EventArgs e)
	    {
	        var page = new CoordinatorPage
	        {
	            Title = "Finally Worked Monkeys!",
	            NestedContent = new NestedPage(),
	            ImageSource = "monkey.png",
	            StatusBarColor = Color.MediumAquamarine,
	            ToolbarBackgroundColor = Color.MediumAquamarine,
	            CoordinatorBackgroundColor = Color.MediumAquamarine,
	            CoordinatorScrimBackgroundColor = Color.Transparent,
	            FloatingButtonImageSource = "icon.png",
	            FloatingButtonBackgroundColor = Color.DarkRed,
	            FloatingButtonCommand = new Command(async () => await DisplayAlert("hehe", "hehe", "ok"), () => true),
                ChangeStatusBarColor = true,
                HasBackButton = true
	        };


	        await Navigation.PushAsync(page);
	    }
    }
}
