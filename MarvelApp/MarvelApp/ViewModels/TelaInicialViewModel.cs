using MarvelApp.ViewModels.Base;
using Acr.UserDialogs;
using Xamarin.Forms;
using MarvelApp.ViewModels.Services;

namespace MarvelApp.ViewModels
{
    public class TelaInicialViewModel : AbstractViewModel
    {
        public Command BuscarHeroisCommand { get; set; }
        private readonly INavigationService navigationService;

        public TelaInicialViewModel(IUserDialogs dialogs) : base(dialogs)
        {
            navigationService = DependencyService.Get<INavigationService>();

            BuscarHeroisCommand = new Command(async () =>
            {
                await navigationService.NavigateToHeroLista();
            });
        }
    }
}
