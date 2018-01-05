using MarvelApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using MarvelApp.Models;
using MarvelApp.Service;
using MarvelApp.Views;

namespace MarvelApp.ViewModels
{
    public class TelaInicialViewModel : AbstractViewModel
    {
        public Command BuscarHeroisCommand { get; set; }

        private ObservableCollection<Personagens> personagens;
        public ObservableCollection<Personagens> Personagens
        {
            get
            {
                return personagens;
            }
            set
            {
                personagens = value;
                OnPropertyChanged();
            }
        }

        public TelaInicialViewModel(IUserDialogs dialogs) : base(dialogs)
        {
            MarvelClient marvelClient = new MarvelClient();

            Personagens = new ObservableCollection<Personagens>();
            BuscarHeroisCommand = new Command(async () =>
            {
                using (Dialogs.Loading("Carregando"))
                {
                    var result = await marvelClient.GetPersonagens();
                    Personagens = new ObservableCollection<Personagens>(result.Results);

                    await App.Current.MainPage.Navigation.PushModalAsync(new ListaHeroisView(Personagens));
                }
            });
        }
    }
}
