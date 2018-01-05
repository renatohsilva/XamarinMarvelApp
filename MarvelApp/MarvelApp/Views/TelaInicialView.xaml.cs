using Acr.UserDialogs;
using MarvelApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarvelApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TelaInicialView : ContentPage
	{
		public TelaInicialView ()
		{
			InitializeComponent ();
            BindingContext = new TelaInicialViewModel(UserDialogs.Instance);
        }
	}
}