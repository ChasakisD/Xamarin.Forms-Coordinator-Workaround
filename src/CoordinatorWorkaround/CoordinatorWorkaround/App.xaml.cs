﻿using CoordinatorWorkaround.Views;

namespace CoordinatorWorkaround
{
	public partial class App
	{
		public App ()
		{
			InitializeComponent();
            MainPage = new MainPage();
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
