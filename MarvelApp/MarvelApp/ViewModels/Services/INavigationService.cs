using MarvelApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarvelApp.ViewModels.Services
{
    public interface INavigationService
    {
        Task NavigateToHeroLista();
        Task NavigateToHeroDetail(Personagens hero);
    }
}
