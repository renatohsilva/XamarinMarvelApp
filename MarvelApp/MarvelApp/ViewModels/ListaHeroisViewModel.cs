using Acr.UserDialogs;
using MarvelApp.Helper;
using MarvelApp.Models;
using MarvelApp.Service;
using MarvelApp.ViewModels.Base;
using MarvelApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MarvelApp.ViewModels
{
    public class ListaHeroisViewModel : AbstractViewModel
    {
        public Command BuscarHeroisCommand { get; set; }
        public Command LoadMoreCommand { get; set; }

        public Command NavigationCommand { get; set; }

        private MarvelClient marvelClient;

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

        private Personagens heroiselecionado;
        public Personagens HeroiSelecionado
        {
            get
            {
                return heroiselecionado;
            }
            set
            {
                heroiselecionado = value;
                OnPropertyChanged();
            }
        }

        public ListaHeroisViewModel(IUserDialogs dialogs) : base(dialogs)
        {
            marvelClient = new MarvelClient();

            Personagens = new ObservableCollection<Personagens>();
            BuscarHeroisCommand = new Command(async () =>
            {
                using (Dialogs.Loading("Carregando"))
                {
                    var result = await marvelClient.GetPersonagens();
                    Personagens = new ObservableCollection<Personagens>(result.Results);
                }
            });

            int offset = 30;
            LoadMoreCommand = new Command(async () =>
            {
                IsBusy = true;

                var result = await marvelClient.GetPersonagens(null, 30, offset);
                offset += 30;

                foreach (var item in result.Results)
                {
                    Personagens.Add(item);
                }

                IsBusy = false;
            });

            NavigationCommand = new Command(async () =>
            {
                if (heroiselecionado != null)
                {
                    await NavHelper.PushModalAsync(new HeroiView(HeroiSelecionado));
                }
            });
        }
    }
}
