using Acr.UserDialogs;
using MarvelApp.Helper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace MarvelApp.ViewModels.Base
{
    public abstract class AbstractViewModel : ObservableObject
    {
        private bool _IsBusy;

        public virtual bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }

        protected AbstractViewModel(IUserDialogs dialogs)
        {
            IsBusy = false;
            this.Dialogs = dialogs;
        }

        protected IUserDialogs Dialogs { get; }

        protected virtual void Result(string msg)
        {
            this.Dialogs.Alert(msg);
        }

        protected virtual ICommand LoadingCommand(MaskType mask)
        {
            return new Command(async () =>
            {
                var cancelSrc = new CancellationTokenSource();
                var config = new ProgressDialogConfig()
                    .SetTitle("Carregando")
                    .SetIsDeterministic(false)
                    .SetMaskType(mask)
                    .SetCancel(onCancel: cancelSrc.Cancel);

                using (this.Dialogs.Progress(config))
                {
                    try
                    {
                        await Task.Delay(TimeSpan.FromSeconds(5), cancelSrc.Token);
                    }
                    catch { }
                }
                this.Result(cancelSrc.IsCancellationRequested ? "Cancelado" : "Completo");
            });
        }
    }
}
