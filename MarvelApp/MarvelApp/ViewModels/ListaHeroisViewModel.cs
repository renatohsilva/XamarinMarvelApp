using Acr.UserDialogs;
using MarvelApp.Helper;
using MarvelApp.Models;
using MarvelApp.Service;
using MarvelApp.ViewModels.Base;
using MarvelApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MarvelApp.ViewModels
{
    public class ListaHeroisViewModel : AbstractViewModel
    {
        private List<Personagens> PersonagensInicial;

        private MarvelClient marvelClient;

        private string _filter;
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                OnPropertyChanged();

                this.FilterHeroes();
            }
        }

        private ObservableCollection<Personagens> _personagens;
        public ObservableCollection<Personagens> Personagens
        {
            get
            {
                return _personagens;
            }
            set
            {
                _personagens = value;
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

        public Command LoadMoreCommand { get; set; }
        public Command NavigationCommand { get; set; }

        public ListaHeroisViewModel(IUserDialogs dialogs, ObservableCollection<Personagens> personagens) : base(dialogs)
        {
            PersonagensInicial = new List<Personagens>(personagens);
            Personagens = personagens;

            marvelClient = new MarvelClient();

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


        private void FilterHeroes()
        {
            if (String.IsNullOrEmpty(_filter))
            {
                Personagens = new ObservableCollection<Personagens>(PersonagensInicial);
            }
            else
            {
                var personagensList = new List<Personagens>(Personagens);
                var personagensFiltrados = personagensList?.Where(x => x.Name.ToLower().Contains(_filter.ToLower())).ToList();

                if (personagensFiltrados.Any())
                    Personagens = new ObservableCollection<Personagens>(personagensFiltrados);
            }
        }
    }
}
