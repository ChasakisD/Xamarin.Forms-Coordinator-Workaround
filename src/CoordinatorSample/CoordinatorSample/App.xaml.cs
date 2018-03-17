using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoordinatorWorkaround.CustomViews;
using Xamarin.Forms;

namespace CoordinatorSample
{
	public partial class App
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new NavigationPage(new MainPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
