using MarvelApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace MarvelApp
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            DependencyService.Register<ViewModels.Services.INavigationService, Views.Services.NavigationService>();
            MainPage = new TelaInicialView();
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
