using MarvelApp.ViewModels.Base;
using Acr.UserDialogs;
using Xamarin.Forms;
using MarvelApp.ViewModels.Services;
using System.Threading.Tasks;
using System;

namespace MarvelApp.ViewModels
{
    public class TelaInicialViewModel : AbstractViewModel
    {
        public Command BuscarPersonagensCommand { get; set; }
        private readonly INavigationService navigationService;

        private bool _IsBusy;

        public override bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
                BuscarPersonagensCommand?.ChangeCanExecute();
            }
        }

        public TelaInicialViewModel(IUserDialogs dialogs) : base(dialogs)
        {
            navigationService = DependencyService.Get<INavigationService>();

            BuscarPersonagensCommand = new Command(async () =>
            {
                IsBusy = true;
                await navigationService.NavigateToHeroLista();
                IsBusy = false;
            }, CanExecute());
        }

        private Func<bool> CanExecute()
        {
            return new Func<bool>(() => !IsBusy);
        }
    }
}
