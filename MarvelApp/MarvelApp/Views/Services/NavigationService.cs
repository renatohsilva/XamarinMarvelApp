using MarvelApp.Helper;
using MarvelApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarvelApp.Views.Services
{
    public class NavigationService : ViewModels.Services.INavigationService
    {
        #region INavigationService implementation

        public async Task NavigateToHeroLista()
        {
            await NavigationHelper.PushModalAsync(new ListaHeroisView());
        }

        public async Task NavigateToHeroDetail(Personagens hero)
        {
            await NavigationHelper.PushModalAsync(new HeroiView(hero));
        }

        #endregion

        public NavigationService()
        {
        }
    }
}
