using Acr.UserDialogs;
using MarvelApp.Models;
using MarvelApp.Service;
using MarvelApp.ViewModels.Base;
using MarvelApp.ViewModels.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace MarvelApp.ViewModels
{
    public class ListaHeroisViewModel : AbstractViewModel
    {
        public Command LoadMoreCommand { get; set; }
        public Command SearchCommand { get; set; }
        public Command NavigationCommand { get; set; }

        private Command InicializarHerois { get; set; }

        private ObservableCollection<Personagens> heroes;
        public ObservableCollection<Personagens> Heroes
        {
            get
            {
                return heroes;
            }
            set
            {
                heroes = value;
                OnPropertyChanged();
            }
        }

        private Personagens selectedHero;
        public Personagens SelectedHero
        {
            get
            {
                return selectedHero;
            }
            set
            {
                selectedHero = value;
                OnPropertyChanged();
            }
        }

        private String searchText;
        public String SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                OnPropertyChanged();

                if (String.IsNullOrEmpty(searchText))
                {
                    InicializarHerois?.Execute(null);
                }
            }
        }

        private Boolean thereIsResultList;
        public Boolean ThereIsResultList
        {
            get
            {
                return thereIsResultList;
            }
            set
            {
                thereIsResultList = value;
                OnPropertyChanged();
            }
        }

        private MarvelClient marvelClient;

        private readonly INavigationService navigationService;

        public ListaHeroisViewModel(IUserDialogs dialogs) : base(dialogs)
        {
            marvelClient = MarvelClient.Instance;
            navigationService = DependencyService.Get<INavigationService>();

            InicializarHerois = new Command(async () =>
            {
                using (Dialogs.Loading("Carregando"))
                {
                    Heroes = await FiltrarPersonagens();
                }
            });

            SearchCommand = new Command(async () =>
            {
                using (Dialogs.Loading("Carregando"))
                {
                    Heroes = await FiltrarPersonagens(SearchText);
                }
            });

            int offSetSearch = 30;
            LoadMoreCommand = new Command(async () =>
            {
                IsBusy = true;

                var result = await FiltrarPersonagens(SearchText, 30, offSetSearch);
                offSetSearch += 30;

                if (result.Any())
                {
                    ThereIsResultList = true;
                    result.ForEach(personagem => Heroes.Add(personagem));
                }
                else
                {
                    ThereIsResultList = false;
                }

                IsBusy = false;

            }, CanExecute());

            NavigationCommand = new Command(async () => await navigationService.NavigateToHeroDetail(SelectedHero), CanNavigate());

            InicializarHerois.Execute(null);
        }

        private async Task<ObservableCollection<Personagens>> FiltrarPersonagens(String filtro = null, int limit = 30, int offset = 0)
        {
            var heroes = new ObservableCollection<Personagens>();
            var result = await marvelClient.GetPersonagens(filtro, limit, offset);

            if (result.Results.Any())
            {
                ThereIsResultList = true;
                heroes = new ObservableCollection<Personagens>(result.Results);
            }
            else
            {
                ThereIsResultList = false;
            }

            return heroes;
        }

        private Func<bool> CanExecute()
        {
            return new Func<bool>(() => ThereIsResultList);
        }

        private Func<bool> CanNavigate()
        {
            return new Func<bool>(() => SelectedHero != null);
        }
    }
}
