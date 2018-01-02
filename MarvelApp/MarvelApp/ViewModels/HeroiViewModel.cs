using Acr.UserDialogs;
using MarvelApp.Models;
using MarvelApp.Service;
using MarvelApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MarvelApp.ViewModels
{
    public class HeroiViewModel : AbstractViewModel
    {
        private readonly IOpenWebService _openWebService;

        private Personagens _Personagem;

        public Personagens Personagem
        {
            get
            {
                return _Personagem;
            }
            set
            {
                _Personagem = value;
                OnPropertyChanged();
            }
        }

        public HeroiViewModel(IUserDialogs dialogs, Personagens personagem) : base(dialogs)
        {
            Personagem = personagem;
            _openWebService = DependencyService.Get<IOpenWebService>();
        }

        #region OpenWeb Command

        private Command _OpenWeb;

        public Command OpenWeb
        {
            get
            {
                return _OpenWeb ?? (_OpenWeb = new Command(ExecuteOpenWebCommand, ValidateOpenWebCommand));
            }
        }

        private void ExecuteOpenWebCommand()
        {
            _openWebService.OpenUrl(Personagem.Image);
        }

        private bool ValidateOpenWebCommand()
        {
            return true;
        }

        #endregion

    }
}
